using UnityEngine;

namespace InformaticaMusical
{
    public class Enemy : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }

        public void Init(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
        }

        public void DoAction()
        {
            audioSource.Play();
        }
    }

}

