using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Clase serializable para poder establecer enemigos desde el Editor
    /// Tipo de enemigo y posicion en la que aparece
    /// </summary>
    [System.Serializable]
    public class EnemyInfo
    {
        public EnemyAsset EnemyAsset;
        public Vector2Int EnemyPos;
    }

    /// <summary>
    /// Construye el nivel y se destruye
    /// </summary>
    public class LevelConstructor : MonoBehaviour
    {
        [Header("Attributes")]
        public uint BoardWidth;
        public EnemyInfo[] InitialEnemies;  //Lista de enemigos iniciales
        public AudioSource PresentationMusic;

        [Header("References")]
        public Board BoardPrefab;
        public EnemyManager EnemyManager;
        public AudioSource Music;

        [Header("UI")]
        public GameObject InitialPanel;

        private Board board;

        /// <summary>
        /// Construye el juego
        /// Inicializa el tablero y los enemigos
        /// </summary>
        public void Start()
        {
            board = Instantiate(BoardPrefab);
            board.Init(BoardWidth);

            EnemyManager.Init(board);

            //Instancia los enemigos iniciales
            for (int i = 0; i < InitialEnemies.Length; i++)
                EnemyManager.AddEnemy(InitialEnemies[i].EnemyAsset, InitialEnemies[i].EnemyPos);
        }

        private void Update()
        {
            if (!PresentationMusic.isPlaying)
            {
                StartGame();
                Destroy(InitialPanel);
            }
        }

        private void StartGame()
        {
            Music.Play();
            Destroy(this.gameObject);
        }

    }
}
