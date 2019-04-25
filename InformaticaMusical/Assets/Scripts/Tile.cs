using UnityEngine;

namespace InformaticaMusical
{
    public class Tile : MonoBehaviour
    {
        public bool HasEnemy { get; set; }

        public void Init()
        {
            HasEnemy = false;
        }
    }
}
