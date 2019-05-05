using System;
using System.Collections;
using UnityEngine;

namespace InformaticaMusical {
    public class Enemy : MonoBehaviour {
        private System.Random rnd = new System.Random (Guid.NewGuid ().GetHashCode ());

        private AudioSource audioSource;
        private Rigidbody rb;
        Material material;

        private Board _board;

        private int tilesPerJump = 1;

        float startScale = 1.0f, maxScale = 3.0f;

        private float[] samples = new float[512];
        private float[] freqBand = new float[8];
        private float[] bandBuffer = new float[8];
        float[] bufferDecrease = new float[8];

        float[] freqBandHighest = new float[8];
        private float[] audioBand = new float[8];
        private float[] audioBandBuffer = new float[8];

        private float amplitude, amplitudeBuffer;
        private float amplitudeHighest;

        Vector2Int tilePos;

        private void Awake () {
            audioSource = GetComponentInChildren<AudioSource> ();
            rb = GetComponent<Rigidbody> ();
            material = GetComponentInChildren<MeshRenderer> ().materials[0];
        }

        public void Init (AudioClip audioClip, Board board) {
            audioSource.clip = audioClip;
            _board = board;

            tilePos = new Vector2Int ();
        }

        public void SetTilePosition (int x, int y) {
            tilePos.x = x;
            tilePos.y = y;
        }

        public Vector2Int GetPosition () {
            return tilePos;
        }

        public void Update () {
            GetSpectrumAudioSource ();
            MakeFrequencyBands ();
            BandBuffer ();
            CreateAudioBands ();
            GetAmplitude ();

            UpdateLocalScale ();
        }

        public void DoAction (float pitch) {
            // aumentar el pitch de este sonido
            audioSource.pitch = pitch;
            audioSource.Play();

            MoveGameObject();
        }

        #region metodosMusica

        void GetSpectrumAudioSource () {
            audioSource.GetSpectrumData (samples, 0, FFTWindow.Blackman);
        }

        void MakeFrequencyBands () {
            int count = 0;
            for (int i = 0; i < freqBand.Length; i++) {
                float average = 0;
                // De 2 a 256 Hz
                int sampleCount = (int) Mathf.Pow (2, i) * 2;

                if (i == freqBand.Length - 1) {
                    sampleCount += 2;
                }

                for (int j = 0; j < sampleCount; j++) {
                    average += samples[count] * (count + 1);
                    count++;
                }
                average /= count;
                freqBand[i] = average * 10;
            }
        }

        void BandBuffer () {
            for (int i = 0; i < 8; i++) {
                if (freqBand[i] > bandBuffer[i]) {
                    bandBuffer[i] = freqBand[i];
                    bufferDecrease[i] = 0.005f;
                }

                if (freqBand[i] > bandBuffer[i]) {
                    bandBuffer[i] -= bufferDecrease[i];
                    bufferDecrease[i] *= 1.2f;
                }
            }
        }

        void CreateAudioBands () {
            for (int i = 0; i < freqBand.Length; i++) {
                if (freqBand[i] > freqBandHighest[i]) {
                    freqBandHighest[i] = freqBand[i];
                }
                audioBand[i] = (freqBand[i] / freqBandHighest[i]);
                audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
            }
        }

        void GetAmplitude () {
            float currentAmplitude = 0;
            float currentAmplitudeBuffer = 0;

            for (int i = 0; i < audioBand.Length; i++) {
                currentAmplitude += audioBand[i];
                currentAmplitudeBuffer += audioBandBuffer[i];
            }
            if (currentAmplitude > amplitudeHighest) {
                amplitudeHighest = currentAmplitude;
            }
            amplitude = currentAmplitude / amplitudeHighest;
            amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
        }

        #endregion metodosMusica

        #region Movements

        void UpdateLocalScale () {
            if (!float.IsNaN (amplitude)) {
                this.gameObject.transform.localScale = new Vector3 (this.gameObject.transform.localScale.x, (amplitude * maxScale) + startScale, this.gameObject.transform.localScale.z);
                Color color = new Color (amplitude, amplitude, amplitude);
                material.SetColor ("EmissionColor", color);
            }
        }

        private IEnumerator Jump (Vector3 direction) {
            bool jumping = true;
            Vector3 startPoint = transform.position;
            Vector3 targetPoint = startPoint + direction;
            float time = 0;
            float jumpProgress = 0;
            float tilesPerJumpY = -0.5f * Physics.gravity.y / 2;;
            float height = startPoint.y;

            while (jumping) {
                jumpProgress = time / 0.5f;

                if (jumpProgress > 1) {
                    jumping = false;
                    jumpProgress = 1;
                }

                Vector3 currentPos = Vector3.Lerp (startPoint, targetPoint, jumpProgress);
                currentPos.y = height;
                transform.position = currentPos;

                //Wait until next frame.
                yield return null;

                height += tilesPerJumpY * Time.deltaTime;
                tilesPerJumpY += Time.deltaTime * Physics.gravity.y;
                time += Time.deltaTime;
            }

            transform.position = targetPoint;
            yield break;
        }

        private void MoveToTile (int nX, int nY, Vector3 target) {
            _board.Tiles[tilePos.x, tilePos.y].HasEnemy = false;
            _board.Tiles[nX, nY].HasEnemy = true;
            SetTilePosition (nX, nY);
            StartCoroutine (Jump (target));
        }

        private void MoveGameObject () {
            bool possible = false;
            int nX = 0, nY = 0;
            Vector3 target = new Vector3(0, 0, 0);

            do { //better way?
                int newTile = rnd.Next (0, 5);
                switch (newTile) {
                    case 0: //right
                        if (tilePos.x + tilesPerJump < _board.GetWidth () && !(_board.Tiles[tilePos.x + tilesPerJump, tilePos.y].HasEnemy)) {
                            nX = tilePos.x + tilesPerJump;
                            nY = tilePos.y;
                            target = (transform.right);
                            possible = true;
                        }
                        break;
                    case 1: //left
                        if (tilePos.x - tilesPerJump >= 0 && !(_board.Tiles[tilePos.x - tilesPerJump, tilePos.y].HasEnemy)) {
                            nX = tilePos.x - tilesPerJump;
                            nY = tilePos.y;
                            target = (-transform.right);
                            possible = true;
                        }
                        break;
                    case 2: //up
                        if (tilePos.y + tilesPerJump < _board.GetWidth () && !_board.Tiles[tilePos.x, tilePos.y + tilesPerJump].HasEnemy) {
                            nX = tilePos.x;
                            nY = tilePos.y + tilesPerJump;
                            target = (transform.forward);
                            possible = true;
                        }
                        break;
                    case 3: //down
                        if (tilePos.y - tilesPerJump >= 0 && !_board.Tiles[tilePos.x, tilePos.y - tilesPerJump].HasEnemy) {
                            nX = tilePos.x;
                            nY = tilePos.y - tilesPerJump;
                            target = (-transform.forward);
                            possible = true;
                        }
                        break;
                    case 4: //stay
                        nX = tilePos.x;
                        nY = tilePos.y;
                        possible = true;
                        break;
                }
            }
            while (!possible);

            if (possible)
                MoveToTile (nX, nY, target);
        }
    }

    #endregion Movements

}