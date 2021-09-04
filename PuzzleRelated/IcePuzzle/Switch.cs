using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //List of tiles that this switch will turn off
    [SerializeField] List<GameObject> turnOff;
    public bool on;
    public List<GameObject> GetSwitchTiles(){
        return turnOff;
    }
}
