using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Static class with access to the player's inventory
public static class PlayerInventory
{
    static Inventory inventory;
    //Used for if the player dies in a puzzle or restarts it
    static Inventory savedInventory;

    public static void AddItem(ItemInformation item){
        inventory.AddItem(item);
    }

    public static void RemoveItem(ItemInformation item){
        inventory.RemoveItem(item);
    }

    public static List<ItemInformation> GetInventory(){
        return inventory.GetInventory();
    }

    public static void ClearInventory(){
        inventory = new Inventory();
        savedInventory = new Inventory();
    }
    //Reverts the player's inventory back to what it was at the beginning of the puzzle
    public static void Revert(){
        inventory = new Inventory(savedInventory);
    }

    //Save the player's inventory when they start a puzzle
    public static void SaveInventory(){
        savedInventory = new Inventory(inventory);
    }

}
