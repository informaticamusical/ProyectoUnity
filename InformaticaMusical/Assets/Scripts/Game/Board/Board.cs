using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Tablero contenedor de los tiles
    /// Constructor del tablero
    /// </summary>
    public class Board : MonoBehaviour
    {
        [Header("References")]
        public GameObject TilePrefab;     //TODO: Diferentes tipos de Tiles?

        /// <summary>
        /// Matriz del tablero
        /// True tiene enemigo
        /// </summary>
        public bool[,] Tiles { get; set; }

        //TODO. Referencias a levelManager?
        //TODO: Constructor de mapa por fichero???

        /// <summary>
        /// Construye el tablero.
        /// </summary>
        /// <param name="width">Tamaño del tablero</param>
        public void Init(uint width)
        {
            //Creacion del tablero
            Tiles = new bool[width, width];

            for (uint i = 0; i < width; i++)
            {
                for (uint j = 0; j < width; j++)
                {
                    //Construye el tile
                    Instantiate(TilePrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
                    Tiles[i, j] = false;
                }
            }
        }

        /// <summary>
        /// Devuelve el tamaño del tablero
        /// </summary>
        /// <returns></returns>
        public uint GetWidth() { return (uint)Tiles.GetLength(0); }
    }
}
