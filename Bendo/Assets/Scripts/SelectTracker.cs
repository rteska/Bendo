using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelectTracker : ScriptableObject
{
    [System.NonSerialized] public bool isItemSelected = false;
    [System.NonSerialized] public ItemData selectedItem = null;
}
