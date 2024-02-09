using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATQ_Stat : MonoBehaviour
{
    public string Name;
    public int ID;
    public int Cost;
    public string Description;
    public string Rarity;

    ATQ_Stat(string name, int id, int cost, string description, string rarity)
    {
        Name = name;
        ID = id;
        Cost = cost;
        Description = description;
        Rarity = rarity;
    }
}
