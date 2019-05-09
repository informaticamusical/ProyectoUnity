using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Manager de la escena de juego
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        #region Singleton

        public static LevelManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion Singleton

        //Delegates
        public System.Action MusicResetDelegate;    //Se invoca cuando se hace loop de la canción

        [Header("Attributes")]
        public ConductorData ConductorData; // Conductor del ritmo de la canción
        public float PitchIncreasePerReset;

        /// <summary>
        /// Devuelve si el juego está pausado
        /// </summary>
        private bool paused;

        /// <summary>
        /// Inicializa valores
        /// </summary>
        private void Start()
        {
            paused = true;
        }

        /// <summary>
        /// Empieza el juego.
        /// Inicia la canción 
        /// </summary>
        public void Init()
        {
            paused = false;

            //Inicio conductorData
            ConductorData.Init();
            ConductorData.TrackedSong.Play();
        }

        /// <summary>
        /// Si el juego no está pausado, actualiza el conductorData y detecta si la canción ha llegado al final, en cuyo caso, 
        /// aumenta el pitch y reinicio el ConductorData, informando a todos los componentes suscritos a MusicResetDelegate
        /// </summary>
        private void Update()
        {
            if (!paused)
            {
                //Si la canción ha llegado al final
                if (!ConductorData.TrackedSong.isPlaying)
                {
                    //Aumentamos pitch
                    ConductorData.TrackedSong.pitch += PitchIncreasePerReset;

                    //Reinicio conductorData
                    ConductorData.Init();
                    ConductorData.TrackedSong.Play();

                    //Informa a todos los suscritos
                    MusicResetDelegate?.Invoke();
                }

                ConductorData.Update();
            }


            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}