using UnityEngine;

namespace InformaticaMusical
{
    public class Tile : MonoBehaviour
    {
        public Enemy Enemy { get; set; }

        /// <summary>
        /// Devuelve si hay un enemigo en la casilla
        /// </summary>
        /// <returns></returns>
        public bool HasEnemy() { return Enemy != null; }

        public void Init()
        {
            Enemy = null;
        }
    }
}
