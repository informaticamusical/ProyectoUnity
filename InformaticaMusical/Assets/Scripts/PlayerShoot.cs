using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InformaticaMusical
{
    public class PlayerShoot : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                    if (hit.collider.GetComponent<Enemy>())
                        hit.collider.GetComponent<Enemy>().Die();
                }

            }
        }
    }
}
