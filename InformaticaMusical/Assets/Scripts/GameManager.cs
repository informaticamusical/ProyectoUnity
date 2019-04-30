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

        private void Awake()
        {
            Instance = this;
        }

    }
}
