using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Base : MonoBehaviour
{

    public event Action OnInventoryChange;
    public int maxInventorySize = 10;
    public List<Inventory_Item> itemList = new List<Inventory_Item>();


    protected virtual void Awake()
    {

    }


    public bool CanAddItem() => itemList.Count < maxInventorySize;
    public bool CanAddToStack(Inventory_Item itemToAdd)
    {
        List<Inventory_Item> stackableItems = itemList.FindAll(item => item.itemData == itemToAdd.itemData);

        foreach (var stack in stackableItems)
        {
            if (stack.CanAddStack())
                return true;
        }
        return false;
    }

    public void AddItem(Inventory_Item itemToAdd)
    {
        Inventory_Item itemInInventory = FindItem(itemToAdd.itemData);

        if (itemInInventory != null  && itemInInventory.CanAddStack())
            itemInInventory.AddStack();
        else
            itemList.Add(itemToAdd);

        OnInventoryChange?.Invoke();
    }
    
    public void RemoveItem(Inventory_Item itemToRemove)
    {
        itemList.Remove(FindItem(itemToRemove.itemData));
        OnInventoryChange?.Invoke();
    }

    public Inventory_Item FindItem(ItemDataSO itemData)
    {
        return itemList.Find(item => item.itemData == itemData);
    }
}
