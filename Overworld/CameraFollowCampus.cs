using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Camera's behavior as it follows Ky around on campus
public class CameraFollowCampus : MonoBehaviour
{
    [SerializeField] Transform target;
    //Borders that the camera can't pass
    [SerializeField] Transform leftBorder;
    [SerializeField] Transform rightBorder;
    [SerializeField] Transform downBorder;
    [SerializeField] Transform upBorder;
    float width;
    float height;

    private void Start() {
        //If Ky is on the outer 30% part of the screen, the camera starts moving with him
        //Camera's position is clamped according to bounds set from the borders
        width = GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect;
        height = GetComponent<Camera>().orthographicSize;
        Vector3 edgeBoundHor = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width * 0.7f,target.transform.position.y,transform.position.z));
        Vector3 edgeBoundVer = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(target.transform.position.x,Screen.height * 0.7f,transform.position.z));
        float difX = edgeBoundHor.x - transform.position.x;
        float difY = edgeBoundVer.y - transform.position.y;
        if(transform.position.x < leftBorder.position.x + width){
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBorder.position.x + width, rightBorder.position.x - width),
                                                        transform.position.y,transform.position.z); 
        }  
        else if(transform.position.x > rightBorder.position.x - width){
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBorder.position.x + width, rightBorder.position.x - width),
                                                        transform.position.y,transform.position.z); 
        }  
        if(transform.position.y < downBorder.position.y + height){
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 
                                            downBorder.position.y + height, upBorder.position.y - height),transform.position.z); 
        }  
        else if(transform.position.y > upBorder.position.y - height){
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 
                                            downBorder.position.y + height, upBorder.position.y - height),transform.position.z); 
        }  
    }
    private void LateUpdate() {
        //If Ky is on the outer 30% part of the screen, the camera starts moving with him
        //Camera's position is clamped according to bounds set from the borders
        Vector3 edgeBoundHor = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width * 0.7f,target.transform.position.y,transform.position.z));
        Vector3 edgeBoundVer = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(target.transform.position.x,Screen.height * 0.7f,transform.position.z));
        float difX = edgeBoundHor.x - transform.position.x;
        float difY = edgeBoundVer.y - transform.position.y;
        if(target.transform.position.x > transform.position.x + difX){
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + target.transform.position.x - (transform.position.x + difX), 
                                            leftBorder.position.x + width, rightBorder.position.x - width),transform.position.y,transform.position.z); 
        }  
        else if(target.transform.position.x < transform.position.x - difX){
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + target.transform.position.x - (transform.position.x - difX), 
                                            leftBorder.position.x + width, rightBorder.position.x - width),transform.position.y,transform.position.z); 
        }  
        if(target.transform.position.y > transform.position.y + difY){
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + target.transform.position.y - (transform.position.y + difY), 
                                            downBorder.position.y + height, upBorder.position.y - height),transform.position.z); 
        }  
        else if(target.transform.position.y < transform.position.y - difY){
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + target.transform.position.y - (transform.position.y - difY), 
                                            downBorder.position.y + height, upBorder.position.y - height),transform.position.z); 
        }  
    }

}
