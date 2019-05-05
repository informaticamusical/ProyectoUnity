using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InformaticaMusical;

public class Panel : MonoBehaviour
{
    private ConductorData CD;
    public float time = 5.0f;
    

    void InitConductorData(ConductorData conductor)
    {
        CD = conductor;
    }

    void Awake()
    {
        Invoke("ClosePanel", time);
    }

    void ClosePanel()
    {
        if(CD.getSongPosition() >= time)
        {
            this.gameObject.SetActive(false);
        }
    }
}
