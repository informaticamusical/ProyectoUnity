using UnityEngine;

namespace InformaticaMusical
{
    public class Board : MonoBehaviour
    {
        [Header("References")]
        public Tile TilePrefab;

        public Tile[,] Tiles { get; set; }

        public void Init(int width)
        {
            //Creacion del tablero
            Tiles = new Tile[width, width];

            float offset = 0.5f;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Tiles[i, j] = Instantiate(TilePrefab, new Vector3(i + offset, transform.position.y, j + offset), Quaternion.identity, transform);
                    Tiles[i, j].Init();
                }
            }

        }

    }
}
