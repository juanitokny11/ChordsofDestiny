using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KGUtils
{
    public class KGLevelEditor : MonoBehaviour
    {
        [Header("Floor")]
        public List<GameObject> m_Floor;
        public float m_GridXFloor=2.0f;
        public float m_GridZFloor=2.0f;
        public float m_HeightFloor=0.0f;
        public Dictionary<Vector3, GameObject> m_FloorElements;
        public bool m_RandomRotation=false;
        [HideInInspector]public bool m_ActualizeFloor=true;
        [HideInInspector]public bool m_GetFloorDictionary=true;
        [HideInInspector]public GameObject m_FloorInstance=null;
        [HideInInspector]public List<string> m_FloorName;
        [HideInInspector]public List<string> m_FloorBrushName;
        [HideInInspector]public int m_SelectedFloorInBrush;
        [HideInInspector]public int m_SelectedFloorBrush;
        public List<CFloorBrush> m_FloorBrushes;
        [System.Serializable]
        public class CFloorBrush
        {
            public string m_Name;
            public List<GameObject> m_FloorPrefabs;
        }

        [System.Serializable]
        public class CDecalBrush
        {
            public string m_Name;
            public List<GameObject> m_DecalPrefabs;
        }
        [Header("Walls")]
        public List<GameObject> m_Walls;
        public float m_GridXWall=2.0f;
        public float m_GridZWall=2.0f;
        public float m_HeightWall=0.0f;
        public Dictionary<Vector3, GameObject> m_WallElements;
        public List<CWallBrush> m_WallBrushes;
        [HideInInspector]public GameObject m_WallInstance=null;
        [HideInInspector]public List<string> m_WallName;
        [HideInInspector]public List<string> m_WallBrushName;
        [HideInInspector]public int m_SelectedWallInBrush;
        [HideInInspector]public int m_SelectedWallBrush;
        [HideInInspector]public bool m_ActualizeWall=true;
        [HideInInspector]public bool m_GetWallDictionary=true;

        [System.Serializable]
        public class CWallBrush
        {
            public string m_Name;
            public List<GameObject> m_WallPrefabs;
        }

        [Header("Decals")]
        public List<CDecalBrush> m_DecalBrushes;
        public float m_DecalOffset=0.009f;
        public LayerMask m_DecalLayerMask=0xffff;
        public float m_DecalDistance=50.0f;
        [HideInInspector]public List<string> m_DecalBrushName;
        [HideInInspector]public GameObject m_DecalGameObject;
        [HideInInspector]public int m_SelectedDecalBrush;

        int m_FloorRotation;
        int m_WallRotation;
        int m_RadomNumber;
        public int m_RotateWall;

     /*   public void AddDecalBrushName()
        {
            m_DecalBrushName=new List<string>();
            foreach(CDecalBrush brush in m_DecalBrushes)
                m_DecalBrushName.Add(brush.m_Name);
        }
        public void AddFloorName()
        {
            m_FloorName=new List<string>();
            foreach(GameObject floor in m_FloorBrushes[m_SelectedFloorBrush].m_FloorPrefabs)
            {
                m_FloorName.Add(floor.name);
            }
        }
        public void AddFloorBrushName()
        {
            m_FloorBrushName=new List<string>();
            foreach(CFloorBrush brush in m_FloorBrushes)
            {
                m_FloorBrushName.Add(brush.m_Name);
            }
        }
        public bool AddFloor(Vector3 Position, GameObject Instance)
        {
            m_RadomNumber=UnityEngine.Random.Range(0, 4);
            switch(m_RadomNumber)
            {
                case 0:
                    m_FloorRotation=0;
                    break;
                case 1:
                    m_FloorRotation=90;
                    break;
                case 2:
                    m_FloorRotation=180;
                    break;
                case 3:
                    m_FloorRotation=-90;
                    break;
            }
            bool l_AddedFloor=false;
            if(m_FloorElements==null)
            {
                m_FloorElements=new Dictionary<Vector3, GameObject>();
                foreach(GameObject floor in m_Floor)
                    m_FloorElements.Add(Position, floor); 
            }
            if(m_FloorElements.ContainsKey(Position))
            {
                if(m_FloorElements[Position]==null)
                {
                    m_FloorElements[Position]=Instance;
                    if(m_RandomRotation)
                        Instance.transform.GetChild(0).transform.localRotation=Quaternion.Euler(0, m_FloorRotation, 0);
                    l_AddedFloor=true;
                }
            }
            else
            {
                m_FloorElements.Add(Position, Instance);
                if(m_RandomRotation)
                    Instance.transform.GetChild(0).transform.localRotation=Quaternion.Euler(0, m_FloorRotation, 0);
                l_AddedFloor=true;
            }
            return l_AddedFloor;
        }
        void CheckDictionary()
        {
            if(m_FloorElements==null)
            {
                m_FloorElements=new Dictionary<Vector3, GameObject>();
                AddFloorElements();
            }
        }
        void AddFloorElements()
        {
            for(int i=0; i<m_Floor.Count; i++)
            {
                m_FloorElements.Add(m_Floor[i].transform.localPosition,m_Floor[i]);
                m_Floor[i].name=m_Floor[i].name;
            }
            m_ActualizeFloor=false;
        }
        bool CanCreateFloor(Vector3 Position)
        {
            if(m_FloorElements==null)
                m_FloorElements=new Dictionary<Vector3, GameObject>();
            if(m_ActualizeFloor)
            {
                m_ActualizeFloor=false;
                AddFloorElements();
            }
            if(m_FloorElements.ContainsKey(Position))
                return m_FloorElements[Position]==null;
            return true;
        }
        public GameObject CreateFloor(Vector3 Position, GameObject Prefab)
        {
            Vector3 l_Position=new Vector3(Position.x-Position.x%m_GridXFloor, m_HeightFloor, Position.z-Position.z%m_GridZFloor);
            if(CanCreateFloor(l_Position))
            {
                m_FloorInstance=Instantiate(Prefab, l_Position, Quaternion.identity);
                m_FloorInstance.name=m_FloorInstance.name + m_Floor.Count;
                AddFloor(l_Position, m_FloorInstance);
                m_Floor.Add(m_FloorInstance);
            }
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
        public void DoWall(Vector3 Position, GameObject Prefab)
        {
            CheckDictionary();
            if(!m_FloorElements.ContainsKey(Position+new Vector3(m_GridXFloor, 0.0f, 0.0f)))
            {
                m_RotateWall=1;
                CreateWall(Position+new Vector3(m_GridXFloor, 0.0f, 0.0f),Prefab);
            }
            if(!m_FloorElements.ContainsKey(Position+new Vector3(-m_GridXFloor, 0.0f, 0.0f)))
            {
                m_RotateWall=1;
                CreateWall(Position,Prefab);
            }
            if(!m_FloorElements.ContainsKey(Position+new Vector3(0.0f, 0.0f, m_GridZFloor)))
            {
                m_RotateWall=0;
                CreateWall(Position+new Vector3(0.0f, 0.0f, m_GridZFloor),Prefab);
            }
            if(!m_FloorElements.ContainsKey(Position+new Vector3(0.0f, 0.0f, -m_GridZFloor)))
            {
                m_RotateWall=0;
                CreateWall(Position,Prefab);
            }
        }
        /*public void AddWallName()
        {
            m_WallName=new List<string>();
            foreach(GameObject wall in m_WallBrushes[m_SelectedWallBrush].m_WallPrefabs)
            {
                m_WallName.Add(wall.name);
            }
        }
        public void AddWallBrushName()
        {
            m_WallBrushName=new List<string>();
            foreach(CWallBrush brush in m_WallBrushes)
            {
                m_WallBrushName.Add(brush.m_Name);
            }
        }*/
      /*  public bool AddWall(Vector3 Position, GameObject Instance)
        {
            switch(m_RotateWall)
            {
                case 0:
                    m_WallRotation = 0;
                    break;
                case 1:
                    m_WallRotation = -90;
                    break;
            }
            bool l_AddedWall=false;
            if(m_WallElements==null)
            {
                m_WallElements=new Dictionary<Vector3, GameObject>();
                foreach(GameObject wall in m_Walls)
                    m_WallElements.Add(Position, wall); 
            }
            if(m_WallElements.ContainsKey(Position))
            {
                if(m_WallElements[Position]==null)
                {
                    m_WallElements[Position]=Instance;
                    if(m_AutoRotation)
                    {
                        Instance.transform.localRotation=Quaternion.Euler(0,m_WallRotation,0);
                        Instance.transform.GetChild(0).transform.localRotation=Quaternion.Euler(90, 0, 0);
                    }
                    l_AddedWall=true;
                }
            }
            else
            {
                m_WallElements.Add(Position, Instance);
                if(m_AutoRotation)
                    {
                        Instance.transform.localRotation=Quaternion.Euler(0,m_WallRotation,0);
                        Instance.transform.GetChild(0).transform.localRotation=Quaternion.Euler(90, 0, 0);
                    }
                l_AddedWall=true;
            }
            return l_AddedWall;
        }
        void AddWallsElements()
        {
            for(int i = 0; i < m_Walls.Count; i++)
            {
                m_WallElements.Add(m_Walls[i].transform.localPosition,m_Walls[i]);
                m_Walls[i].name=m_Walls[i].name;
            }
            m_ActualizeWall=false;
        }
        bool CanCreateWall(Vector3 Position)
        {
            if(m_WallElements==null)
                m_WallElements=new Dictionary<Vector3, GameObject>();
            if(m_ActualizeWall)
            {
                m_ActualizeWall=false;
                AddWallsElements();
            }
            if(m_WallElements.ContainsKey(Position))
                return m_WallElements[Position]==null;
            return true;
        }
        public GameObject CreateWall(Vector3 Position, GameObject Prefab)
        {
            Vector3 l_Position=new Vector3(Position.x-Position.x%m_GridXFloor, m_HeightFloor, Position.z-Position.z%m_GridZFloor);
            //if(CanCreateWall(l_Position))
            //{
            m_WallInstance =Instantiate(Prefab, l_Position, Quaternion.identity);
            m_WallInstance.name=m_WallInstance.name + m_Walls.Count;
            AddWall(l_Position, m_WallInstance);
            m_Walls.Add(m_WallInstance);
            //}
            return m_WallInstance;
        }*/
        public void CreateDecal(Vector3 Position, Vector3 Normal)
        {
            m_DecalGameObject=Instantiate(m_DecalBrushes[m_SelectedDecalBrush].m_DecalPrefabs[UnityEngine.Random.Range(0, m_DecalBrushes[m_SelectedDecalBrush].m_DecalPrefabs.Count)], Position+Normal*m_DecalOffset, Quaternion.LookRotation(Normal));
            //m_DecalGameObject.transform.parent=Parent;
        }
    }
}