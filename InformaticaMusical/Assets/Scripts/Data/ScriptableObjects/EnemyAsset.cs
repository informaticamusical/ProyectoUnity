using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Objeto Scriptable que define un tipo de enemigo
    /// </summary>
    [CreateAssetMenu(fileName = ("New Enemy"), menuName = "Enemy")]
    public class EnemyAsset : ScriptableObject
    {
        [Header("Logic")]
        public double Multiplier = 4;         //Compás que sigue el enemigo. 4 = 1 Compas
        public int TilesPerJump = 1;          //Número de tiles que se mueve el enemigo por turno

        [Header("Audio")]
        public float AudioVolume = 1.0f;      //Volumen de los clips
        public AudioClip[] Audios;            //Pistas que reproducen al hacer una accion

        [Header("References")]
        public Enemy EnemyPrefab;
    }
}