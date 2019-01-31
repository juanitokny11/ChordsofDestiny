using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    Transform cameraTransform;
    float zoom;
    float hitZoom;
    public float coneDistance = 0.8f;
    [SerializeField] float defaultZoom = 6.0f;
    [SerializeField] float minZoom = 2.0f;
    [SerializeField] float maxZoom = 10.0f;
    [SerializeField] float offsetZoom = 4.0f;
    [SerializeField] float smoothZoom = 10.0f;
    [SerializeField] float zoomSensitivity = 5.0f;
    [SerializeField] float rotSensitivity = 5.0f;
    [SerializeField] float vertSensitivity = 5.0f;

    Ray[] ray;

    public LayerMask mask; 
    float hitDistance;

    void Start()
    {
        cameraTransform = this.transform;
        zoom = 6;//Mathf.Abs(cameraTransform.position.z - target.position.z);

        ray = new Ray[5];
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        RotateAround();
        VerticalMovement();
        CameraColisionSimple();
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

        cameraTransform.Translate(Vector3.up * axisY * Time.deltaTime, Space.World);

        Vector3 pos = cameraTransform.localPosition;
        pos.y = Mathf.Clamp(pos.y, -1, 3);
        cameraTransform.localPosition = pos;

        Vector3 look = target.position;

        cameraTransform.LookAt(look);
        
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

}
