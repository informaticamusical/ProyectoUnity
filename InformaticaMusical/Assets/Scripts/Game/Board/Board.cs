using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Tablero contenedor de los tiles
    /// Constructor del tablero
    /// </summary>
    public class Board : MonoBehaviour
    {
        [Header("Attributes")]
        public Vector3 BoardOffset;     //Offset de todo el tablero

        [Header("References")]
        public Tile TilePrefab;     //TODO: Diferentes tipos de Tiles?

        /// <summary>
        /// Matriz del tablero
        /// </summary>
        public Tile[,] Tiles { get; protected set; }

        //TODO. Referencias a levelManager?
        //TODO: Constructor de mapa por fichero???

        /// <summary>
        /// Construye el tablero.
        /// </summary>
        /// <param name="width">Tamaño del tablero</param>
        public void Init(uint width)
        {
            //Creacion del tablero
            Tiles = new Tile[width, width];

            for (uint i = 0; i < width; i++)
            {
                for (uint j = 0; j < width; j++)
                {
                    //Construye el tile
                    Tiles[i, j] = Instantiate(TilePrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
                    Tiles[i, j].Init();
                }
            }

            //Aplicamos un offset para que el tablero este bien colocado
            transform.position = BoardOffset;
        }

        /// <summary>
        /// Devuelve el tamaño del tablero
        /// </summary>
        /// <returns></returns>
        public uint GetWidth() { return (uint)Tiles.GetLength(0); }
    }
}
