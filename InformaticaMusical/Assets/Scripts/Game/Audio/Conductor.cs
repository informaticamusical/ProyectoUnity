using UnityEngine;

namespace InformaticaMusical
{
    [System.Serializable]
    public class ConductorData
    {
        public double bpm = 120;
        public AudioSource song;

        private double songPosition;
        private double offset = 0.2d;
        private double dpsin; //init time
        private double crotchet;

        public void init() { crotchet = (60 / bpm); dpsin = AudioSettings.dspTime; }

        public void update() { songPosition = (AudioSettings.dspTime - dpsin) * song.pitch - offset; /*(song.time) * song.pitch - offset*/; }

        public double getCrotchet() { return crotchet; }
        public double getSongPosition() { return songPosition; }
        public float GetPitch() { return song.pitch;  }
        public void SetPitch(float pitch) { song.pitch = pitch; }
    }
}