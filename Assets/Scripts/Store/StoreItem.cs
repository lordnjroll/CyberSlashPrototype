using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    public enum ItemType
    {
        Potion1,
        Potion2,
        PowerUp,
        SpeedUp,
        Shield
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Potion1:      return 10;
            case ItemType.Potion2:      return 50;
            case ItemType.PowerUp:      return 100;
            case ItemType.SpeedUp:      return 250;
            case ItemType.Shield:       return 500;
        }
    }

    internal static int GetCost(object itemType)
    {
        throw new NotImplementedException();
    }

    public static Sprite GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Potion1:      return GameAssets.i.s_Potion1;
            case ItemType.Potion2:      return GameAssets.i.s_Potion2;
            case ItemType.PowerUp:      return GameAssets.i.s_PowerUp;
            case ItemType.SpeedUp:      return GameAssets.i.s_SpeedUp;
            case ItemType.Shield:       return GameAssets.i.s_Shield;
        }
    }
}
