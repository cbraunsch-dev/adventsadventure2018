using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreator {
    public static Item CreateItem(int itemIndex) {
        switch (itemIndex) {
            case 0:
                return new LuckyShroom();
            default:
                return new LuckyShroom();
        }
    }
}

public interface Item {
    int Cost { get; }

    void Use();
}

[Serializable]
public class LuckyShroom : Item
{
    public int Cost
    {
        get
        {
            return 5;
        }
    }

    public void Use()
    {
        throw new NotImplementedException();
    }
}
