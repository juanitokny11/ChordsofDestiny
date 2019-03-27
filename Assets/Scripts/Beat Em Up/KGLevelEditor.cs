using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KGUtils
{ 
	public class KGLevelEditor: MonoBehaviour
	{
		[Header("Floor")]
       public List<GameObject> m_Floor;
		public float m_GridXFloor=2.0f;
		public float m_GridZFloor=2.0f;
		public float m_HeightFloor=0.0f;
		Dictionary<Vector3, GameObject> m_FloorElements;
		public GameObject m_FloorPrefab;
        [HideInInspector]public GameObject m_FloorInstance=null;
		[System.Serializable]
		public class CDecalBrush
		{
			public string m_Name;
			public List<GameObject> m_DecalPrefabs;
		}
		[HideInInspector]public int m_SelectedBrush;
		[HideInInspector]public List<string> m_BrushName;
       [HideInInspector]public GameObject m_DecalGameObject;
		[Header("Decals")]
		public List<CDecalBrush> m_DecalBrushes;
		public float m_DecalOffset=0.009f;
		public LayerMask m_DecalLayerMask=0xffff;
		public float m_DecalDistance=50.0f;
		
		public void AddBrushName()
		{
			m_BrushName=new List<string>();
			foreach(CDecalBrush brush in m_DecalBrushes)
				m_BrushName.Add(brush.m_Name);
		}
		bool AddFloor(Vector3 Position, GameObject Instance)
		{
			bool l_AddedFloor=false;
			if(m_FloorElements==null)
				m_FloorElements=new Dictionary<Vector3, GameObject>();
			if(m_FloorElements.ContainsKey(Position))
			{
				if(m_FloorElements[Position]==null)
				{ 
					m_FloorElements[Position]=Instance;
					l_AddedFloor=true;
				}
			}
			else
			{
				m_FloorElements.Add(Position, Instance);
				l_AddedFloor=true;
			}
			return l_AddedFloor;
		}
		bool CanCreateFloor(Vector3 Position)
		{
			if(m_FloorElements==null)
				m_FloorElements=new Dictionary<Vector3, GameObject>();
			if(m_FloorElements.ContainsKey(Position))
				return m_FloorElements[Position]==null;
			return true;
		}
		public GameObject CreateFloor(Vector3 Position, GameObject Prefab)
		{
			Vector3 l_Position=new Vector3(Position.x-Position.x%m_GridXFloor, m_HeightFloor, Position.z-Position.z%m_GridZFloor);
			if(CanCreateFloor(l_Position))
			{ 
				m_FloorInstance=Instantiate(Prefab,l_Position,Quaternion.identity);
				AddFloor(l_Position, m_FloorInstance);
				m_Floor.Add(m_FloorInstance);
			}
			//Debug.Log((Position.x-Position.x%m_GridXFloor)+" "+(Position.z-Position.z%m_GridZFloor));
			return m_FloorInstance;
		}
		public void DeleteFloor(Vector3 Position, GameObject Object)
		{
			Vector3 l_Position=new Vector3(Position.x-Position.x%m_GridXFloor, m_HeightFloor, Position.z-Position.z%m_GridZFloor);
			if(!CanCreateFloor(l_Position))
			{ 
				DestroyImmediate(Object);
				m_Floor.Remove(Object);
			}
		}
		public void CreateDecal(Vector3 Position, Vector3 Normal, Transform Parent)
		{
			m_DecalGameObject=Instantiate(m_DecalBrushes[m_SelectedBrush].m_DecalPrefabs[UnityEngine.Random.Range(0, m_DecalBrushes[m_SelectedBrush].m_DecalPrefabs.Count)], Position+Normal*m_DecalOffset, Quaternion.LookRotation(Normal), null);
			m_DecalGameObject.transform.parent=Parent;
		}
	}
}