using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour {
    private int money;

    public void CollectMoney(int money) {
        this.money += money;
    }

    public void SpendMoney(int money) {
        this.money -= money;
    }

    public void Print() {
        Debug.Log(">Inventory - Money: " + this.money + "<");
    }
}
