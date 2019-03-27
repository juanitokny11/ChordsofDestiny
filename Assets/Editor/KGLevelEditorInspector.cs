using UnityEditor;
using UnityEngine;

namespace KGUtils
{    
    [CustomEditor(typeof(KGLevelEditor))]
    public class KGLevelEditorInspector : Editor
    {
        bool m_CreateObjects=false;
        bool m_CreateFloor=false;
        readonly bool m_AddNamesToList=true;

        void OnSceneGUI()
        {
            KGLevelEditor l_KGLevelEditor=(KGLevelEditor)target;
            if(m_CreateObjects)
            {
                Selection.activeGameObject=l_KGLevelEditor.gameObject;
                Event l_Event=Event.current;
                if(l_Event.isMouse && l_Event.type==EventType.MouseDown && l_Event.button==0)
                {
                    RaycastHit l_RaycastHit;
                    Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    if(Physics.Raycast(l_MouseRay, out l_RaycastHit, l_KGLevelEditor.m_DecalDistance, l_KGLevelEditor.m_DecalLayerMask))
                    {
                        l_KGLevelEditor.CreateDecal(l_RaycastHit.point, l_RaycastHit.normal, l_RaycastHit.collider.transform);
                        Undo.RegisterCreatedObjectUndo (l_KGLevelEditor.m_DecalGameObject, "Created Floor");
                    }
                }
            }
            if(m_CreateFloor)
            {
                Event l_Event=Event.current;
                if(l_Event.type==EventType.KeyDown && l_Event.keyCode==KeyCode.D)
                {
                    Plane l_Plane=new Plane(Vector3.up, new Vector3(0.0f, l_KGLevelEditor.m_HeightFloor, 0.0f));
                    Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    float l_Enter=0.0f;

                    if(l_Plane.Raycast(l_MouseRay, out l_Enter))
                    {
                        Vector3 l_Hitpoint=l_MouseRay.GetPoint(l_Enter);
                        l_KGLevelEditor.CreateFloor(l_Hitpoint, l_KGLevelEditor.m_FloorPrefab);
                        Undo.RegisterCreatedObjectUndo (l_KGLevelEditor.m_FloorInstance, "Created Floor");
                    }
                }
                if(l_Event.type==EventType.KeyDown && l_Event.keyCode==KeyCode.E)
                {
                    RaycastHit l_RaycastHit;

                    Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    if(Physics.Raycast(l_MouseRay, out l_RaycastHit, l_KGLevelEditor.m_DecalDistance, l_KGLevelEditor.m_DecalLayerMask))
                    {
                        l_KGLevelEditor.DeleteFloor(l_RaycastHit.point,l_RaycastHit.collider.transform.parent.gameObject);
                        Undo.DestroyObjectImmediate(l_RaycastHit.collider.transform.parent.gameObject);
                    }
                }
            }
            if(m_AddNamesToList)
            {
                Selection.activeGameObject=l_KGLevelEditor.gameObject;
                l_KGLevelEditor.AddBrushName();
            }
        }
        public override void OnInspectorGUI()
        {
            KGLevelEditor l_KGLevelEditor=(KGLevelEditor)target;
            DrawDefaultInspector();
            l_KGLevelEditor.m_SelectedBrush=EditorGUILayout.Popup("Selected Brush:", l_KGLevelEditor.m_SelectedBrush, l_KGLevelEditor.m_BrushName.ToArray());
            if(GUILayout.Button("Create Decal "+ m_CreateObjects))
                m_CreateObjects=!m_CreateObjects;
            if(GUILayout.Button("Create Floor "+ m_CreateFloor))
                m_CreateFloor=!m_CreateFloor;
        }
    }
}
