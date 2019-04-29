using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace InformaticaMusical
{
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
        /// </summary>
        /// <param name="enemyAsset"></param>
        /// <param name="enemyPos"></param>
        public void AddEnemy(EnemyAsset enemyAsset, Vector2Int enemyPos)
        {
            //Comprobación de error
            if (enemyPos.x >= _board.GetWidth() || enemyPos.y >= _board.GetWidth())
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.LogError("Se ha tratado de añadir un enemigo en Tile Inexistente");
#endif
            else
            {
                //Buscamos el grupo al que corresponde este enemigo
                EnemyGroup enemyGroup = Enemies.FirstOrDefault(t => t.EnemyAsset == enemyAsset);

                //Si no existe el grupo, se crea 
                if (enemyGroup == null)
                {
                    GameObject enemyGroupGO = new GameObject("EnemyGroup: " + enemyAsset.name);
                    enemyGroup =  enemyGroupGO.AddComponent<EnemyGroup>();
                    enemyGroup.Init(enemyAsset);
                    enemyGroup.transform.parent = transform;
                    Enemies.Add(enemyGroup);
                }

                //Creamos y añadimos el enemigo a su grupo
                Enemy enemy = Instantiate(enemyGroup.EnemyAsset.EnemyPrefab, new Vector3(enemyPos.x,0.0f,enemyPos.y),Quaternion.identity,enemyGroup.transform);
                enemyGroup.Enemies.Add(enemy);
                _board.Tiles[enemyPos.x, enemyPos.y].HasEnemy = true;
            }

        }
    }
}
