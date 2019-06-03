using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCulling : CullingGroupBase {

	//private MeshRenderer[] rend;

	public  List<ParticlesBevahavour> particles;
   // private ParticleCulling particleCulling;
    protected override void Start()
	{
        ParticlesBevahavour[] objs = FindObjectsOfType<ParticlesBevahavour>();
        particles = new List<ParticlesBevahavour>();
        cullingObj = new List<Transform>();
        for (int i = 0; i < objs.Length; i++)
        {
            cullingObj.Add(objs[i].transform);
            particles.Add(objs[i]);
        }
        base.Start();
        //particleCulling = GameObject.FindObjectOfType<ParticleCulling>();
        //rend = new MeshRenderer[cullingObj.Length];

		group.SetBoundingDistances(distances);
		group.SetDistanceReferencePoint(reference);
	}
	void Update(){
		int lon= spheres.Count;
		for(int i = 0; i< lon; i++)
		{
            BoundingSphere sphere = spheres[i];
            if (cullingObj[i] != null)
                sphere.position = cullingObj[i].position;
            spheres[i] = sphere;
           
            //spheres[i].position=cullingObj[i].position;
        }
        group.SetBoundingSpheres(spheres.ToArray());
    }
	protected override void OnStateChanged(CullingGroupEvent sphere)
	{
        if (particles[sphere.index] != null)
        {
            //Debug.Log("OnStateChanged " + sphere.index);
            if (!sphere.isVisible) particles[sphere.index].HasBecomeInvisible();
            if (sphere.isVisible) particles[sphere.index].HasBecomeVisible();
        }
	}
    public void RemoveParticle(ParticlesBevahavour particle)
    {
        //npcs.Remove(npc);
        int index = particles.IndexOf(particle);
        particles.RemoveAt(index);
        cullingObj.RemoveAt(index);

        spheres = new List<BoundingSphere>();
        int len = cullingObj.Count;
        for (int i = 0; i < len; i++)
        {
            spheres.Add(new BoundingSphere(cullingObj[i].position, 1.5f));// asignamos las esferas a los objectos que metamos en la array
        }
        group.SetBoundingSpheres(spheres.ToArray());// asignar las esferas a los objectos

        group.SetBoundingSphereCount(len);// asignamos el numero de esferas que vamos a usar
    }
    protected virtual void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;// no dibujar las esferas hasta que se le de a play

        Gizmos.color = Color.red;

        int len = particles.Count;

        for (int i = 0; i < len; i++)
        {
            //Gizmos.DrawWireSphere(cullingObj[i].position,1.5f);
            Gizmos.DrawWireSphere(spheres[i].position, spheres[i].radius);
        }

    }
}
