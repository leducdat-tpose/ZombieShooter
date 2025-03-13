using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Key,
}
[CreateAssetMenu(fileName ="ItemData", menuName ="ItemData/Item Data")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public ItemType Type;
    public Sprite Sprite;
}
