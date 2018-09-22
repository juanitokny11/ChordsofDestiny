using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickPath : MonoBehaviour
{
    public EnemyAI enemyAI;
    private Camera _cam;
    public LayerMask mask;

	// Use this for initialization
	void Start ()
    {
		_cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore))
            {
                Debug.Log(hit.transform.name);
                enemyAI.SetTarget(hit.point);
            }
        }

        
	}

}
