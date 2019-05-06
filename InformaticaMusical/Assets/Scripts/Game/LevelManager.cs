using UnityEngine;

namespace InformaticaMusical
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        public float PitchMultiplier;
        public float PitchIncrease;

        /// <summary>
        /// Conductor del ritmo de la canción
        /// </summary>
        public ConductorData ConductorData;

        double lastBeat;

        public bool Paused { get; set; }

        public System.Action MusicResetDelegate;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ConductorData.Init();
            lastBeat = 0.0d;
            Paused = true;
        }

        private void Update()
        {
            if (!Paused)
            {
                if (!ConductorData.TrackedSong.isPlaying)
                {
                    MusicResetDelegate?.Invoke();
    
                    ConductorData.Init();
                    lastBeat = 0.0d;
                    ConductorData.TrackedSong.Play();
                }

                ConductorData.Update();

                if (ConductorData.SongPosition > lastBeat + ConductorData.Crotchet * PitchMultiplier)
                {
                    ConductorData.TrackedSong.pitch += PitchIncrease;

                    lastBeat += ConductorData.Crotchet * PitchMultiplier;
                }
            }
        }
    }
}