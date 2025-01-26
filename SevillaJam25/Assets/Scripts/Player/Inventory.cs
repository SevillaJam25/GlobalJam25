using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<Object> objectsInventory;
    private Object selectedObject;
    private int inventoryIndex = 0;
    [SerializeField] public int inventorySize = 3;
    [SerializeField] public List<Text> texts;

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
            noStylizedStr = $"{selectedObject.objectName}";
        }
        this.texts[inventoryIndex].text = noStylizedStr;

        inventoryIndex = index;
        selectedObject = objectsInventory[inventoryIndex];
        string stylizedStr = $"<color=#cfba00>Empty</color>";
        if (selectedObject)
        {
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
        this.objectsInventory[inventoryIndex] = null;
        selectedObject.drop();
    }

    public void useObject()
    {
        if (!this.objectsInventory[inventoryIndex]) return;
        this.objectsInventory[inventoryIndex].action();
    }
}
