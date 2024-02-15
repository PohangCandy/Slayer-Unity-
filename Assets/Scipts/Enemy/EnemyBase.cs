using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StatusAdjustmentInformationNameSpace;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    int Curhp;
    int Maxhp;
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
    public GameObject PowertxtObj;
    public TextMeshProUGUI hptxt;
    private TextMeshProUGUI powertext;
    private Animator EnemyAnim;


    enum EnemyPatternType { ChargeSkill, ReadyAttack };
    EnemyPatternType CurPattern;
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        PowertxtObj.SetActive(false);
        powertext = PowertxtObj.GetComponent<TextMeshProUGUI>();
        Maxhp = 100;
        Curhp = Maxhp;
        hptxt.text = Maxhp.ToString();
        power = 10;
        slider.value = Maxhp;
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
            
            GetAttack(10); }
    }

    public void GetAttack(int damage)
    {
        if (Curhp > 0)
        {
            Curhp -= damage;
            slider.value = Curhp;
            hptxt.text = Maxhp.ToString() + "/" + Curhp.ToString();
        }
    }

    public void SetPercentagetoValue(int randompercentage)
    {
        
        if (randompercentage < SkillPercentage)
        {
            SwitchNumber = 1;
        }
        else if(randompercentage < SkillPercentage + AttackPercentage)
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
                PowertxtObj.SetActive(true);
                powertext.text = power.ToString();
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

    public void GetNextAction()
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
        //StartCoroutine(ResetEnemyBehaviour());
    }
    void Skill()
    {
        EnemyAnim.SetTrigger("ChargingSkill");
        StatusAdjustment.EnemypowerUp(this);
    }

    void Attack()
    {
        EnemyAnim.SetTrigger("StartAttack");
        target.takeDamage(this.power);
    }

    public void ResetEnemyBehaviour()
    {
        RandomNumber = Random.Range(0, 99);
        PowertxtObj.SetActive(false);
        SetPercentagetoValue(RandomNumber);
        turnManager.EnemyTurnOver();
    }
}

