using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCulling : CullingGroupBase {

	//private MeshRenderer[] rend;

	public  List<ParticlesBevahavour> particles;
    private ParticleCulling particleCulling;
    protected override void Start()
	{
		/*for(int i=0; i > cullingObj.Length; i++){
			cullingObj[i]= ;
		}*/
		base.Start();
        particleCulling = GameObject.FindObjectOfType<ParticleCulling>();
        //rend = new MeshRenderer[cullingObj.Length];
        particles = new List<ParticlesBevahavour>();
		int lon= cullingObj.Length;
		for(int i = 0; i< lon; i++)
		{
            //spheres[i].position=transform.position;
            particles.Add(cullingObj[i].GetComponent<ParticlesBevahavour>());
		}
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
            group.SetBoundingSpheres(spheres.ToArray());
            //spheres[i].position=cullingObj[i].position;
        }

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
    public void RemoveParticle(ParticlesBevahavour particle, BoundingSphere sphere)
    {
        //npcs.Remove(npc);
        int index = particles.IndexOf(particle);
        particles.RemoveAt(index);
        spheres.Remove(particleCulling.spheres[index]);
        group.SetBoundingSpheres(spheres.ToArray());
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
