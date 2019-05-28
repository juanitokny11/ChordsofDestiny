using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCulling : CullingGroupBase {



    private Animator[] anims;

    public List<NpcBevahavour> npcs;
    private NpcCulling npcCulling;

    protected override void Start()
	{
		/*for(int i=0; i > cullingObj.Length; i++){
			cullingObj[i]= ;
		}*/
		base.Start();
        npcCulling = GameObject.FindObjectOfType<NpcCulling>();
        npcs =new List<NpcBevahavour>();
        int lon = cullingObj.Length;
		for(int i = 0; i< lon; i++)
		{
			npcs.Add(cullingObj[i].GetComponent<NpcBevahavour>());
		}
		group.SetBoundingDistances(distances);
		group.SetDistanceReferencePoint(reference);
	}
	void Update(){
		int lon= spheres.Count;
		for(int i = 0; i< lon; i++)
		{
            BoundingSphere sphere = spheres[i];
            //spheres[i].position = cullingObj[i].position;
            if (cullingObj[i] != null) 
                sphere.position = cullingObj[i].position;
            spheres[i] = sphere;
            group.SetBoundingSpheres(spheres.ToArray());
            //Debug.Log(sphere.position);
            //spheres[i].position=cullingObj[i].position;
        }

	}
	protected override void OnStateChanged(CullingGroupEvent sphere)
	{
       // Debug.Log("OnStateChanged: " + sphere.index + " Visible: " + sphere.isVisible);
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
        spheres.Remove(npcCulling.spheres[index]);
        group.SetBoundingSpheres(spheres.ToArray());
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
