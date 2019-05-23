using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesBevahavour : MonoBehaviour {

	// Use this for initialization

	private float timecounter=0f;
    private Renderer rend;
    private ParticleSystem particle;

	void Start () {

        rend = GetComponent<Renderer>();
        particle = GetComponent<ParticleSystem>();
        HasBecomeInvisible();
	}
	public void HasBecomeInvisible(){
        //anim.enabled=false;
        particle.Pause();
	}
	public void HasBecomeVisible(){
        //anim.enabled=true;
        particle.Play();
    }
}
