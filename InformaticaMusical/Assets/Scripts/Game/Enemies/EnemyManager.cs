using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InformaticaMusical {
    public class EnemyManager : MonoBehaviour {
        /// <summary>
        /// TODO: Comentario
        /// //un conductor por ritmo. Si todos siguen el ritmo de la cancion de fondo, solo habria un conductor, se tendria que cambiar el multiplicador por cada tipo de enemigo
        /// </summary>
        public ConductorData ConductorData;

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
        public void Init (Board board) {
            _board = board;
            Enemies = new List<EnemyGroup> ();

            ConductorData.init ();
        }

        
        private void Update () { 
            ConductorData.update ();
        }

        /// <summary>
        /// Añade un enemigo en una posición
        /// </summary>
        /// <param name="enemyAsset"></param>
        /// <param name="enemyPos"></param>
        public void AddEnemy (EnemyAsset enemyAsset, Vector2Int enemyPos) {
            //Comprobación de error
            if (enemyPos.x >= _board.GetWidth () || enemyPos.y >= _board.GetWidth ())
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.LogError ("Se ha tratado de añadir un enemigo en Tile Inexistente");
#endif
            else {
                //Buscamos el grupo al que corresponde este enemigo
                EnemyGroup enemyGroup = Enemies.FirstOrDefault (t => t.EnemyAsset == enemyAsset);

                //Si no existe el grupo, se crea 
                if (enemyGroup == null) {
                    GameObject enemyGroupGO = new GameObject ("EnemyGroup: " + enemyAsset.name);
                    enemyGroup = enemyGroupGO.AddComponent<EnemyGroup> ();
                    enemyGroup.Init (ConductorData, enemyAsset);
                    enemyGroup.transform.parent = transform;
                    Enemies.Add (enemyGroup);
                }

                //Comprobación de si se puede añadir este tipo de enemigo
                if (enemyGroup.Enemies.Count == enemyGroup.EnemyAsset.Audios.Length)
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                    Debug.LogError ("No se pueden añadir más enemigos de este tipo");
#endif

                else {
                    //Creamos y añadimos el enemigo a su grupo
                    Enemy enemy = Instantiate (enemyGroup.EnemyAsset.EnemyPrefab, new Vector3 (enemyPos.x, _board.TilePrefab.gameObject.transform.localScale.y / 2.0f, enemyPos.y), Quaternion.identity, enemyGroup.transform);
                    enemy.Init (enemyAsset.Audios[enemyGroup.Enemies.Count], _board);
                    enemyGroup.Enemies.Add (enemy);
                    _board.Tiles[enemyPos.x, enemyPos.y].HasEnemy = true;
                    enemy.SetTilePosition (enemyPos.x, enemyPos.y);
                }
            }

        }
    }
}