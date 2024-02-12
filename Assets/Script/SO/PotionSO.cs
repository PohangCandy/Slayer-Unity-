using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Potion
{
    public Sprite sprite;
    public string name;
    public string description;
    public int rarity;
    public string targetType;
    public string target;
    public int turn;
    public string category;
    public int amount;
}
[CreateAssetMenu(fileName ="PotionSO",menuName ="Scriptable Object/PotionSO")]
public class PotionSO : ScriptableObject
{
    public Potion[] potions;
}
