using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour {
    private int money;
    private List<Item> items = new List<Item>();

    public void CollectMoney(int money) {
        this.money += money;
    }

    public void SpendMoney(int money) {
        this.money -= money;
    }

    public void BuyItem(Item item) {
        var itemCost = item.Cost;
		this.SpendMoney(itemCost);
        this.items.Add(item);
        this.Print();
    }

    public void Print() {
        Debug.Log(">Inventory - Money: " + this.money + ". Nr. of items: " + this.items.Count + "<");
    }
}
