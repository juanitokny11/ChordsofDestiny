using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCulling : CullingGroupBase {



    private Animator[] anims;

    public List<NpcBevahavour> npcs;

	protected override void Start()
	{
		/*for(int i=0; i > cullingObj.Length; i++){
			cullingObj[i]= ;
		}*/
		base.Start();

		npcs=new List<NpcBevahavour>();
        int lon= cullingObj.Length;
		for(int i = 0; i< lon; i++)
		{
            //spheres[i].position = transform.position;
            BoundingSphere sphere = spheres[i];
            sphere.position = transform.position;
            spheres[i] = sphere;
			npcs.Add(cullingObj[i].GetComponent<NpcBevahavour>());
		}
		group.SetBoundingDistances(distances);
		group.SetDistanceReferencePoint(reference);
	}
	void Update(){
		int lon= cullingObj.Length;
		for(int i = 0; i< lon; i++)
		{
            BoundingSphere sphere = spheres[i];
            spheres.ToArray()[i].position = cullingObj[i].position;
            sphere.position = cullingObj[i].position;
            spheres[i] = sphere;
            Debug.Log(sphere.position);
            //spheres[i].position=cullingObj[i].position;
        }

	}
	protected override void OnStateChanged(CullingGroupEvent sphere)
	{
        if (!sphere.isVisible) npcs[sphere.index].HasBecomeInvisible();
        if (sphere.isVisible) npcs[sphere.index].HasBecomeVisible();
        
        if (sphere.currentDistance <= distances[0] ) npcs[sphere.index].Near();
        else if (sphere.currentDistance <= distances[1]) npcs[sphere.index].Far();
        else if (sphere.currentDistance <= distances[2]) npcs[sphere.index].ToFar();
        else if (sphere.currentDistance > distances[2] ) npcs[sphere.index].MuchFar();
        Debug.Log(sphere.currentDistance);
    }

    public void RemoveNPC(NpcBevahavour npc,BoundingSphere sphere)
    {
        //npcs.Remove(npc);
        int index = npcs.IndexOf(npc);
        npcs.RemoveAt(index);
        spheres.Remove(sphere);
    }
	
}
