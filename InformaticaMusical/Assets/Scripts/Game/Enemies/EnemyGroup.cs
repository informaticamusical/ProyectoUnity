using UnityEngine;
using System.Collections.Generic;

namespace InformaticaMusical
{
    public class EnemyGroup : MonoBehaviour
    {
        public List<Enemy> Enemies { get; protected set; }
        public EnemyAsset EnemyAsset { get; protected set; }

        double lastBeat;

        public void Init(EnemyAsset enemyAsset)
        {
            EnemyAsset = enemyAsset;

            Enemies = new List<Enemy>();
            lastBeat = 0.0d;

            LevelManager.Instance.MusicResetDelegate += OnMusicReset;
        }

        private void OnMusicReset()
        {
            lastBeat = 0.0d;
        }

        private void Update()
        {
            //Me tengo que mover
            if (LevelManager.Instance.ConductorData.SongPosition > lastBeat + LevelManager.Instance.ConductorData.Crotchet * EnemyAsset.Multiplier)
            {
                foreach (Enemy enemy in Enemies)
                    enemy.DoAction(LevelManager.Instance.ConductorData.TrackedSong.pitch);

                lastBeat += LevelManager.Instance.ConductorData.Crotchet * EnemyAsset.Multiplier;
            }
        }
    }
}