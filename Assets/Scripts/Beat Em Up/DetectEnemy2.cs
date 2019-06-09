using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy2 : MonoBehaviour
{
    public Vector3 halfSize;
    public Vector3 offset;
    public LayerMask enemyMask;
    public GameObject boss;

    public bool TargetDetected { get; private set; }

    public void MyFixedUpdate()
    {
        TargetDetected = false;
        
        Collider[] hits = Physics.OverlapBox(transform.position + offset, halfSize, Quaternion.identity, enemyMask, QueryTriggerInteraction.Collide);
        if(hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].gameObject.CompareTag("Boss") && !boss.GetComponent<HealthScript>().characterDied)
                {
                    TargetDetected = true;
                    break;
                }
            }
        }
    }

    public  void SetOrientation(bool left)
    {
        /*if (left ) offset.z = 1;
        else offset.z = -1;*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + offset, halfSize * 2);
    }
}
