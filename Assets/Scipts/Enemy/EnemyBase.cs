using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    int Curhp;
    int Maxhp;
    int RandomNumber;

    int SwitchNumber;
    [SerializeField]
    private int power;
    [SerializeField]
    public Slider slider;
    [SerializeField]
    public TurnManager turnManager;
    [SerializeField]
    public Player target;
    public EnemyActionIcon EnemyActionIcon;
    public SA_UI EnemySA;
    public TextMeshProUGUI hptxt;
    private Animator EnemyAnim;
    private float VulnerablePercent_to_float;
    private float VulnerableValue;
    private float WeakPercent_to_float;
    private float DefenseValue;
    private int PowerUpValue;

    public enum EnemyPatternType { ChargeDefense, ReadyAttack, ChargeDeBuff, ChargeBuff};
    EnemyPatternType CurPattern;
    EnemyPatternType NextPattern;
    enum EnemyPatternPercent { ChargeDefensePercent = 20 , ReadyAttackPercent = 40, ChargeDeBuffPercent = 20, ChargeBuffPercent = 20 };
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        VulnerablePercent_to_float = 1f;
        WeakPercent_to_float = 1f;
        DefenseValue = 0;
        PowerUpValue = 0;

        Maxhp = 100;
        Curhp = Maxhp;
        hptxt.text = Maxhp.ToString();
        power = 10;
        EnemyActionIcon.GetEnemyPower(power);
        slider.value = Maxhp;
        ResetEnemyBehaviour();
    }
    public int GetEnemyPower(int enemypower)
    {
        return enemypower;
    }

    public void SetVulnerable(int vulnerablepercentage)
    {
        VulnerablePercent_to_float = vulnerablepercentage / 100;
    }
    public void SetWeak(int WeakPercentage)
    {
        WeakPercent_to_float = 1 - (WeakPercentage / 100);
    }

    public void SetDefense(int value)
    {
        DefenseValue += value;
    }
    public void SetPowerUP(int value)
    {
        PowerUpValue = value;
        power += PowerUpValue;
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
            Curhp -= (int)(damage * VulnerablePercent_to_float);
            slider.value = Curhp;
            hptxt.text = Maxhp.ToString() + "/" + Curhp.ToString();
        }
    }

    public void PickNextActionWithPercentage(int randompercentage)
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

        SetNextpattern();
    }

    void SetNextpattern()
    {
        switch(NextPattern)
        //ChargeDefense, ReadyAttack, ChargeDeBuff, ChargeBuff
        {
            case EnemyPatternType.ChargeDefense:
                SetNextActionUI(EnemyPatternType.ChargeDefense);               
                break;
            case EnemyPatternType.ReadyAttack:
                SetNextActionUI(EnemyPatternType.ReadyAttack);
                break;
            case EnemyPatternType.ChargeDeBuff:
                SetNextActionUI(EnemyPatternType.ChargeDeBuff);
                break;
            case EnemyPatternType.ChargeBuff:
                SetNextActionUI(EnemyPatternType.ChargeBuff);
                break;
            
            default:
                Debug.Log("Wrong PatternType in NextPattern");
                Debug.Log(SwitchNumber);
                break;
            }
    }
    void SetNextActionUI(EnemyPatternType nextpattern)
    {
        if(nextpattern == EnemyPatternType.ReadyAttack)
        {
            EnemyActionIcon.GetEnemyPower(power);
        }
        EnemyActionIcon.SetNextActionImage(nextpattern);
        //NextAction.
        //������ ���� �ൿ�� ����Ǿ��ִ� �̹��� ���������
        //���� �ൿ�� ���� �̹��� �ֱ�
    }
    public void EnemyTurnStart()
    {
        SetCurAction();
    }
    void SetCurAction()
    {
        CurPattern = NextPattern;
        DoCurPattern(CurPattern);
    }
    void DoCurPattern(EnemyPatternType curpattern)
    {
        switch (curpattern)
        //ChargeDefense, ReadyAttack, ChargeDeBuff, ChargeBuff
        {
            case EnemyPatternType.ChargeDefense:
                EnemyAnim.SetTrigger("ChargingDefense");//Do_Defense() is in event at anim
                break;
            case EnemyPatternType.ReadyAttack:
                Do_Attack();
                break;
            case EnemyPatternType.ChargeDeBuff:
                EnemyAnim.SetTrigger("ChargingDeBuff");
                break;
            case EnemyPatternType.ChargeBuff:
                EnemyAnim.SetTrigger("ChargingBuff");
                break;

            default:
                Debug.Log("Wrong PatternType in NextPattern");
                Debug.Log(SwitchNumber);
                break;
        }

    }

    void Do_Defense()
    {
        //EnemyAnim.SetTrigger("ChargingSkill");
        StatusAdjustment.EnemyGetDefense(this, 5);
        EnemySA.Set_UI_Defense(5);
        ResetEnemyBehaviour(); // Set Next Pattern
    }
    void Do_DeBuff()
    {
        //EnemyAnim.SetTrigger("ChargingSkill");
        StatusAdjustment.EnemyGetVulnerable(this, 5);//enemy get debuff by himself just for Test
        EnemySA.Set_UI_Vulnerable(5);
        ResetEnemyBehaviour();
    }
    void Do_Buff()
    {
        //EnemyAnim.SetTrigger("ChargingSkill");
        StatusAdjustment.EnemyGetpowerUp(this,5,10);
        EnemySA.Set_UI_strength(PowerUpValue);
        ResetEnemyBehaviour();
    }

    void Do_Attack()
    {
        EnemyAnim.SetTrigger("StartAttack");
        target.takeDamage(this.power * WeakPercent_to_float);
    }

    public void ResetEnemyBehaviour()
    {
        RandomNumber = Random.Range(0, 99);
        PickNextActionWithPercentage(RandomNumber);
        turnManager.EnemyTurnOver();
    }
}

