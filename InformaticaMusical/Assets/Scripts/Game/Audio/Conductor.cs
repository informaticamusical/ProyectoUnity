using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Clase conductora del ritmo de la canción
    /// TODO: Comentar
    /// </summary>
    [System.Serializable]
    public class ConductorData
    {
        public double Bpm = 120;

        /// <summary>
        /// Fuente de audio que se sigue
        /// </summary>
        public AudioSource TrackedSong;

        /// <summary>
        /// Posición actual de la canción
        /// </summary>
        public double SongPosition { get; private set; }
        public double Crotchet { get; private set; }

        private double offset = 0.2d;
        private double dpsin; //init time

        public void Init()
        {
            Crotchet = 60 / Bpm;
            dpsin = AudioSettings.dspTime;
        }

        /// <summary>
        /// Actualiza la posición de la canción
        /// </summary>
        public void Update()
        {
            SongPosition = (AudioSettings.dspTime - dpsin) * TrackedSong.pitch - offset; /*(song.time) * song.pitch - offset*/;
        }
    }
}