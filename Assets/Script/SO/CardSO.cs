using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
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

}
[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{

    public Card[] cards; 
}
