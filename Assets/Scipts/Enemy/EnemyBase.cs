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
    public GameObject NextActionIcon;
    [SerializeField]
    public TurnManager turnManager;
    [SerializeField]
    public Player target;
    public Sprite[] NextAction;
    
    enum EnemyPatternType { ChargeSkill, ReadyAttack };
    EnemyPatternType CurPattern;
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
        else if(randomNumber - AttackPercentage < 0)
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
                SetNextAction(EnemyPatternType.ChargeSkill, NextAction[0]);
                this.CurPattern = EnemyPatternType.ChargeSkill;
                break;
            case 2:
                SetNextAction(EnemyPatternType.ReadyAttack, NextAction[1]);
                this.CurPattern = EnemyPatternType.ReadyAttack;
                break;
            default:
                Debug.Log("Wrong Percentage Value in EnemyPattern");
                Debug.Log(SwitchNumber);
                break;
            }
    }
    void SetNextAction(EnemyPatternType nextpattern, Sprite NextImage)
    {
        NextActionIcon.GetComponent<SpriteRenderer>().sprite = NextImage;
        //NextAction.
        //������ ���� �ൿ�� ����Ǿ��ִ� �̹��� ���������
        //���� �ൿ�� ���� �̹��� �ֱ�
        //���� �ൿ�� ������ �� ���ݷ� ǥ���ϱ�
        //���� �ൿ�� ������ �� �̹��� ǥ���ϱ�
    }

    public void GetCurAction()
    {
        if(CurPattern == EnemyPatternType.ChargeSkill)
        {
            Skill();
        }
        else if(CurPattern == EnemyPatternType.ReadyAttack)
        {
            Attack();
        }
        //���� ���� ���̶��, ���� ���� �����ϱ�
        //���� ���� ������ �ٽ� ���� �ൿ ���� ���ϱ�
        RandomNumber = Random.Range(0, 99);
        SetPercentagetoValue(RandomNumber);
        turnManager.EnemyTurnOver();
    }
    void Skill()
    {
        StatusAdjustment.EnemypowerUp(this);
    }

    void Attack()
    {
        target.takeDamage(this.power);
    }
}
