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
        public Board Board;
        public EnemyManager EnemyManager;

        [Header("UI")]
        public GameObject InitialPanel;

        /// <summary>
        /// Construye el juego
        /// Inicializa el tablero y los enemigos
        /// </summary>
        public void Start()
        {
            Board.Init(BoardWidth);
            EnemyManager.Init(Board);

            //Instancia los enemigos iniciales
            for (int i = 0; i < InitialEnemies.Length; i++)
                EnemyManager.AddEnemy(InitialEnemies[i].EnemyAsset, InitialEnemies[i].EnemyPos);
        }

        /// <summary>
        /// Detecta cuando se ha acabado la musica de presentación, despausa el juego y se destruye
        /// </summary>
        private void Update()
        {
            if (!PresentationMusic.isPlaying)
            {
                //Inicio del nivel
                LevelManager.Instance.Init();

                //Destrucción
                Destroy(InitialPanel);
                Destroy(this.gameObject);
            }
        }

    }
}
