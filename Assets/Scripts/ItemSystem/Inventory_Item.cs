using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    public ItemDataSO itemData;
    public int stackSize = 1;

    public Inventory_Item(ItemDataSO itemData)
    {
        this.itemData = itemData;
    }

    public bool CanAddToStack() => stackSize < itemData.maxStackSize;
    public void AddToStack() => stackSize++;
    public void RemoveFromStack() => stackSize--;

}
