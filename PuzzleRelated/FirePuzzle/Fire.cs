using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //Fire is destroyed if an object is moved over it
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Moveable"){
            Destroy(gameObject, 0.1f);
        }    
    }
}
