using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Clase que representa un tile del juego
    /// </summary>
    public class Tile : MonoBehaviour
    {
        /// <summary>
        /// Posición lógica del tile
        /// </summary>
        public Vector2Int Pos { get; protected set; } 

        /// <summary>
        /// Devuelve tiene un enemigo encima
        /// TODO: Enemy en vez de bool?
        /// </summary>
        public bool HasEnemy { get; set; }

        /// <summary>
        /// Inicializa el Tile
        /// </summary>
        public void Init()
        {
            Pos.Set((int)transform.localPosition.x, (int)transform.localPosition.z);
            HasEnemy = false;
        }
    }
}
