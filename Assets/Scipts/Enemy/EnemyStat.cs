using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStat : MonoBehaviour
{
    public string Name;
    public int ID;
    public int Hp;
    public int HadMoney;
    public int Power;
    public int Defend;

    EnemyStat(string name, int id, int hp , int hadmoney, int power, int defend)
    { 
        Name = name;
        ID = id;
        Hp = hp;
        HadMoney = hadmoney;
        Power = power;
        Defend = defend;
    }
}
