using UnityEngine;

namespace InformaticaMusical
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("References")]
        public Board Board;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Construye el juego
        /// </summary>
        private void Start()
        {
            Board.Init();
        }

    }
}
