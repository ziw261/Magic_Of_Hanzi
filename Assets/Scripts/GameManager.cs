using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive;

    public string[] itemsHeld;
    public int[] numberOfItems;

    // All items exists in this game
    public Item[] referenceItems;


    public int currentGold;

    // Start is called before the first frame update
    void Start() {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems();
    }

    // Update is called once per frame
    void Update() {
        if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive) {
            PlayerController.instance.canMove = false;

        }else {
            PlayerController.instance.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            AddItem("Iron Armor");
            AddItem("blabbla");

            RemoveItem("Health Potion");
            RemoveItem("AAHH");
        }


    }

    public Item GetItemDetails(string itemToGrab) {

        for(int i=0; i<referenceItems.Length; i++) {
            if(referenceItems[i].itemName == itemToGrab) {
                return referenceItems[i];
            }
        }


        return null;
    }

    public void SortItems() {

        bool itemAfterSpace = true;

        while (itemAfterSpace) {

            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++) {
                if (itemsHeld[i] == "") {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";


                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;


                    if(itemsHeld[i] != "") {
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd) {
        int newItemPosition = 0;
        bool foundSpace = false;

        for(int i=0; i<itemsHeld.Length; i++) {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd) {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
                //break;
            } 
        }

        if(foundSpace) {
            bool itemExists = false;
            for(int i=0; i<referenceItems.Length; i++) {
                if(referenceItems[i].itemName == itemToAdd) {
                    itemExists = true;
                    i = referenceItems.Length;
                }
            }

            if(itemExists) {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            } else {
                Debug.LogError(itemToAdd + " Does Not Exist!!");
            }
        }

        // update items
        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove) {
        bool foundItem = false;
        int itemPosition = 0;

        for(int i=0; i<itemsHeld.Length; i++) {
            if(itemsHeld[i] == itemToRemove) {
                foundItem = true;
                itemPosition = i;

                i = itemsHeld.Length;
            }
        }

        if(foundItem) {
            numberOfItems[itemPosition]--;

            if(numberOfItems[itemPosition] <= 0) {
                itemsHeld[itemPosition] = "";
            }

            GameMenu.instance.ShowItems();
        } else {
            Debug.LogError("Could not find " + itemToRemove);
        }
    }
}
