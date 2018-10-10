using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackAndSlashCam : MonoBehaviour {
public float RotateAmount = 10f;
public Transform pers;
public Transform rotationref;
    public GameObject camPos;
 public float distance = 10.0f;
     // the height we want the camera to be above the target
     public float height = 5.0f;
     // How much we 
     public float heightDamping = 2.0f;
     public float rotationDamping = 3.0f;
 
void LateUpdate()
{
	if (!pers)
             return;
  
   float wantedRotationAngle = pers.eulerAngles.y;
         float wantedHeight = pers.position.y + height;
         float currentRotationAngle = transform.eulerAngles.y;
         float currentHeight = transform.position.y;
     
         // Damp the rotation around the y-axis
         currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
 
         // Damp the height
         currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
 
         // Convert the angle into a rotation
         Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

		   OrbitCamera();
}

public void OrbitCamera()
{
 this.transform.forward=rotationref.transform.forward;
 this.transform.rotation=rotationref.transform.rotation;
  Vector3 target = pers.transform.position;
  float y_rotate = Input.GetAxis("Mouse X") * RotateAmount;
  float x_rotate = Input.GetAxis("Mouse Y") * RotateAmount;
  OrbitCamera(target, y_rotate, x_rotate);
}

public void OrbitCamera(Vector3 target, float y_rotate, float x_rotate)
{
 Vector3 angles = transform.eulerAngles;
 angles.z = 0;
 transform.eulerAngles = angles;
 transform.RotateAround(target, Vector3.up, y_rotate);
 transform.RotateAround(target, Vector3.zero, x_rotate);
 //this.transform.position = camPos.transform.position;
 transform.LookAt(target);
}
  
    
   
}
