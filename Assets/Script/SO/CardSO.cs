using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Card
{
    public Sprite sprite;
    public string name;
    public string description;
    public int rarity;
    public string targetTag;
    public string target;
    public string type;
    public int turn;
    public string category;
    public int amount;
    public int reinforcelevel;
    public int cost;
    public bool isdiscarding;
    public string mode;

    public Card(Sprite sprite, string name, string description, int rarity, string targetTag,string target,string type,int turn,string category,int amount,int reinforcelevel,int cost,bool isdiscarding,string mode)
    {
        this.sprite = sprite;
        this.name = name;
        this.description = description;
        this.rarity = rarity;
        this.targetTag = targetTag;            
        this.target = target;            
        this.type = type;
        this.turn = turn;         
        this.category = category;
        this.amount = amount;
        this.reinforcelevel = reinforcelevel;
        this.cost = cost;
        this.isdiscarding = isdiscarding;
        this.mode = mode;
    }

    //Card(CardInterface card) 
    //{
        
    //}
}
[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{

    public Card[] cards; 
}
