using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic inventory class of items
public class Inventory
{
    private List<ItemInformation> items;

    public Inventory(){
        items = new List<ItemInformation>();
    }

    //Sets the inventory to the contents of the given one
    public Inventory(Inventory copy){
        items = new List<ItemInformation>();
        foreach(ItemInformation item in copy.GetInventory()){
            items.Add(item);
        }
    }

    public List<ItemInformation> GetInventory(){
        return items;
    }

    public void AddItem(ItemInformation item){
        items.Add(item);
    }

    public void RemoveItem(ItemInformation item){
        items.Remove(item);
    }
}
