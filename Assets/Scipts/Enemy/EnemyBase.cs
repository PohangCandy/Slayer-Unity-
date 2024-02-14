using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatusAdjustmentInformationNameSpace;

public class EnemyBase : MonoBehaviour
{
    int hp;
    int RandomNumber;
    int AttackPercentage;
    int SkillPercentage;

    int SwitchNumber;
    [SerializeField]
    public int power;
    [SerializeField]
    public Slider slider;
    [SerializeField]
    public TurnManager turnManager;
    [SerializeField]
    public Player target;
    enum EnemyPatternType { ChargeSkill, ReadyAttack };
    void Start()
    {
        
        hp = 100;
        power = 10;
        slider.value = hp;
        AttackPercentage = 60;
        SkillPercentage = 40;
        RandomNumber = Random.Range(0, 99);
        SetPercentagetoValue(RandomNumber);
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

    public void SetPercentagetoValue(int randomNumber)
    {
        
        if (randomNumber - SkillPercentage < 0)
        {
            SwitchNumber = 1;
        }
        else if(randomNumber - AttackPercentage >= 0)
        {
            SwitchNumber = 2;
        }
        pattern();
    }

    void pattern()
    {
        switch(SwitchNumber)
        {
            case 1:
                SetNextAction(EnemyPatternType.ChargeSkill);
                break;
            case 2:
                SetNextAction(EnemyPatternType.ReadyAttack);
                break;
            default:
                Debug.Log("Wrong Percentage Value in EnemyPattern");
                break;
            }
    }
    void SetNextAction(EnemyPatternType nextpattern)
    {
        //���� �ൿ�� ���� �̹��� �ֱ�
        //���� �ൿ�� ������ �� ���ݷ� ǥ���ϱ�
        //���� �ൿ�� ������ �� �̹��� ǥ���ϱ�
    }

    void GetCurAction(EnemyPatternType curpattern)
    {
        //���� ���� ���̶��, ���� ���� �����ϱ�
        //���� ���� ������ �ٽ� ���� �ൿ ���� ���ϱ�
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
