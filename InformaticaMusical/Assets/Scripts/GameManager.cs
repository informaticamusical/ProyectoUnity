using UnityEngine;


/**
 * Board.
 * Tile.
 * Enemigos -> Arquero, lancero, soldado
 * GameManager. Encargado de la musica General.
 * EnemyManager ->
 * Jugador 
 * */
namespace InformaticaMusical
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Attributes")]
        public int Width;
        public Vector2Int[] EnemiesPos;

        [Header("References")]
        public Board Board;
        public EnemyManager EnemyManager;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Construye el juego
        /// </summary>
        private void Start()
        {
            Board.Init(Width);
            EnemyManager.Init(EnemiesPos);
        }
    }
}
