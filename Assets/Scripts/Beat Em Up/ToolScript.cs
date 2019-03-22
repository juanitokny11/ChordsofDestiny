using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolScript : MonoBehaviour
{
	public List<GameObject> m_Prefab;
	public float m_Offset=0.009f;
	public LayerMask m_LayerMask=0xffff;
	public float m_Distance=50f;
	public void CreateObject(Vector3 Position, Vector3 Normal, Transform Parent)
	{
		GameObject l_GameObject=Instantiate(m_Prefab[Random.Range(0,m_Prefab.Count)],Position+Normal*m_Offset, Quaternion.LookRotation(Normal), null);
		l_GameObject.transform.parent=Parent;
	}
}