using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBeat : MonoBehaviour {
    public ConductorData c; //un conductor por ritmo. Si todos siguen el ritmo de la cancion de fondo, solo habria un conductor, se tendria que cambiar el multiplicador por cada tipo de enemigo
    public double multiplier = 4;
    double lastBeat;

	// Use this for initialization
	void Start () {
        lastBeat = 0.0d;
        c.init();
	}
	
	// Update is called once per frame
	void Update () {
        c.update(); //deberia estar en el manager de los enemigos

        //Me tengo que mover
		if(c.getSongPosition() > lastBeat + c.getCrotchet() * multiplier)
        {
            //do something
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + 1, this.gameObject.transform.localScale.y+1, this.gameObject.transform.localScale.z+1);
            this.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            lastBeat += c.getCrotchet()* multiplier;
        }
	}
}
