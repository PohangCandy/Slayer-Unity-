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
    
    float takedamagepercent;



    Renderer playerColor;
    private void Awake()
    {
        maxHp = 80;
        currenthp = maxHp;
    }
    // Start is called before the first frame update
    void Start()
    {
        drawPerTurn = 5;
        additionalDefense = 0;
        defense = 0;
        extraAttackStatus = 0;
        takedamagepercent = 1.0f;
        playerColor = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            takeDamage(10); Debug.Log("Pressed"); 
        }
        if (Input.GetKeyDown(KeyCode.I)) { defense -= 10; }
        if(getDefense() <= 0) { playerColor.material.color = Color.white; }
    }
    public void setDefense(int amount)
    {
        defense += amount+additionalDefense;
        Debug.Log("방어력 상승");
        playerColor.material.color = Color.gray;
    }
    public void takeDamage(float damage)
    {
        int temp = defense - (int)(damage * takedamagepercent);
        if(temp > 0) 
        {
            defense -= (int)(damage * takedamagepercent);
            return;
        }
        defense = 0;
        currenthp += temp;
    }

    public void pureHptakeDamage(float damage)
    {
        currenthp -= (int)(damage * takedamagepercent);
    }

    public void setAddtionalDefense(int amount)
    {
        additionalDefense+= amount;
    }

    public float getMaxHp() { return maxHp; }
    public float getCurrentHp() { return currenthp; }
    public int getDefense() { return defense; }
    public int getDrawPerturn() { return drawPerTurn; }
    public int getAttackstat() { return extraAttackStatus; }
    public void setAttackstat(int plus) { extraAttackStatus += plus; }
    public void healHp(int plus) 
    {
        if (currenthp + plus > maxHp)
        {
            currenthp = maxHp;
            return;
        }
        currenthp += plus;
    }
}
