using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatusAdjustmentInformationNameSpace;

public class EnemyBase : MonoBehaviour
{
    int hp;
    [SerializeField]
    public int power;
    [SerializeField]
    public Slider slider;
    [SerializeField]
    public TurnManager turnManager;
    [SerializeField]
    public Player target;
    void Start()
    {
        hp = 100;
        power = 10;
        slider.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("v");
            Attack(); }
    }

    public void Attack(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            slider.value = hp;
        }
    }

    public void Skill()
    {
        StatusAdjustment.EnemypowerUp(this);
    }

    public void Attack()
    {
        target.takeDamage(this.power);
    }
}
