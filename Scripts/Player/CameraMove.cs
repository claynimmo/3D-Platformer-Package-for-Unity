using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class CameraMove : MonoBehaviour
{
    public PlayerInputs playerControls;

    private InputAction look;

    public float YMinAngle = -7f;

    private const float YMax = 80.0f;

    private float distanceOffset; //offset is subtracted from the distance
    private float offset; //offset obtained from the raycast. preserved so that distanceOffset can be smoothed
    public Vector3 cameraOffset = new Vector3(0,0.3f,0); //offset is added, so that the camera can look above the target and not directly at for better camera framing

    public float defaultCamDistance = 10.0f;
    public float minDistance = 2;


    public float xsensitivity = 4.0f;
    public float ysensitivity= 4.0f;

    public float followSmooth = 0.001f;
    public float lookSmooth = 0.0005f;

    

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float currentXLook;
    private float currentYLook;
    private float currentXIncrement;
    private float currentYIncrement;

    public float[] layerRenderDistances;
 
    public bool invertx;
    public bool inverty;

    public bool camMoveAble=true;

    Vector3 relativePos;
    public Transform target;

    public LayerMask rayLayerMask; 


    //variables for damping
    private float distanceRef = 0.0f;

    private Vector3 positionRef = Vector3.zero;
    private Vector3 lookRef = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().layerCullDistances = layerRenderDistances;
 
    }
    
    private void Awake(){
        playerControls = new PlayerInputs();
    }

    private void OnEnable(){
        look = playerControls.Player.Look;
        look.Enable();
    }

    private void OnDisable(){
        look.Disable();
    }

    void Update()
    {

        Vector2 lookVals = look.ReadValue<Vector2>();
        if(camMoveAble){
            currentXIncrement = lookVals.x * xsensitivity;
            currentYIncrement = lookVals.y * ysensitivity;
            currentX += currentXIncrement;
            currentY += currentYIncrement;
            currentXLook = lookVals.x;
            currentYLook = lookVals.y;

            currentY = Mathf.Clamp(currentY, YMinAngle, YMax);
        }

        relativePos = transform.position - (target.position);
        RaycastHit hit;
        if(Physics.SphereCast(target.position,0.15f, relativePos, out hit, defaultCamDistance + 0.5f,rayLayerMask))
        {   
            float angle = Vector3.Angle(relativePos, hit.normal);

            offset = defaultCamDistance - hit.distance;

            //move the camera forward from the normal angle to the wall to avoid viewport bleeding and clipping
            if(angle < 100){
                offset -= (1 - angle/100) * 2f;
            }
            
            offset = Mathf.Clamp(offset, 0, defaultCamDistance - minDistance);
            distanceOffset = Mathf.SmoothDamp(distanceOffset,offset,ref distanceRef,followSmooth);
        }
        else
        {
            distanceOffset = 0;
        }
    }
    void LateUpdate()
    {
        if(!camMoveAble){return;}

        
        float realDistance = defaultCamDistance - distanceOffset;

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        var position = rotation * new Vector3(0.0f, 0.0f, -realDistance) + target.position;

        transform.position = Vector3.SmoothDamp(transform.position, position, ref positionRef, lookSmooth);

        Vector3 rawLookTarget = target.position + cameraOffset;
        Vector3 smoothLookTarget = rawLookTarget;
        
        transform.LookAt(smoothLookTarget);
    }

    private void MinMovementLock(){
        RaycastHit hit;

        //raycast right
        if(Physics.Raycast(transform.position, transform.right, out hit, 0.1f, rayLayerMask)){
            Debug.Log(currentX);
            if(currentXLook < 0){
                currentX -= currentXIncrement;
                return;
            }
        }

        //raycast left
        if(Physics.Raycast(transform.position, -transform.right, out hit, 0.1f, rayLayerMask)){
            if(currentXLook > 0){
                currentX -= currentXIncrement;
                return;
            }
        }

    }
    

    public Vector3 GetCameraDirection(){
        Vector3 returnVal =  (target.position - transform.position);
        returnVal.Normalize();
        return returnVal;
    }
}