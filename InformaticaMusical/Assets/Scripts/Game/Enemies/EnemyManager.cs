using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace InformaticaMusical
{
    /// <summary>
    /// Manager de los enemigos
    /// Permite instanciar enemigos en el tablero
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        /// <summary>
        /// Lista con la lista de los diferentes tipos de enemigos
        /// </summary>
        private List<EnemyGroup> Enemies;

        /// <summary>
        /// Referencia al tablero
        /// </summary>
        private Board _board;

        /// <summary>
        /// Obtiene referencias
        /// Inicializa atributos
        /// </summary>
        /// <param name="board"></param>
        public void Init(Board board)
        {
            _board = board;
            Enemies = new List<EnemyGroup>();
        }

        /// <summary>
        /// Añade un enemigo en una posición
        /// Lo añade a su lista de enemigos
        /// </summary>
        /// <param name="enemyAsset"></param>
        /// <param name="enemyPos"></param>
        public void AddEnemy(EnemyAsset enemyAsset, Vector2Int enemyPos)
        {
            //Comprobación de error
            if (enemyPos.x >= _board.GetWidth() || enemyPos.y >= _board.GetWidth())
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.LogError("Se ha tratado de añadir un enemigo en Tile Inexistente: " + enemyPos);
#endif
            else
            {
                //Buscamos el grupo al que corresponde este enemigo
                EnemyGroup enemyGroup = Enemies.FirstOrDefault(t => t.EnemyAsset == enemyAsset);

                //Si no existe el grupo
                if (enemyGroup == null)
                {
                    //Instancia e inicializa un grupo de enemigos
                    GameObject enemyGroupGO = new GameObject("EnemyGroup: " + enemyAsset.name);
                    enemyGroup = enemyGroupGO.AddComponent<EnemyGroup>();
                    enemyGroup.Init(enemyAsset, _board);
                    enemyGroup.transform.parent = transform;
                    Enemies.Add(enemyGroup);
                }

                //Añadimos un enemigo al grupo                    
                enemyGroup.AddEnemy(enemyPos);
            }

        }

    }
}