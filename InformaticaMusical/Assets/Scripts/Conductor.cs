using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConductorData
{
    public double bpm = 120;
    public double crotchet;
    public double offset = 0.2d;
    public double songPosition;
    public double dpsin;
    AudioSource song;
    
    public ConductorData(AudioSource audioSource) { song = audioSource; crotchet = (60 / bpm) * 4; dpsin = AudioSettings.dspTime; }

    public void update() { songPosition = (AudioSettings.dspTime - dpsin)*song.pitch - offset; /*(song.time) * song.pitch - offset*/; }
}

public class Conductor : MonoBehaviour {

    ConductorData c;
    AudioSource song;

	// Use this for initialization
	void Awake () {
        song = GetComponent<AudioSource>();
        c = new ConductorData(song);
    }
	
	// Update is called once per frame
	void Update () {
        c.update();
	}

    public ConductorData GetConductorData(){
        return c;
    }
}
