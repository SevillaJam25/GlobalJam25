using UnityEngine;

public class Object : MonoBehaviour
{
    private Vector3 initialPos;
    private Transform initialParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        initialParent = transform.parent;
        initialPos = transform.position;
    }

    public void drop()
    {
        transform.position = initialPos;
        transform.SetParent(initialParent);
    }

    public void takeObject(Transform newParent)
    {
        transform.SetParent(newParent);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
