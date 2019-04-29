using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Clase que representa un tile del juego
    /// </summary>
    public class Tile : MonoBehaviour
    {
        /// <summary>
        /// Devuelve si tiene un enemigo encima
        /// TODO: Enemy en vez de bool?
        /// </summary>
        public bool HasEnemy { get; set; }

        /// <summary>
        /// Posición lógica del tile
        /// </summary>
        private Vector2Int pos;

        /// <summary>
        /// Inicializa atributos
        /// </summary>
        public void Init()
        {
            HasEnemy = false;
            pos.Set((int)transform.localPosition.x, (int)transform.localPosition.z);
        }
    }
}
