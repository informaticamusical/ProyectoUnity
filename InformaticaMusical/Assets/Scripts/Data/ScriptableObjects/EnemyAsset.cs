using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Objeto Scriptable que define un tipo de enemigo
    /// </summary>
    [CreateAssetMenu(fileName = ("New Enemy"), menuName = "Enemy")]
    public class EnemyAsset : ScriptableObject
    {
        public Enemy EnemyPrefab;
        public AudioClip[] Audios;    //Pistas que reproducen al hacer una accion
    }
}