using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCulling : CullingGroupBase {

	private MeshRenderer[] rend;

	private ParticlesBevahavour[] particles;

	protected override void Start()
	{
		/*for(int i=0; i > cullingObj.Length; i++){
			cullingObj[i]= ;
		}*/
		base.Start();
		rend = new MeshRenderer[cullingObj.Length];
        particles = new ParticlesBevahavour[cullingObj.Length];
		int lon= cullingObj.Length;
		for(int i = 0; i< lon; i++)
		{
			rend[i]= cullingObj[i].GetComponent<MeshRenderer>();
            BoundingSphere sphere = spheres[i];
            sphere.position = transform.position;
            spheres[i] = sphere;
            //spheres[i].position=transform.position;
            particles[i]=cullingObj[i].GetComponent<ParticlesBevahavour>();
		}
		group.SetBoundingDistances(distances);
		group.SetDistanceReferencePoint(reference);
	}
	void Update(){
		int lon= cullingObj.Length;
		for(int i = 0; i< lon; i++)
		{
            BoundingSphere sphere = spheres[i];
            sphere.position = cullingObj[i].position;
            spheres[i] = sphere;
            //spheres[i].position=cullingObj[i].position;
		}

	}
	protected override void OnStateChanged(CullingGroupEvent sphere)
	{
        //Debug.Log("OnStateChanged " + sphere.index);
        if (!sphere.isVisible)
        {
            particles[sphere.index].HasBecomeInvisible();
        }
		if(sphere.isVisible) particles[sphere.index].HasBecomeVisible();
       
	}
	
}
