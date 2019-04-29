using UnityEngine;

namespace InformaticaMusical
{
    public class Enemy : MonoBehaviour
    {

        public void Init()
        {

        }

        public void DoAction()
        {
            Debug.Log("Acción de: " + gameObject.name);
        }
    }

}

