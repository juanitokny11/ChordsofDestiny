using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingGroupBase : MonoBehaviour {
		protected CullingGroup group;
		protected List<BoundingSphere> spheres;

		public Transform[] cullingObj;
		public Transform reference;
		public float[] distances;
	protected virtual void Start () 
	{
		group= new CullingGroup ();
        //reference = GameObject.FindGameObjectWithTag("MainCamera").transform;
		group.targetCamera = Camera.main;// asignar el grupo a una camara

		spheres= new List< BoundingSphere>();// creamos esferas para meter dentro objectos 

		int len =cullingObj.Length;
		if (len>100) len=100;
		for(int i = 0;i< len; i++)
		{
			spheres.Add(new BoundingSphere(cullingObj[i].position, 1.5f));// asignamos las esferas a los objectos que metamos en la array
		}

		group.SetBoundingSpheres(spheres.ToArray());// asignar las esferas a los objectos

		group.SetBoundingSphereCount(len);// asignamos el numero de esferas que vamos a usar

		//callback
		group.onStateChanged = OnStateChanged;
	}


	protected virtual void OnStateChanged(CullingGroupEvent sphere)
	{
		//cullingObj[sphere.index].gameObject.SetActive(sphere.isVisible);
		//.index te dice en que posicion de la array es 
	}
	private void OnDestroy()
	{
		group.Dispose();//Descagar grupo de la memoria
		group=null;//decir que el grupo esta vacio 
	}
	
}
