using UnityEngine;
using System.Collections.Generic;

namespace InformaticaMusical
{
    public class EnemyGroup : MonoBehaviour
    {
        public List<Enemy> Enemies { get; protected set; }
        public EnemyAsset EnemyAsset { get; protected set; }

        private ConductorData _conductorData;
        double lastBeat;

        public void Init(ConductorData conductorData, EnemyAsset enemyAsset)
        {
            _conductorData = conductorData;
            EnemyAsset = enemyAsset;

            Enemies = new List<Enemy>();
            lastBeat = 0.0d;
        }

        private void Update()
        {
            //Me tengo que mover
            if (_conductorData.getSongPosition() > lastBeat + _conductorData.getCrotchet() * EnemyAsset.Multiplier)
            {
                // Aumentar el pitch de la melodia
                foreach (Enemy enemy in Enemies)
                    enemy.DoAction(_conductorData.song.pitch);

                lastBeat += _conductorData.getCrotchet() * EnemyAsset.Multiplier;
            }
        }
    }
}