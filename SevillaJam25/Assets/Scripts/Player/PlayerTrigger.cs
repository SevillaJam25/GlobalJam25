using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Globalization;


public class PlayerTrigger : MonoBehaviour
{
    public delegate void OnSeaEnter();
    public static event OnSeaEnter onSeaEnter;
    public delegate void OnSeaLeave();
    public static event OnSeaLeave onSeaLeave;
    public delegate void OnTriggerEnterWithElement(string infoText);
    public static event OnTriggerEnterWithElement onTriggerEnterWithElement;
    public delegate void OnTriggerExitWithElement();
    public static event OnTriggerExitWithElement onTriggerExitWithElement;
    public static PlayerPosition playerPosition = PlayerPosition.BOAT;

    public delegate void OnEnterEMFArea(int area);
    public static event OnEnterEMFArea onEnterEMFArea;


    public static Object objectTriggered;
    public static bool ladderTriggered;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sea")
        {
            playerPosition = PlayerPosition.SEA;
            onSeaEnter.Invoke();
        }
        if (other.tag == "Object")
        {
            string stylizedStr = $"<color=#cfba00>{other.name}</color>";
            onTriggerEnterWithElement.Invoke("coger el objeto " + stylizedStr);
            objectTriggered = other.GetComponent<Object>();
        }
        if (other.tag == "Ladder")
        {
            ladderTriggered = true;
            string stylizedStr = $"<color=#cfba00>Subir hacia el barco</color>";
            onTriggerEnterWithElement.Invoke(stylizedStr);
        }

        if (other.tag == "Selected")
        {
            onTriggerExitWithElement.Invoke();
        }

        if (other.tag == "EMF")
        {
            onEnterEMFArea.Invoke(int.Parse(other.name));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sea")
        {
            playerPosition = PlayerPosition.BOAT;
            onSeaLeave.Invoke();
        }

        if (other.tag == "Object" || other.tag == "Ladder")
        {
            ladderTriggered = false;
            objectTriggered = null;
            onTriggerExitWithElement.Invoke();
        }
    }
}
