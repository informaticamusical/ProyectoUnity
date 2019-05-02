using UnityEngine;

namespace InformaticaMusical
{
    public class Enemy : MonoBehaviour
    {
        private AudioSource audioSource;

        float startScale = 1.0f, maxScale = 3.0f;
        Material material;


        public float[] samples = new float[512];
        public float[] freqBand = new float[8];
        public float[] bandBuffer = new float[8];
        float[] bufferDecrease = new float[8];

        float[] freqBandHighest = new float[8];
        public float[] audioBand= new float[8];
        public float[] audioBandBuffer = new float[8];

        public float amplitude, amplitudeBuffer;
        public float amplitudeHighest;
        

        private void Awake()
        {
            audioSource = GetComponentInChildren<AudioSource>();
            material = GetComponentInChildren<MeshRenderer>().materials[0];
        }

        public void Init(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
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
        }

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
            Debug.Log("amplitude:" + amplitude);
            amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
        }

        void UpdateLocalScale()
        {
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, (amplitude * maxScale) + startScale, this.gameObject.transform.localScale.z);
            Color color = new Color(amplitude, amplitude, amplitude);
            material.SetColor("EmissionColor", color);
        }
    }

}

