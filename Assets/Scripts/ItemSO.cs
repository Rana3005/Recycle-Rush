using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    // Scriptable object class
    [Header("Properties")]
    public float cooldown;
    public ItemType item_type;
    public bool stackable = true;

}

public enum ItemType {
    LiftTech,
    Key,
    GunEMP
};