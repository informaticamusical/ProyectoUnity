using UnityEngine;

namespace InformaticaMusical
{
    /// <summary>
    /// Permite disparar al jugador. Lanza un rayo que si colisiona con un enemigo, le mata
    /// </summary>
    public class PlayerShoot : MonoBehaviour
    {
        /// <summary>
        /// Detecta input de disparo
        /// </summary>
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                    //Si es un enemigo, le informa de que ha muerto
                    if (hit.collider.GetComponent<Enemy>())
                        hit.collider.GetComponent<Enemy>().Die();
                }

            }
        }
    }
}
