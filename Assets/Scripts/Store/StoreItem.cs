using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item", menuName ="Item/Create New Item")]
public class StoreItem : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite itemicon;
    public int level;
    public static string itemtag;
}
