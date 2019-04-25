using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBeat : MonoBehaviour {
    double lastBeat;
    ConductorData c = null;
	// Use this for initialization
	void Start () {
        lastBeat = 0.0d;
        c = this.gameObject.GetComponent<Conductor>().GetConductorData();
	}
	
	// Update is called once per frame
	void Update () {
        //Me tengo que mover
		if(c.songPosition > lastBeat + c.crotchet)
        {
            //do something
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x + 1, this.gameObject.transform.localScale.y+1, this.gameObject.transform.localScale.z+1);
            this.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            lastBeat += c.crotchet;
        }
	}
}
