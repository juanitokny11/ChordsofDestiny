using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraCollision : MonoBehaviour {

    public bool lookenemy=false;
    public int numenemies=0;
    [Header("Camera Properties")]
    private float DistanceAway;                     //how far the camera is from the player.

    public float minDistance = 1;                //min camera distance
    public float maxDistance = 2;                //max camera distance

    public float DistanceUp = -2;                    //how high the camera is above the player
    public float smooth ;                    //how smooth the camera moves into place
    public float rotateAround = 70f;   
          //the angle at which you will rotate the camera (on an axis)

    [Header("Player to follow")]
    private Transform target;
    public Transform[] targets;
public Transform player;                     //the target the camera follows

    [Header("Layer(s) to include")]
    public LayerMask CamOcclusion;                //the layers that will be affected by collision

    [Header("Map coordinate script")]
    //    public worldVectorMap wvm;
    RaycastHit hit;
    float cameraHeight = 30f;
    float cameraPan = 0f;
    float camRotateSpeed = 180f;
    Vector3 camPosition;
    Vector3 camMask;
    Vector3 followMask;
   

    RaycastHit wallHit = new RaycastHit();

    public float HorizontalAxis;
    public float VerticalAxis;

    // Use this for initialization
    void Start()
    {
        //the statement below automatically positions the camera behind the target.
        rotateAround = player.eulerAngles.y - 45f;
        targets =new Transform[10];
       //player=FindObjectOfType<PlayerMovement>().transform;
    }
    void Update(){
    }
    void LateUpdate()
    {
        if(lookenemy==false){
        HorizontalAxis = Input.GetAxis("Mouse X");
        VerticalAxis = Input.GetAxis("Mouse Y");

        //Offset of the targets transform (Since the pivot point is usually at the feet).
        Vector3 targetOffset = new Vector3(player.position.x, (player.position.y + 2f), player.position.z);
        Quaternion rotation = Quaternion.Euler(cameraHeight, rotateAround, cameraPan);
        Vector3 vectorMask = Vector3.one;
        Vector3 rotateVector = rotation * vectorMask;
        //this determines where both the camera and it's mask will be.
        //the camMask is for forcing the camera to push away from walls.
        camPosition = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;
        camMask = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;

        occludeRay(ref targetOffset);
        smoothCamMethod();

        transform.LookAt(player);
        #region wrap the cam orbit rotation
        if (rotateAround > 360)
        {
            rotateAround = 0;
         
        }
        else if (rotateAround < 0)
        {
            rotateAround = rotateAround + 360 ;
          
        
        }
        #endregion 
        
        rotateAround += HorizontalAxis * camRotateSpeed * Time.deltaTime;
       
        DistanceAway = Mathf.Clamp(DistanceAway , minDistance, maxDistance);
        }else {
            LookAtEnemy();
           for (int i=0;i>10;i++){
            targets[i]=FindObjectOfType<EnemyBehaviour> ().transform;
            }
             player.LookAt(targets[0],Vector3.up);
        }

    }
    void smoothCamMethod()
    {

        smooth = 5f;
        transform.position = Vector3.Lerp(transform.position, camPosition, Time.deltaTime * smooth);
    }
    void occludeRay(ref Vector3 targetFollow)
    {
        #region prevent wall clipping
        //declare a new raycast hit.
        
        //linecast from your player (targetFollow) to your cameras mask (camMask) to find collisions.
        if (Physics.Linecast(targetFollow, camMask, out wallHit, CamOcclusion))
        {
            //the smooth is increased so you detect geometry collisions faster.
            smooth = 10f;
            //the x and z coordinates are pushed away from the wall by hit.normal.
            //the y coordinate stays the same.
            camPosition = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, camPosition.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
        #endregion
    }

    public void LookAtEnemy(){
        //target=FindObjectOfType<EnemyBehaviour> ().transform;
        lookenemy=true;
        //target.rotation = Quaternion.RotateTowards(target.rotation, target.rotation, 0.5f);
            
    }
     
}
