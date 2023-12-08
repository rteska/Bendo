using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject associatedObj;
    public string usedFor; //"None" is passed if it isn't used for anything. Otherwise, Tag of object
    public string tag;
    [System.NonSerialized] public bool hasItem;
    [System.NonSerialized] public bool used;
    [System.NonSerialized] public bool selected;
    [TextArea(100, 500)]
    public string description;
}
