using UnityEngine;
using System.Collections.Generic;

namespace InformaticaMusical
{

    public class EnemyGroup : MonoBehaviour
    {
        //public ConductorData ConductorData;
        public List<Enemy> Enemies;
        public EnemyAsset EnemyAsset { get; protected set; }

        public void Init(EnemyAsset enemyAsset)
        {
            EnemyAsset = enemyAsset;
            Enemies = new List<Enemy>();
        }
    }
}