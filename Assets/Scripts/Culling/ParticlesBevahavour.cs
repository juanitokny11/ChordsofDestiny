using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesBevahavour : MonoBehaviour {

	// Use this for initialization

    private ParticleSystem particle;
    public Light light;
    public bool Bidon;

    void Start () {
        particle = GetComponent<ParticleSystem>();
        HasBecomeInvisible();
	}
	public void HasBecomeInvisible(){
        //anim.enabled=false;
        if (particle != null)
            particle.Pause();
        if (Bidon)
        {
            light.enabled = false;
            light.gameObject.GetComponent<Animator>().enabled = false;
        }
           
    }
	public void HasBecomeVisible(){
        //anim.enabled=true;
        if(particle!=null)
            particle.Play();
        if (Bidon)
        {
            light.enabled = true;
            light.gameObject.GetComponent<Animator>().enabled = true;
        }
    }
}
