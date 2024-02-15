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
    private float VulnerableValue;
    private float WeakValue;
    private float DefenseValue;
    private float PowerUpValue;

    enum EnemyPatternType { ChargeDefense, ReadyAttack, ChargeDeBuff, ChargeBuff};
    EnemyPatternType CurPattern;
    EnemyPatternType NextPattern;
    public enum EnemyPatternPercent { ChargeDefensePercent = 20 , ReadyAttackPercent = 20, ChargeDeBuffPercent = 20, ChargeBuffPercent = 20 };
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        PowertxtObj.SetActive(false);
        powertext = PowertxtObj.GetComponent<TextMeshProUGUI>();
        VulnerableValue = 1f;
        WeakValue = 1f;
        DefenseValue = 0;
        PowerUpValue = 0;

        Maxhp = 100;
        Curhp = Maxhp;
        hptxt.text = Maxhp.ToString();
        power = 10;
        slider.value = Maxhp;
        RandomNumber = Random.Range(0, 99);
        SetNextActionWithPercentage(RandomNumber);
    }

    public void SetVulnerable(int vulnerablepercentage)
    {
        VulnerableValue = vulnerablepercentage / 100;
    }
    public void SetWeak(int WeakPercentage)
    {
        WeakValue = 1 - (WeakPercentage / 100);
    }

    public void SetDefense(int value)
    {
        DefenseValue += value;
    }
    public void SetPowerUP(int value)
    {
        PowerUpValue += value;
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
            Curhp -= (int)(damage * VulnerableValue);
            slider.value = Curhp;
            hptxt.text = Maxhp.ToString() + "/" + Curhp.ToString();
        }
    }

    public void SetNextActionWithPercentage(int randompercentage)
    {
        
        if (randompercentage < (int)EnemyPatternPercent.ChargeDefensePercent)
        {
            NextPattern = EnemyPatternType.ChargeDefense;
        }
        else if(randompercentage < (int)EnemyPatternPercent.ChargeDefensePercent + (int)EnemyPatternPercent.ReadyAttackPercent)
        {
            NextPattern = EnemyPatternType.ReadyAttack;
        }
        else if (randompercentage < (int)EnemyPatternPercent.ChargeDefensePercent + (int)EnemyPatternPercent.ReadyAttackPercent + 
            (int)EnemyPatternPercent.ChargeDeBuffPercent)
        {
            NextPattern = EnemyPatternType.ChargeDeBuff;
        }
        else if(randompercentage < (int)EnemyPatternPercent.ChargeDefensePercent + (int)EnemyPatternPercent.ReadyAttackPercent + 
            (int)EnemyPatternPercent.ChargeDeBuffPercent + (int)EnemyPatternPercent.ChargeBuffPercent)
        {
            NextPattern = EnemyPatternType.ChargeBuff;
        }

        pattern();
    }

    void pattern()
    {
        switch(NextPattern)
        //ChargeDefense, ReadyAttack, ChargeDeBuff, ChargeBuff
        {
            case EnemyPatternType.ChargeDefense:
                SetNextAction(EnemyPatternType.ReadyAttack, NextAction[1]);
                PowertxtObj.SetActive(true);
                powertext.text = power.ToString();
                this.CurPattern = EnemyPatternType.ReadyAttack;
                break;
            case EnemyPatternType.ReadyAttack:
                SetNextAction(EnemyPatternType.ReadyAttack, NextAction[1]);
                PowertxtObj.SetActive(true);
                powertext.text = power.ToString();
                this.CurPattern = EnemyPatternType.ReadyAttack;
                break;
            case EnemyPatternType.ChargeDeBuff:
                SetNextAction(EnemyPatternType.ChargeBuff, NextAction[0]);
                this.CurPattern = EnemyPatternType.ChargeBuff;
                break;
            case EnemyPatternType.ChargeBuff:
                SetNextAction(EnemyPatternType.ChargeBuff, NextAction[0]);
                this.CurPattern = EnemyPatternType.ChargeBuff;
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
        if(CurPattern == EnemyPatternType.ChargeBuff)
        {
            ChargeBuff();
        }
        else if(CurPattern == EnemyPatternType.ReadyAttack)
        {
            Attack();
        }
        //���� ���� ���̶��, ���� ���� �����ϱ�
        //���� ���� ������ �ٽ� ���� �ൿ ���� ���ϱ�
        //StartCoroutine(ResetEnemyBehaviour());
    }
    void ChargeBuff()
    {
        EnemyAnim.SetTrigger("ChargingSkill");
        StatusAdjustment.EnemyGetpowerUp(this,5,10);
    }

    void Attack()
    {
        EnemyAnim.SetTrigger("StartAttack");
        target.takeDamage((this.power + PowerUpValue) * WeakValue);
    }

    public void ResetEnemyBehaviour()
    {
        RandomNumber = Random.Range(0, 99);
        PowertxtObj.SetActive(false);
        SetNextActionWithPercentage(RandomNumber);
        turnManager.EnemyTurnOver();
    }
}

