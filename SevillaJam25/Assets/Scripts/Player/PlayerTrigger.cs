using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public class PlayerTrigger : MonoBehaviour
{
    public delegate void OnSeaEnter ();  
    public static event OnSeaEnter onSeaEnter;  
    public delegate void OnSeaLeave ();  
    public static event OnSeaLeave onSeaLeave;
    public static PlayerPosition playerPosition = PlayerPosition.BOAT;  

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Sea") {
            playerPosition = PlayerPosition.SEA;
            onSeaEnter.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Sea") {
            playerPosition = PlayerPosition.BOAT;
            onSeaLeave.Invoke();
        }
    }
}
