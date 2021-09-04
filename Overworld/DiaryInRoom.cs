using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Diary is moved next to the door only on day 1
public class DiaryInRoom : MonoBehaviour
{
    [SerializeField] Vector3 nextToDoor;

    private void Start() {
        if(GameManager.Instance.day == 1){
            transform.position = nextToDoor;
        }
    }
}
