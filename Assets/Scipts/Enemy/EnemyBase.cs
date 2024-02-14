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
        //다음 행동의 패턴 이미지 넣기
        //다음 행동이 공격일 때 공격력 표시하기
        //다음 행동이 버프일 때 이미지 표시하기
    }

    void GetCurAction(EnemyPatternType curpattern)
    {
        //현재 적의 턴이라면, 지금 패턴 수행하기
        //적의 턴이 끝나면 다시 다음 행동 패턴 정하기
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
