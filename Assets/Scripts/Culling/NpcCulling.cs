using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCulling : CullingGroupBase {

    private Animator[] anims;

    public List<NpcBevahavour> npcs;
   // private NpcCulling npcCulling;

    protected override void Start()
	{
        NpcBevahavour[] objs = FindObjectsOfType<NpcBevahavour>();
        npcs = new List<NpcBevahavour>();
        cullingObj = new List<Transform>();
        for (int i = 0; i < objs.Length; i++)
        {
            cullingObj.Add(objs[i].transform);
            npcs.Add(objs[i]);
        }

		base.Start();       

		group.SetBoundingDistances(distances);
		group.SetDistanceReferencePoint(reference);
	}
	void Update(){
		int lon= cullingObj.Count;
		for(int i = 0; i< lon; i++)
		{
            BoundingSphere sphere = spheres[i];
            //spheres[i].position = cullingObj[i].position;
            if (cullingObj[i] != null)  sphere.position = cullingObj[i].position;
            spheres[i] = sphere;
            
            //Debug.Log(sphere.position);
            //spheres[i].position=cullingObj[i].position;
        }
        group.SetBoundingSpheres(spheres.ToArray());
    }
	protected override void OnStateChanged(CullingGroupEvent sphere)
	{
        //Debug.Log("OnStateChanged: " + sphere.index + " Visible: " + sphere.isVisible);
       if(npcs[sphere.index]!=null)
        {
            if (!sphere.isVisible) npcs[sphere.index].HasBecomeInvisible();
            if (sphere.isVisible) npcs[sphere.index].HasBecomeVisible();
        }
        //if (sphere.currentDistance <= distances[0] ) npcs[sphere.index].Near();
        //else if (sphere.currentDistance <= distances[1]) npcs[sphere.index].Far();
        //else if (sphere.currentDistance <= distances[2]) npcs[sphere.index].ToFar();
        //else if (sphere.currentDistance > distances[2] ) npcs[sphere.index].MuchFar();
        //Debug.Log(sphere.currentDistance);
    }

    public void RemoveNPC(NpcBevahavour npc)
    {
        //npcs.Remove(npc);
        int index = npcs.IndexOf(npc);

        npcs.RemoveAt(index);
        cullingObj.RemoveAt(index);

        // create spheres again
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

        int len = npcs.Count;

        for (int i = 0; i < len; i++)
        {
            //Gizmos.DrawWireSphere(cullingObj[i].position,1.5f);
            Gizmos.DrawWireSphere(spheres[i].position, spheres[i].radius);
        }

    }

}
