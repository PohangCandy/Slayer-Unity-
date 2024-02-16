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
    private int power;
    [SerializeField]
    public Slider slider;
    [SerializeField]
    public TurnManager turnManager;
    [SerializeField]
    public Player target;
    public EnemyActionIcon EnemyActionIcon;
    public TextMeshProUGUI hptxt;
    private Animator EnemyAnim;
    private float VulnerableValue;
    private float WeakValue;
    private float DefenseValue;
    private float PowerUpValue;

    public enum EnemyPatternType { ChargeDefense, ReadyAttack, ChargeDeBuff, ChargeBuff};
    EnemyPatternType CurPattern;
    EnemyPatternType NextPattern;
    enum EnemyPatternPercent { ChargeDefensePercent = 20 , ReadyAttackPercent = 20, ChargeDeBuffPercent = 20, ChargeBuffPercent = 20 };
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
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
                SetNextAction(EnemyPatternType.ChargeDefense);               
                break;
            case EnemyPatternType.ReadyAttack:
                SetNextAction(EnemyPatternType.ReadyAttack);
                break;
            case EnemyPatternType.ChargeDeBuff:
                SetNextAction(EnemyPatternType.ChargeDeBuff);
                break;
            case EnemyPatternType.ChargeBuff:
                SetNextAction(EnemyPatternType.ChargeBuff);
                break;
            
            default:
                Debug.Log("Wrong PatternType in NextPattern");
                Debug.Log(SwitchNumber);
                break;
            }
    }
    void SetNextAction(EnemyPatternType nextpattern)
    {
        if(nextpattern == EnemyPatternType.ReadyAttack)
        {
            EnemyActionIcon.GetEnemyPower(power);
        }
        EnemyActionIcon.SetNextActionImage(nextpattern);
        //NextAction.
        //몬스터의 다음 행동에 저장되어있는 이미지 가지고오기
        //다음 행동의 패턴 이미지 넣기
    }

    public void SetCurAction()
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
                Do_Defense();
                break;
            case EnemyPatternType.ReadyAttack:
                Do_Attack();
                break;
            case EnemyPatternType.ChargeDeBuff:
                Do_DeBuff();
                break;
            case EnemyPatternType.ChargeBuff:
                Do_Buff();
                break;

            default:
                Debug.Log("Wrong PatternType in NextPattern");
                Debug.Log(SwitchNumber);
                break;
        }

    }

    void Do_Defense()
    {
        EnemyAnim.SetTrigger("ChargingSkill");
        StatusAdjustment.EnemyGetDefense(this, 5);
    }
    void Do_DeBuff()
    {
        EnemyAnim.SetTrigger("ChargingSkill");
        StatusAdjustment.EnemyGetVulnerable(this, 5);//enemy get debuff by himself just for Test
    }
    void Do_Buff()
    {
        EnemyAnim.SetTrigger("ChargingSkill");
        StatusAdjustment.EnemyGetpowerUp(this,5,10);
    }

    void Do_Attack()
    {
        EnemyAnim.SetTrigger("StartAttack");
        target.takeDamage((this.power + PowerUpValue) * WeakValue);
    }

    public void ResetEnemyBehaviour()
    {
        RandomNumber = Random.Range(0, 99);
        SetNextActionWithPercentage(RandomNumber);
        turnManager.EnemyTurnOver();
    }
}

