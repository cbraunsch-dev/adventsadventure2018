using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Inventory {
    public int Money { get; private set; }
    public List<Item> Items { get; private set; }
    public int Collectibles { get; private set; }

    public Inventory() {
        this.Items = new List<Item>();
    }

    public void CollectMoney(int money) {
        this.Money += money;
    }

    public void SpendMoney(int money) {
        this.Money -= money;
    }

    public void FindCollectible() {
        this.Collectibles++;
    }

    public void BuyItem(Item item) {
        var itemCost = item.Cost;
		this.SpendMoney(itemCost);
        this.Items.Add(item);
    }
}
