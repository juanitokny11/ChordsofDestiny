using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBevahavour : MonoBehaviour {

	// Use this for initialization
	private float timecounter=0f;
    public SkinnedMeshRenderer rend;
    private Animator anim;

	void Start () {

        //rend = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
	}
	public void Near(){
        anim.enabled=true;
       // rend.enabled = true;

    }
	public void Far(){
        anim.enabled=false;
       // rend.enabled = true;
    }
    public void ToFar()
    {
        anim.enabled=false;
        //rend.enabled = false;

    }
    public void MuchFar()
    {
       anim.enabled=false;
       //rend.enabled = false;
    }
    public void HasBecomeInvisible()
    {
        anim.enabled=false;
        //rend.enabled = false;
    }
    public void HasBecomeVisible()
    {
        anim.enabled=true;
        //rend.enabled = true;
    }
}
