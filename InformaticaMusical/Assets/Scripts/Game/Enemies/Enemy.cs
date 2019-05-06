using System;
using System.Collections;
using UnityEngine;

namespace InformaticaMusical
{
    public class Enemy : MonoBehaviour
    {
        private System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());

        private AudioSource audioSource;

        private Board _board;

        private int tilesPerJump = 1;


        Vector2Int tilePos;

        private void Awake()
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }

        public void Init(AudioClip audioClip, Board board)
        {
            audioSource.clip = audioClip;
            _board = board;

            tilePos = new Vector2Int();
        }

        public void SetTilePosition(int x, int y)
        {
            tilePos.x = x;
            tilePos.y = y;
        }

        public Vector2Int GetPosition()
        {
            return tilePos;
        }

        public void DoAction(float pitch)
        {
            // aumentar el pitch de este sonido
            audioSource.pitch = pitch;
            audioSource.Play();

            MoveGameObject();
        }


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

        private void MoveToTile(int nX, int nY, Vector3 target)
        {
            _board.Tiles[tilePos.x, tilePos.y].HasEnemy = false;
            _board.Tiles[nX, nY].HasEnemy = true;
            SetTilePosition(nX, nY);
            StartCoroutine(Jump(target));
        }

        private void MoveGameObject()
        {
            bool possible = false;
            int nX = 0, nY = 0;
            Vector3 target = new Vector3(0, 0, 0);

            do
            { //better way?
                int newTile = rnd.Next(0, 5);
                switch (newTile)
                {
                    case 0: //right
                        if (tilePos.x + tilesPerJump < _board.GetWidth() && !(_board.Tiles[tilePos.x + tilesPerJump, tilePos.y].HasEnemy))
                        {
                            nX = tilePos.x + tilesPerJump;
                            nY = tilePos.y;
                            target = (transform.right);
                            possible = true;
                        }
                        break;
                    case 1: //left
                        if (tilePos.x - tilesPerJump >= 0 && !(_board.Tiles[tilePos.x - tilesPerJump, tilePos.y].HasEnemy))
                        {
                            nX = tilePos.x - tilesPerJump;
                            nY = tilePos.y;
                            target = (-transform.right);
                            possible = true;
                        }
                        break;
                    case 2: //up
                        if (tilePos.y + tilesPerJump < _board.GetWidth() && !_board.Tiles[tilePos.x, tilePos.y + tilesPerJump].HasEnemy)
                        {
                            nX = tilePos.x;
                            nY = tilePos.y + tilesPerJump;
                            target = (transform.forward);
                            possible = true;
                        }
                        break;
                    case 3: //down
                        if (tilePos.y - tilesPerJump >= 0 && !_board.Tiles[tilePos.x, tilePos.y - tilesPerJump].HasEnemy)
                        {
                            nX = tilePos.x;
                            nY = tilePos.y - tilesPerJump;
                            target = (-transform.forward);
                            possible = true;
                        }
                        break;
                    case 4: //stay
                        nX = tilePos.x;
                        nY = tilePos.y;
                        possible = true;
                        break;
                }
            }
            while (!possible);

            if (possible)
                MoveToTile(nX, nY, target);
        }
    }

}