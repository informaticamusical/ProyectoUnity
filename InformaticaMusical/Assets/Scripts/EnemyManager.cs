using UnityEngine;
using System.Collections.Generic;

namespace InformaticaMusical
{
    public class EnemyData
    {
        public ConductorData ConductorData;
        public List<Enemy> Enemies;

        public EnemyData(List<Enemy> enemies)
        {
            Enemies = enemies;
        }
    }

    public class EnemyManager : MonoBehaviour
    {
        public Enemy EnemyPrefab;

        private List<EnemyData> Enemies;

        public void Init(Vector2Int[] enemiesPos)
        {
            Enemies = new List<EnemyData>();

            List<Enemy> enemiesAux = new List<Enemy>();

            //Instanciación de enemigos
            foreach (Vector2Int enemyPos in enemiesPos)
            {
                Tile tileAux = GameManager.Instance.Board.Tiles[enemyPos.x, enemyPos.y];               
                enemiesAux.Add(Instantiate(EnemyPrefab, tileAux.transform.position + new Vector3(0f,1.0f,0f), Quaternion.identity, transform));
                tileAux.HasEnemy = true;
            }

            Enemies.Add(new EnemyData(enemiesAux));
        }
    }
}
