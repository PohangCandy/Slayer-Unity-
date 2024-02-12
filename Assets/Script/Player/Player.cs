using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Player : MonoBehaviour
{
    int defense;
    int additionalDefense;
    float maxHp;
    float currenthp;
    int drawPerTurn;
    //추가 공격력
    int extraAttackStatus;
    
    float givedamagepercent;
    float takedamagepercent;



    Renderer playerColor;
    // Start is called before the first frame update
    void Start()
    {
        drawPerTurn = 5;
        maxHp = 80;
        currenthp = maxHp;
        additionalDefense = 0;
        defense = 0;
        extraAttackStatus = 0;
        givedamagepercent = 1.0f;
        takedamagepercent = 1.0f;
        playerColor = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { currenthp -= 5; }
    }
    public void setDefense(int amount)
    {
        defense += amount+additionalDefense;
        Debug.Log("방어력 상승");
        playerColor.material.color = Color.gray;
    }
    public void takeDamage(float damage)
    {
        currenthp -= (int)(damage * takedamagepercent);
    }

    public void attack()
    {
        //takeDamage()
    }

    public float getMaxHp() { return maxHp; }
    public float getCurrentHp() { return currenthp; }
    public int getDefense() { return defense; }
}
