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

    public void Awake()
    {
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

    private void addObject(Object obj)
    {
        var firstIndex = getFirstEmptyIndex();

        if (objectsInventory.Count < inventorySize || firstIndex != null)
        {
            objectsInventory[firstIndex.Value] = obj;
            obj.takeObject(transform);
            texts[firstIndex.Value].text = obj.name;
        }
        else
        {
            Debug.Log("No hay espacio en inventario");
        }
    }

    private void setIndex(int index)
    {
        selectedObject = objectsInventory[inventoryIndex];
        string noStylizedStr = $"Empty";
        if (selectedObject)
        {
            noStylizedStr = $"{selectedObject.name}";
        }
        this.texts[inventoryIndex].text = noStylizedStr;

        inventoryIndex = index;
        selectedObject = objectsInventory[inventoryIndex];
        string stylizedStr = $"<color=#cfba00>Empty</color>";
        if (selectedObject)
        {
            stylizedStr = $"<color=#cfba00>{selectedObject.name}</color>";
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

    public void selectObject(Object obj)
    {
        addObject(obj);
    }

    public void dropItem()
    {
        this.texts[inventoryIndex].text = "Empty";
        this.objectsInventory[inventoryIndex] = null;
        selectedObject.drop();
    }
}
