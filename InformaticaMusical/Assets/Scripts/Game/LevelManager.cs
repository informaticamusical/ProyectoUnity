﻿using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Clase serializable para poder establecer enemigos desde el Editor
    /// </summary>
    [System.Serializable]
    public class EnemyInfo
    {
        public EnemyAsset EnemyAsset;
        public Vector2Int EnemyPos;
    }

    public class LevelManager : MonoBehaviour
    {
        [Header("Attributes")]
        public EnemyInfo[] InitialEnemies;  //Lista de enemigos iniciales
        public uint BoardWidth;

        [Header("References")]
        public Board Board;
        public EnemyManager EnemyManager;

        /// <summary>
        /// Construye el juego
        /// Inicializa el tablero y los enemigos
        /// </summary>
        private void Start()
        {
            Board.Init(BoardWidth);
            EnemyManager.Init(Board);

            //Instancia los enemigos iniciales
            for (int i = 0; i < InitialEnemies.Length; i++)
                EnemyManager.AddEnemy(InitialEnemies[i].EnemyAsset, InitialEnemies[i].EnemyPos);
        }
    }
}