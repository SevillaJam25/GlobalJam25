using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HandObject
{
    public ObjectsTypes type;
    public GameObject handObject;
}


public class Inventory : MonoBehaviour
{
    private List<Object> objectsInventory;
    private Object selectedObject;
    private int inventoryIndex = 0;
    [SerializeField] public int inventorySize = 3;
    [SerializeField] public List<Text> texts;
    [SerializeField] public List<HandObject> Hands;

    private ObjectUseManager objectUseManager;
    public void Awake()
    {
        this.objectUseManager = GetComponent<ObjectUseManager>();
        this.objectsInventory = new List<Object> { null, null, null };
    }
    private int? getFirstEmptyIndex()
    {
        for (int i = 0; i < 3; i++)
        {
            if (objectsInventory[i] == null)
            {
                return i;
            }
        }
        return null;  // Si no se encuentra ningún índice vacío
    }

    public GameObject searchHandGameObject(ObjectsTypes objType)
    {
        for (int i = 0; i < Hands.Count; i++)
        {
            if (Hands[i].type == objType)
            {
                return Hands[i].handObject;
            }
        }
        return null;
    }

    private bool addObject(Object obj)
    {
        var firstIndex = getFirstEmptyIndex();

        if (objectsInventory.Count < inventorySize || firstIndex != null)
        {
            objectsInventory[firstIndex.Value] = obj;
            obj.takeObject(transform);
            texts[firstIndex.Value].text = obj.objectName;
            setIndex(firstIndex.Value);
            return true;
        }
        else
        {
            Debug.Log("No hay espacio en inventario");
            return false;
        }
    }

    private void setIndex(int index)
    {
        objectUseManager.changedHand();
        selectedObject = objectsInventory[inventoryIndex];
        string noStylizedStr = $"Empty";
        if (selectedObject)
        {
            GameObject obj = searchHandGameObject(selectedObject.type);
            obj.SetActive(false);
            noStylizedStr = $"{selectedObject.objectName}";
        }
        this.texts[inventoryIndex].text = noStylizedStr;

        inventoryIndex = index;
        selectedObject = objectsInventory[inventoryIndex];
        string stylizedStr = $"<color=#cfba00>Empty</color>";
        if (selectedObject)
        {
            GameObject obj = searchHandGameObject(selectedObject.type);
            obj.SetActive(true);
            stylizedStr = $"<color=#cfba00>{selectedObject.objectName}</color>";
        }

        this.texts[inventoryIndex].text = stylizedStr;
    }
    public void changeInventoryIndex(bool increments)
    {
        if (increments)
        {
            if (inventoryIndex == inventorySize - 1) setIndex(0);
            else setIndex(inventoryIndex + 1);
        }
        else
        {
            if (inventoryIndex == 0) setIndex(inventorySize - 1);
            else setIndex(inventoryIndex - 1);
        }
    }

    public bool selectObject(Object obj)
    {
        return addObject(obj);
    }

    public void dropItem()
    {
        if (!this.objectsInventory[inventoryIndex]) return;
        this.texts[inventoryIndex].text = "Empty";
        searchHandGameObject(this.objectsInventory[inventoryIndex].type).SetActive(false);
        this.objectsInventory[inventoryIndex] = null;
        selectedObject.drop();
    }

    public void useObject()
    {
        if (!this.objectsInventory[inventoryIndex]) return;
        this.objectsInventory[inventoryIndex].action();
    }
}
