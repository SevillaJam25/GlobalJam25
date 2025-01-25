using UnityEngine;

public class Object : MonoBehaviour
{
    private Vector3 initialPos;
    private Transform initialParent;
    public string objectName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        initialParent = transform.parent;
        initialPos = transform.position;
    }

    public void drop()
    {
        gameObject.tag = "Object";
        transform.position = initialPos;
        transform.SetParent(initialParent);
    }

    public void takeObject(Transform newParent)
    {
        gameObject.tag = "Selected";
        transform.position = newParent.position;
        transform.SetParent(newParent);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
