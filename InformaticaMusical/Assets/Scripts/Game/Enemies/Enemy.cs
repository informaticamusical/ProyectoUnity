using System;
using UnityEngine;

namespace InformaticaMusical
{
    public class Enemy : MonoBehaviour
    {
        private System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());

        private AudioSource audioSource;
        private Rigidbody rb;
        Material material;

        private Board _board;

        private int velocity = 1, jumpForce = 5;

        float startScale = 1.0f, maxScale = 3.0f;

        private float[] samples = new float[512];
        private float[] freqBand = new float[8];
        private float[] bandBuffer = new float[8];
        float[] bufferDecrease = new float[8];

        float[] freqBandHighest = new float[8];
        private float[] audioBand= new float[8];
        private float[] audioBandBuffer = new float[8];

        private float amplitude, amplitudeBuffer;
        private float amplitudeHighest;
        

        private void Awake()
        {
            audioSource = GetComponentInChildren<AudioSource>();
            rb = GetComponent<Rigidbody>();
            material = GetComponentInChildren<MeshRenderer>().materials[0];
        }

        public void Init(AudioClip audioClip, Board board)
        {
            audioSource.clip = audioClip;
            _board = board;
        }

        public void Update()
        {
            GetSpectrumAudioSource();
            MakeFrequencyBands();
            BandBuffer();
            CreateAudioBands();
            GetAmplitude();

            UpdateLocalScale();
        }
        public void DoAction()
        {
            audioSource.Play();

            MoveGameObject();
        }

        #region metodosMusica

        void GetSpectrumAudioSource()
        {
            audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        }

        void MakeFrequencyBands()
        {
            int count = 0;
            for (int i = 0; i < freqBand.Length; i++)
            {
                float average = 0;
                // De 2 a 256 Hz
                int sampleCount = (int)Mathf.Pow(2, i) * 2;

                if (i == freqBand.Length - 1)
                {
                    sampleCount += 2;
                }

                for (int j = 0; j < sampleCount; j++)
                {
                    average += samples[count] * (count + 1);
                    count++;
                }
                average /= count;
                freqBand[i] = average * 10;
            }
        }

        void BandBuffer()
        {
            for(int i = 0; i < 8; i++)
            {
                if (freqBand[i] > bandBuffer[i])
                {
                    bandBuffer[i] = freqBand[i];
                    bufferDecrease[i] = 0.005f;
                }

                if (freqBand[i] > bandBuffer[i])
                {
                    bandBuffer[i] -= bufferDecrease[i];
                    bufferDecrease[i] *= 1.2f;
                }
            }
        }

        void CreateAudioBands()
        {
            for(int i = 0; i < freqBand.Length; i++)
            {
                if (freqBand[i] > freqBandHighest[i])
                {
                    freqBandHighest[i] = freqBand[i];
                }
                audioBand[i] = (freqBand[i] / freqBandHighest[i]);
                audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
            }
        }

        void GetAmplitude()
        {
            float currentAmplitude = 0;
            float currentAmplitudeBuffer = 0;
            
            for (int i =0; i < audioBand.Length; i++)
            {
                currentAmplitude += audioBand[i];
                currentAmplitudeBuffer += audioBandBuffer[i];
            }
            if (currentAmplitude > amplitudeHighest)
            {
                amplitudeHighest = currentAmplitude;
            }
            amplitude = currentAmplitude / amplitudeHighest;
            amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
        }

        #endregion metodosMusica

        void UpdateLocalScale()
        {
            if (!float.IsNaN(amplitude))
            {
                this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, (amplitude * maxScale) + startScale, this.gameObject.transform.localScale.z);
                Color color = new Color(amplitude, amplitude, amplitude);
                material.SetColor("EmissionColor", color);
            }
        }

        void MoveGameObject() {
           /* bool possible = false;
            Vector3Int myPosition = new Vector3Int((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, (int)this.gameObject.transform.position.z);
            do
            {
                int newTile = rnd.Next(0, 4);
                switch (newTile)
                {
                    case 0:
                        if (myPosition.x + velocity < _board.GetWidth() && !_board.Tiles[myPosition.x + velocity, myPosition.z].HasEnemy)
                        {
                            _board.Tiles[myPosition.x + velocity, myPosition.z].HasEnemy = true;
                            rb.velocity = new Vector3(velocity, jumpForce, 0);
                            possible = true;
                        }
                        break;
                    case 1:
                        if (myPosition.x - velocity >= 0 && !_board.Tiles[myPosition.x - velocity, myPosition.z].HasEnemy)
                        {
                            _board.Tiles[myPosition.x - velocity, myPosition.z].HasEnemy = true;
                            rb.velocity = new Vector3(-velocity, jumpForce, 0);
                            possible = true;
                        }
                        break;
                    case 2:
                        if (myPosition.z + velocity < _board.GetWidth() && !_board.Tiles[myPosition.x , myPosition.z + velocity].HasEnemy)
                        {
                            _board.Tiles[myPosition.x, myPosition.z + velocity].HasEnemy = true;
                            rb.velocity = new Vector3(0, jumpForce, velocity);
                            possible = true;
                        }
                        break;
                    case 3:
                        if (myPosition.z - velocity >= 0 && !_board.Tiles[myPosition.x , myPosition.z - velocity].HasEnemy)
                        {
                            _board.Tiles[myPosition.x, myPosition.z - velocity].HasEnemy = true;
                            rb.velocity = new Vector3(0, jumpForce, - velocity);
                            possible = true;
                        }
                        break;
                }
            }
            while (!possible);

            _board.Tiles[myPosition.x, myPosition.z].HasEnemy = false;*/
        }
    }

}

