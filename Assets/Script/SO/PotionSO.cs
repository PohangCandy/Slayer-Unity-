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

    public Potion(Sprite sprite,string name,string description,int rarity,string targetType,string target,int turn,string category,int amount) { this.sprite = sprite;
        this.name = name;
        this.description = description;
        this.rarity = rarity;
        this.targetType = targetType;
        this.target = target;
        this.turn = turn;
        this.category = category;
        this.amount = amount;
    }
}
[CreateAssetMenu(fileName ="PotionSO",menuName ="Scriptable Object/PotionSO")]
public class PotionSO : ScriptableObject
{
    public Potion[] potions;
}
