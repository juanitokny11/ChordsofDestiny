using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ToolScript))]
public class ToolScriptInspector : Editor
{
	public Vector3 m_MousePosition;
	
	bool m_CreateObjects=false;

	void OnSceneGUI()
	{
		ToolScript l_ToolScript=(ToolScript)target;
		
		if(m_CreateObjects)
		{
			Selection.activeGameObject=l_ToolScript.gameObject;
			Event l_Event=Event.current;
			if(l_Event.isMouse && l_Event.type==EventType.MouseDown && l_Event.button==0)
			{
				RaycastHit l_Hit;
				Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
				if(Physics.Raycast(l_MouseRay, out l_Hit, l_ToolScript.m_Distance, l_ToolScript.m_LayerMask))
					l_ToolScript.CreateObject(l_Hit.point, l_Hit.normal, l_Hit.collider.transform);
			}
		}
	}
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		if(GUILayout.Button("Create object on mouse click= "+ m_CreateObjects))
			m_CreateObjects=!m_CreateObjects;
	}
}
