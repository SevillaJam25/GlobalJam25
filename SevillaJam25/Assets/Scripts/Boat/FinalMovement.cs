using UnityEngine;

public class FinalMovement : MonoBehaviour
{

    bool isFinished;
    [SerializeField] Camera cam; 
    private void Start()
    {
        isFinished = false;
        cam.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isFinished = true; 
        }
        if (isFinished)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.forward * 2, Time.deltaTime);
            cam.enabled = true;
            Camera.main.gameObject.SetActive(false);
             
        }
    }
   
}
