﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public enum CameraMode { Default, LockEnemy};
    public CameraMode mode;

    // Start is called before the first frame update
    public Transform target;
    Transform cameraTransform;
    float zoom;
    public bool lookToEnemy = false;
    float lockDistance;
    float hitZoom;
    float poscam;
    public float coneDistance = 0.8f;
    public GameObject[] enemies;
    public int currentEnemy = 0;
    [SerializeField] float defaultZoom = 6.0f;
    [SerializeField] float minZoom = 2.0f;
    [SerializeField] float maxZoom = 10.0f;
    [SerializeField] float offsetZoom = 4.0f;
    [SerializeField] float smoothZoom = 10.0f;
    [SerializeField] float smoothLockEnemy = 5.0f;
    [SerializeField] float zoomSensitivity = 5.0f;
    [SerializeField] float rotSensitivity = 5.0f;
    [SerializeField] float vertSensitivity = 5.0f;
    Vector3 currentLockPos;
    Ray[] ray;

    public LayerMask mask; 
    float hitDistance;

    void Start()
    {
        enemies = new GameObject[20];
        cameraTransform = this.transform;
        zoom = 6;//Mathf.Abs(cameraTransform.position.z - target.position.z);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        ray = new Ray[5];
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case CameraMode.Default:
                Zoom();
                RotateAround();
                VerticalMovement();
                CameraColisionSimple();
                break;
            case CameraMode.LockEnemy:
                LookToEnemy();
                VerticalMovementLockCamera();
                break;
            default:
                break;
        }
        if (Input.GetKeyDown(KeyCode.Q))
            ChangeEnemyToLook();
        if (Input.GetKeyDown(KeyCode.A))
        {
            lookToEnemy = !lookToEnemy;
            if (lookToEnemy) currentLockPos = cameraTransform.localPosition;
        }
        if (lookToEnemy == true)
            mode = CameraMode.LockEnemy;
        else
            mode = CameraMode.Default;
        
    }
    public void Zoom()
    {
        //float newZoom = zoom - Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Lerp(zoom, hitZoom, Time.deltaTime * smoothZoom);
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        Vector3 pos = cameraTransform.localPosition;
        pos.z = -zoom;

        cameraTransform.localPosition = pos;
    }

    public void RotateAround()
    {
        float axisX = Input.GetAxis("Mouse X") * rotSensitivity;
        //float axisY = Input.GetAxis("Mouse Y") * 5;
          Vector3 rot = target.localEulerAngles;

        rot.y += axisX;
        //rot.x -= axisY;

        target.localRotation = Quaternion.Euler(rot);
    }

    public void VerticalMovement()
    {
        float axisY = Input.GetAxis("Mouse Y") * vertSensitivity;
        
        Vector3 look = target.position;

        cameraTransform.LookAt(look);
        cameraTransform.Translate(Vector3.up * axisY * Time.deltaTime, Space.World);

        Vector3 pos = cameraTransform.localPosition;
        pos.y = Mathf.Clamp(pos.y, -1, 3);
        cameraTransform.localPosition = pos;
    }
    public void VerticalMovementLockCamera()
    {

        float axisY = Input.GetAxis("Mouse Y") * vertSensitivity;

        Vector3 look = target.position;

        if (lookToEnemy)
        {
            if (enemies.Length > 0)
            {
                look = enemies[currentEnemy].transform.position;
            }
        }

        cameraTransform.LookAt(look);
        cameraTransform.Translate(Vector3.up * axisY * Time.deltaTime, Space.World);

        Vector3 pos = cameraTransform.localPosition;
        pos.y = Mathf.Clamp(pos.y, -1, 3);
        cameraTransform.localPosition = pos;
    }

    public void CameraColisionSimple()
    {
        hitZoom = defaultZoom;
        RaycastHit hit;
        Vector3 dir = (cameraTransform.position - target.position).normalized;

        ray[0] = new Ray(target.position, dir);
        ray[1] = new Ray(target.position, cameraTransform.position + cameraTransform.up * coneDistance + cameraTransform.right * coneDistance);
        ray[2] = new Ray(target.position, cameraTransform.position + cameraTransform.up * coneDistance + -cameraTransform.right * coneDistance);
        ray[3] = new Ray(target.position, cameraTransform.position + -cameraTransform.up * coneDistance + cameraTransform.right * coneDistance);
        ray[4] = new Ray(target.position, cameraTransform.position + -cameraTransform.up * coneDistance + -cameraTransform.right * coneDistance);

        for(int i = 0; i < 5; i++)
        {
            if (Physics.Raycast(ray[i], out hit, defaultZoom, mask))
            {
                Debug.Log("Ray: " + i + " hit");
                
                //Vector3 DistanceCamera = new Vector3(hit.point.x,hit.point.y,hit.point.z);
                hitZoom = hit.distance - offsetZoom;

                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(target.position, cameraTransform.position - target.position);
        Gizmos.DrawRay(target.position, (cameraTransform.position + cameraTransform.up * coneDistance + cameraTransform.right * coneDistance) - target.position );
        Gizmos.DrawRay(target.position, (cameraTransform.position + cameraTransform.up * coneDistance + -cameraTransform.right * coneDistance) - target.position );
        Gizmos.DrawRay(target.position, (cameraTransform.position + -cameraTransform.up * coneDistance + cameraTransform.right * coneDistance) - target.position );
        Gizmos.DrawRay(target.position, (cameraTransform.position + -cameraTransform.up * coneDistance + -cameraTransform.right * coneDistance) - target.position );
    }
    public void LookToEnemy()
    {
        lockDistance = Mathf.Lerp(zoom, hitZoom, Time.deltaTime * smoothZoom);
        lockDistance = Mathf.Clamp(lockDistance, minZoom, maxZoom);

        Vector3 lockDirection = (enemies[currentEnemy].transform.position - target.transform.position).normalized;
        Vector3 newLockPos = new Vector3( target.transform.position.x - (lockDirection.x * lockDistance), cameraTransform.position.y, target.transform.position.z - (lockDirection.z * lockDistance));

        currentLockPos = Vector3.Lerp(currentLockPos, newLockPos, Time.deltaTime * smoothLockEnemy);

        
        cameraTransform.position = currentLockPos;

        Vector3 pos = cameraTransform.position;
        pos += cameraTransform.right * 1.5f;
        cameraTransform.position = pos;
    }
    public void ChangeEnemyToLook() 
    {
        currentEnemy++;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (currentEnemy > enemies.Length-1)
            currentEnemy = 0;
    }

}
