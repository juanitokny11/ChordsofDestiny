using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesBevahavour : MonoBehaviour {

	// Use this for initialization

    private ParticleSystem particle;

	void Start () {
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
