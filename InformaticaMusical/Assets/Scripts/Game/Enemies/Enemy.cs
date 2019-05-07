using System;
using System.Collections;
using UnityEngine;

namespace InformaticaMusical
{
    public class Enemy : MonoBehaviour
    {
        //Static
        /// <summary>
        /// Genera una semilla random
        /// </summary>
        private static System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode()); //TODO: Mejores formas de generar random?
        private static Vector2Int[] possibleDirs = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };

        //Properties
        public Vector2Int Pos { get; set; } //Posición logica del enemigo
        private int _tilesPerJump;

        //Own references
        private AudioSource audioSource;    //Referencia a su sonido

        //Other references
        private Board _board;               //Referencia al tablero

        /// <summary>
        /// Obtiene referencias
        /// </summary>
        private void Awake()
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }

        /// <summary>
        /// Inicializa el enemigo. Establece su clip
        /// </summary>
        /// <param name="audioClip"></param>
        /// <param name="board"></param>
        public void Init(AudioClip audioClip, float audioVolume, int tilesPerJump, Board board)
        {
            audioSource.clip = audioClip;
            audioSource.volume = audioVolume;

            _tilesPerJump = tilesPerJump;
            _board = board;

            Pos = new Vector2Int((int)transform.position.x, (int)transform.position.z);
            _board.Tiles[Pos.x, Pos.y] = true;
        }

        /// <summary>
        /// Es llamado
        /// TODO: UTILIZAR EL PITCH
        /// </summary>
        /// <param name="pitch"></param>
        public void DoAction()
        {
            // aumentar el pitch de este sonido
            audioSource.Play();

            MoveGameObject();
        }

        public void UpdatePitch()
        {
            audioSource.pitch = LevelManager.Instance.ConductorData.TrackedSong.pitch;
        }

        private void MoveGameObject()
        {
            int randomDir = rnd.Next(0, 4);

            Vector2Int movement = possibleDirs[randomDir] * _tilesPerJump;
            Vector2Int newPos = Pos + movement;

            if (newPos.x < _board.GetWidth() && newPos.x >= 0 && newPos.y < _board.GetWidth() && newPos.y >= 0 && !_board.Tiles[newPos.x, newPos.y])
            {
                _board.Tiles[Pos.x, Pos.y] = false;
                _board.Tiles[newPos.x, newPos.y] = true;
                Pos = newPos;

                StartCoroutine(Jump(new Vector3(movement.x, 0, movement.y)));
            }
        }

        /// <summary>
        /// Corrutina de salto
        /// TODO: Revisar y parametrizar
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private IEnumerator Jump(Vector3 direction)
        {
            bool jumping = true;
            Vector3 startPoint = transform.position;
            Vector3 targetPoint = startPoint + direction;
            float time = 0;
            float jumpProgress = 0;
            float tilesPerJumpY = -0.5f * Physics.gravity.y / 2; ;
            float height = startPoint.y;

            while (jumping)
            {
                jumpProgress = time / 0.5f;

                if (jumpProgress > 1)
                {
                    jumping = false;
                    jumpProgress = 1;
                }

                Vector3 currentPos = Vector3.Lerp(startPoint, targetPoint, jumpProgress);
                currentPos.y = height;
                transform.position = currentPos;

                //Wait until next frame.
                yield return null;

                height += tilesPerJumpY * Time.deltaTime;
                tilesPerJumpY += Time.deltaTime * Physics.gravity.y;
                time += Time.deltaTime;
            }

            transform.position = targetPoint;
            yield break;
        }

        /// <summary>
        /// Informa a su grupo de que sea eliminado
        /// </summary>
        public void Die()
        {
            GetComponentInParent<EnemyGroup>().RemoveEnemy(this);
        }

    }

}