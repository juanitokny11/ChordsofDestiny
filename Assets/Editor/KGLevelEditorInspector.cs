using UnityEditor;
using UnityEngine;

namespace KGUtils
{
    [CustomEditor(typeof(KGLevelEditor))]
    public class KGLevelEditorInspector : Editor
    {
        bool m_CreateDecals=false;
        bool m_CreateFloor=false;
        bool m_CreateWall=false;
        LayerMask m_FloorLayer=9;

        private void Awake()
        {
            KGLevelEditor l_KGLevelEditor=(KGLevelEditor)target;
            if(l_KGLevelEditor.m_Floor.Count>0)
                l_KGLevelEditor.m_GetFloorDictionary=true;
        }
        void OnSceneGUI()
        {
            KGLevelEditor l_KGLevelEditor=(KGLevelEditor)target;

            if(m_CreateDecals)
            {
                Selection.activeGameObject=l_KGLevelEditor.gameObject;
                Event l_Event=Event.current;
                if(l_Event.isMouse && l_Event.type==EventType.MouseDown && l_Event.button==0)
                {
                    RaycastHit l_RaycastHit;
                    Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    if(Physics.Raycast(l_MouseRay, out l_RaycastHit, l_KGLevelEditor.m_DecalDistance, l_KGLevelEditor.m_DecalLayerMask))
                    {
                        l_KGLevelEditor.CreateDecal(l_RaycastHit.point, l_RaycastHit.normal);
                        Undo.RegisterCreatedObjectUndo(l_KGLevelEditor.m_DecalGameObject, "Created Decal");
                    }
                }
            }
            if(m_CreateFloor)
            {
                //l_KGLevelEditor.m_MouseReference= Instantiate(l_KGLevelEditor.m_WallBrushes[l_KGLevelEditor.m_SelectedFloorBrush].m_FloorPrefabs[l_KGLevelEditor.m_SelectedFloorInBrush]);
                //l_KGLevelEditor.m_MouseReference.transform.position=GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                Event l_Event=Event.current;
                Vector3 l_MousePosition=Event.current.mousePosition;
                //l_KGLevelEditor.AddFloor(l_MousePosition,l_KGLevelEditor.m_FloorBrushes[l_KGLevelEditor.m_SelectedFloorBrush].m_FloorPrefabs[l_KGLevelEditor.m_SelectedFloorInBrush]);
                if(l_KGLevelEditor.m_Floor.Count>0 && l_KGLevelEditor.m_GetFloorDictionary)
                {
                    l_KGLevelEditor.m_GetFloorDictionary=false;
                    l_KGLevelEditor.m_ActualizeFloor=true;
                }
                if(l_Event.type==EventType.KeyDown && l_Event.keyCode==KeyCode.D)
                {
                    Plane l_Plane=new Plane(Vector3.up, new Vector3(0.0f, l_KGLevelEditor.m_HeightFloor, 0.0f));
                    Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    float l_Enter=0.0f;
                    if(l_Plane.Raycast(l_MouseRay, out l_Enter))
                    {
                        //l_KGLevelEditor.CreateFloor(l_Hitpoint, l_KGLevelEditor.m_FloorBrushes[l_KGLevelEditor.m_SelectedFloorBrush].m_FloorPrefabs[l_KGLevelEditor.m_SelectedFloorInBrush]);
                        Undo.RegisterCreatedObjectUndo(l_KGLevelEditor.m_FloorInstance, "Created Floor");
                    }
                }
                if(l_Event.type==EventType.KeyDown && l_Event.keyCode==KeyCode.E)
                {
                    RaycastHit l_RaycastHit;

                    Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    if(Physics.Raycast(l_MouseRay, out l_RaycastHit, l_KGLevelEditor.m_DecalDistance, l_KGLevelEditor.m_DecalLayerMask))
                    {
                        l_KGLevelEditor.m_Floor.Remove(l_RaycastHit.collider.transform.parent.gameObject);
                        Undo.DestroyObjectImmediate(l_RaycastHit.collider.transform.parent.gameObject);
                    }
                }
            }
            if(m_CreateWall)
            {
                for(int i = 0; i < l_KGLevelEditor.m_Floor.Count; i++)
                {
                    //l_KGLevelEditor.DoWall(l_KGLevelEditor.m_Floor[i].transform.position,l_KGLevelEditor.m_WallBrushes[l_KGLevelEditor.m_SelectedWallBrush].m_WallPrefabs[l_KGLevelEditor.m_SelectedWallInBrush]);
                }
                m_CreateWall=!m_CreateWall;
                //l_KGLevelEditor.m_MouseReference= Instantiate(l_KGLevelEditor.m_WallBrushes[l_KGLevelEditor.m_SelectedWallBrush].m_WallPrefabs[l_KGLevelEditor.m_SelectedWallInBrush]);
                //l_KGLevelEditor.m_MouseReference.transform.position=GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                Event l_Event=Event.current;
                Vector3 l_MousePosition=Event.current.mousePosition;
                //l_KGLevelEditor.AddWall(l_MousePosition,l_KGLevelEditor.m_WallBrushes[l_KGLevelEditor.m_SelectedWallBrush].m_WallPrefabs[l_KGLevelEditor.m_SelectedWallInBrush]);
                if(l_KGLevelEditor.m_Walls.Count>0 && l_KGLevelEditor.m_GetWallDictionary)
                {
                    l_KGLevelEditor.m_GetWallDictionary=false;
                    l_KGLevelEditor.m_ActualizeWall=true;
                }
                if(l_Event.type==EventType.KeyDown && l_Event.keyCode==KeyCode.D)
                {
                    Plane l_Plane=new Plane(Vector3.up, new Vector3(0.0f, l_KGLevelEditor.m_HeightWall, 0.0f));
                    Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    float l_Enter=0.0f;
                    RaycastHit l_RaycastHit;
                    if(Physics.Raycast(l_MouseRay, out l_RaycastHit, l_KGLevelEditor.m_DecalDistance, m_FloorLayer))
                    {
                        if(l_Plane.Raycast(l_MouseRay, out l_Enter))
                        {
                            //l_KGLevelEditor.CreateWall(l_Hitpoint, l_KGLevelEditor.m_WallBrushes[l_KGLevelEditor.m_SelectedWallBrush].m_WallPrefabs[l_KGLevelEditor.m_SelectedWallInBrush]);
                            Undo.RegisterCreatedObjectUndo(l_KGLevelEditor.m_WallInstance, "Created Wall");
                     
                     }
                    }
                }
               if(l_Event.type==EventType.KeyDown && l_Event.keyCode==KeyCode.E)
                {
                    RaycastHit l_RaycastHit;

                    Ray l_MouseRay=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                    if(Physics.Raycast(l_MouseRay, out l_RaycastHit, l_KGLevelEditor.m_DecalDistance, l_KGLevelEditor.m_DecalLayerMask))
                    {
                        l_KGLevelEditor.m_Walls.Remove(l_RaycastHit.collider.transform.parent.gameObject);
                        Undo.DestroyObjectImmediate(l_RaycastHit.collider.transform.parent.gameObject);
                    }
              }
            }
            Selection.activeGameObject=l_KGLevelEditor.gameObject;
            /*l_KGLevelEditor.AddDecalBrushName();
            l_KGLevelEditor.AddFloorBrushName();
            l_KGLevelEditor.AddFloorName();*/
            //l_KGLevelEditor.AddWallBrushName();
            //l_KGLevelEditor.AddWallName();
        }
        public override void OnInspectorGUI()
        {
            KGLevelEditor l_KGLevelEditor=(KGLevelEditor)target;
            DrawDefaultInspector();
            l_KGLevelEditor.m_SelectedDecalBrush=EditorGUILayout.Popup("Selected Decal Brush:", l_KGLevelEditor.m_SelectedDecalBrush, l_KGLevelEditor.m_DecalBrushName.ToArray());
            if(GUILayout.Button("Create Decal: "+ m_CreateDecals))
                m_CreateDecals=!m_CreateDecals;
            /*l_KGLevelEditor.m_SelectedFloorBrush=EditorGUILayout.Popup("Selected Floor Brush:", l_KGLevelEditor.m_SelectedFloorBrush, l_KGLevelEditor.m_FloorBrushName.ToArray());
            l_KGLevelEditor.m_SelectedFloorInBrush=EditorGUILayout.Popup("Selected Floor In "+ l_KGLevelEditor.m_FloorBrushes[l_KGLevelEditor.m_SelectedFloorBrush].m_Name + " : ", l_KGLevelEditor.m_SelectedFloorInBrush, l_KGLevelEditor.m_FloorName.ToArray());
             if(GUILayout.Button("Create Floor: "+ m_CreateFloor))
                m_CreateFloor=!m_CreateFloor;
            l_KGLevelEditor.m_SelectedWallBrush=EditorGUILayout.Popup("Selected Wall Brush:", l_KGLevelEditor.m_SelectedWallBrush, l_KGLevelEditor.m_WallBrushName.ToArray());
            l_KGLevelEditor.m_SelectedWallInBrush=EditorGUILayout.Popup("Selected Wall In "+ l_KGLevelEditor.m_WallBrushes[l_KGLevelEditor.m_SelectedWallBrush].m_Name + " : ", l_KGLevelEditor.m_SelectedWallInBrush, l_KGLevelEditor.m_WallName.ToArray());
            if(GUILayout.Button("Do Wall: " + m_CreateWall))
                m_CreateWall=!m_CreateWall; 
            /*if(GUILayout.Button("Create Wall "+ m_CreateWall))
                m_CreateWall=!m_CreateWall;*/
           
        }
    }
}
