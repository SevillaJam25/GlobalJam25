using System.Collections;
using UnityEngine;

public class ObjectUseManager : MonoBehaviour
{
    public int current;
    private bool isUsedEMF = false;
    private bool isUsedLantern = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerTrigger.onEnterEMFArea += enterEMFArea;
        Object.onObjectUse += manageAction;
    }

    private void enterEMFArea(int area)
    {
        this.current = 1;
    }

    private IEnumerator EMF()
    {
        while (isUsedEMF)
        {
            yield return new WaitForSeconds(0.2f);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray); // Detecta todos los objetos en la trayectoria

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("EMF")) // Busca el primer objeto con la etiqueta "EMF"
                {
                    Debug.Log("Raycast impactó un objeto con tag EMF: " + hit.collider.gameObject.name);
                    break; // Termina el bucle al encontrar el primer objeto válido
                }
            }

        }
    }

    public void changedHand()
    {
        isUsedEMF = false;
        isUsedLantern = false;
    }
    private void manageAction(ObjectsTypes objType)
    {
        switch (objType)
        {
            case ObjectsTypes.LANTERN:
                isUsedLantern = !isUsedLantern;
                break;
            case ObjectsTypes.EMF:
                isUsedEMF = !isUsedEMF;
                StartCoroutine(EMF());
                break;
            case ObjectsTypes.VIAL:
                break;
            case ObjectsTypes.CRUCIFIX:
                break;
        }
    }
}
