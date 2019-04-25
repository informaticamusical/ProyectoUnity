using UnityEngine;

namespace InformaticaMusical
{
    public class Board : MonoBehaviour
    {
        [Header("Attributes")]
        public int Width;

        [Header("References")]
        public Tile TilePrefab;
        public Enemy EnemyPrefab;

        public Tile[,] Tiles { get; set; }

        public void Init()
        {
            Tiles = new Tile[Width,Width];

            float offset = 0.5f;
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Width; j++)
                {
                    Tiles[i, j] = Instantiate(TilePrefab, new Vector3(i + offset, transform.position.y, j + offset), Quaternion.identity, transform);
                    Tiles[i, j].Init();
                }
        }

    }
}
