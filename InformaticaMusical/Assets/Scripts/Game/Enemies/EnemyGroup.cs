using UnityEngine;
using System.Collections.Generic;

namespace InformaticaMusical
{
    public class EnemyGroup : MonoBehaviour
    {
        /// <summary>
        /// Tipo de enemigo que contiene este grupo
        /// </summary>
        public EnemyAsset EnemyAsset { get; protected set; }

        private List<Enemy> enemies;    //Lista de enemigos

        private Board _board;           //Referencia al tablero
        private double lastBeat;        //Temporizador

        /// <summary>
        /// Obtiene referencias, inicializa valores y se suscribe a eventos
        /// </summary>
        /// <param name="enemyAsset"></param>
        /// <param name="board"></param>
        public void Init(EnemyAsset enemyAsset, Board board)
        {
            EnemyAsset = enemyAsset;
            _board = board;

            enemies = new List<Enemy>();
            lastBeat = 0.0d;

            //Se suscribe para que le informen cuando la musica ha acabado
            LevelManager.Instance.MusicResetDelegate += OnMusicReset;
        }

        /// <summary>
        /// Añade un enemigo al grupo en la posición
        /// </summary>
        /// <param name="enemyPos"></param>
        public void AddEnemy(Vector2Int enemyPos)
        {
            //Comprobación de si se puede añadir este tipo de enemigo
            if (enemies.Count == EnemyAsset.Audios.Length)
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.LogError("No se pueden añadir más enemigos de este tipo: " + EnemyAsset.name);
#endif

            //Instancia
            Enemy enemy = Instantiate(EnemyAsset.EnemyPrefab, new Vector3(enemyPos.x, EnemyAsset.EnemyPrefab.transform.localScale.y / 2.0f, enemyPos.y), Quaternion.identity, transform);

            //Inicializa
            enemy.Init(EnemyAsset.Audios[enemies.Count], EnemyAsset.AudioVolume, EnemyAsset.TilesPerJump, _board);

            //Añade al grupo
            enemies.Add(enemy);
        }

        /// <summary>
        /// Elimina un enemigo del grupo
        /// Destruye su instancia
        /// </summary>
        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }

        /// <summary>
        /// Es llamado cuando la musica es reseteada. Resetea su temporizador
        /// Informa a sus enemigos de que actualicen su pitch
        /// </summary>
        private void OnMusicReset()
        {
            lastBeat = 0.0d;
            foreach (Enemy enemy in enemies)
                enemy.UpdatePitch();
        }

        /// <summary>
        /// Detecta si le toca al grupo actuar y le dice a su lista que actue
        /// </summary>
        private void Update()
        {
            //Me tengo que mover
            if (LevelManager.Instance.ConductorData.SongPosition > lastBeat + LevelManager.Instance.ConductorData.Crotchet * EnemyAsset.Multiplier)
            {
                foreach (Enemy enemy in enemies)
                    enemy.DoAction();

                lastBeat += LevelManager.Instance.ConductorData.Crotchet * EnemyAsset.Multiplier;
            }
        }
    }
}