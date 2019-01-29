using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    Transform cameraTransform;
    float zoom;
    [SerializeField] float minZoom = 2.0f;
    [SerializeField] float maxZoom = 10.0f;

    [SerializeField] float smoothZoom = 10.0f;
    [SerializeField] float zoomSensitivity = 5.0f;
    [SerializeField] float rotSensitivity = 5.0f;
    [SerializeField] float vertSensitivity = 5.0f;

    float lastZoom;
    float hitDistance;

    void Start()
    {
        cameraTransform = this.transform;
        zoom = Mathf.Abs(cameraTransform.position.z - target.position.z);
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
        float newZoom = zoom - Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        zoom = Mathf.Lerp(zoom, newZoom, Time.deltaTime * smoothZoom);

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
        RaycastHit hit;
        Vector3 dir = (cameraTransform.position - target.position).normalized;
        Ray ray = new Ray(target.position, dir);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("HIT");
            lastZoom = zoom;
            zoom = hit.distance;
        }
        else
        {
            //zoom = lastZoom;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(target.position, cameraTransform.position - target.position);
    }

}
