using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    int defense;
    int additionalDefense;
    float maxHp;
    float currenthp;
    int gold;
    int drawPerTurn;
    //추가 공격력
    int extraAttackStatus;
    
    float takedamagepercent;
    float attackdamagepercent;
    public TextMeshProUGUI goldtext;
    public SA_UI playerSAUI;

    Renderer playerColor;
    private void Awake()
    {
        maxHp = 80;
        currenthp = maxHp;
    }
    // Start is called before the first frame update
    void Start()
    {
        gold = 50;
        drawPerTurn = 5;
        additionalDefense = 0;
        defense = 0;
        extraAttackStatus = 0;
        takedamagepercent = 1.0f;
        attackdamagepercent = 1.0f;
        playerColor = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        setGoldText();
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
    public void settakedamagepercent(float amount)
    {
        takedamagepercent=amount; 
    }

    public float gettakedamagepercent()
    {
        return takedamagepercent;
    }
    
    public void setattackdamagepercent(float amount)
    {
        attackdamagepercent = amount; 
    }

    public float getattackdamagepercent()
    {
        return attackdamagepercent;
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
    public void setGold(int amount) { gold+= amount; }
    public void setGoldText() { goldtext.text = gold.ToString(); }
    public int getGold() { return gold; }

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
