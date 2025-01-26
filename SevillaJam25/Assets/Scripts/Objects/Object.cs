using UnityEngine;

public class Object : MonoBehaviour
{
    private Vector3 initialPos;
    private Transform initialParent;
    public string objectName;
    [SerializeField] public ObjectsTypes type;

    public delegate void OnObjectUse(ObjectsTypes obj);
    public static event OnObjectUse onObjectUse;

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

    public void action()
    {
        onObjectUse.Invoke(type);
    }
}
