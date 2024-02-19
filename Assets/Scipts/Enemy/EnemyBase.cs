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
    private int VulnerableLastturn;
    private float WeakPercent_to_float;
    private int WeakLastturn;
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
        VulnerableLastturn = 0;
        WeakPercent_to_float = 1f;
        WeakLastturn = 0;
        DefenseValue = 0;
        PowerUpValue = 0;

        Maxhp = 100;
        Curhp = Maxhp;
        hptxt.text = Maxhp.ToString();
        power = 10;
        EnemyActionIcon.GetEnemyPower(power);
        slider.value = Maxhp;
        InintEnemyBehaviour();
        //ResetEnemyBehaviour();
    }

    void InintEnemyBehaviour()
    {
        RandomNumber = Random.Range(0, 99);
        PickNextActionWithPercentage(RandomNumber);
        EnemyTurnOver();
    }
    public int GetEnemyPower(int enemypower)
    {
        return enemypower;
    }


    public void ApplyVulnerable(float vulnerablepercentage,int lastturn) //damage * 1.5,2
    {
       
        SetVulnerablePercent(vulnerablepercentage, lastturn);
        EnemySA.Set_UI_Vulnerable(VulnerableLastturn);
    }
    void SetVulnerablePercent(float vulnerable, int lastturn)
    {
       
        if (VulnerableLastturn + lastturn > 0)
        {
            VulnerableLastturn += lastturn;
            VulnerablePercent_to_float = vulnerable;
            
        }
        else
        {
            VulnerableLastturn = 0;
            VulnerablePercent_to_float = 1f;
        }
        
    }

    float GetVunlerablePercent()
    {
        return VulnerablePercent_to_float;
    }

    public void ApplyWeak(float weakpercentage, int lastturn)
    {
        power = (int)(power * weakpercentage);
        EnemySA.Set_UI_Weak(lastturn);
    }

    public void SetWeak(int value) //for SA
    {
    }
    public void SetDefense(int value) //for SA
    {
        DefenseValue = 0;
    }
    public void SetDefense(int value, int lastturn) // for SA test
    {
        if(lastturn > 0)
        {
            DefenseValue += value;
        }
        DefenseValue = 0;
    }
    public void SetPowerUP(int value)
    {
        PowerUpValue += value;
        power += value;
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
            Curhp -= (int)(damage * GetVunlerablePercent());
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
        //몬스터의 다음 행동에 저장되어있는 이미지 가지고오기
        //다음 행동의 패턴 이미지 넣기
    }
    public void EnemyTurnStart()
    {
        Debug.Log("EnemytrunStart!");
        int Updateturn = turnManager.GetEnemyTurnCount();
        turnManager.StartCountEnemyTurn();
        UpdateEnemySA_Enemyturnvalue(Updateturn);
        Debug.Log("EnemytrunUpdate's " + Updateturn);
        SetCurAction();
    }

    public void EnemyTurnOver()
    {
        Debug.Log("EnemytrunOver!");
        int Updateturn = turnManager.GetPlayerTurnCount();
        turnManager.StartCountPlayerTurn();
        UpdateEnemySA_Playerturnvalue(Updateturn);
        Debug.Log("PlayertrunUpdate's " + Updateturn);
    }

    void UpdateEnemySA_Enemyturnvalue(int updateturn)
    {
        Debug.Log("Enemy's Updateturnvalue is " + updateturn);
        GetDown_DefenseLastTurn(-updateturn);
    }

    void UpdateEnemySA_Playerturnvalue(int updateturn)
    {
        Debug.Log("Player's Updateturnvalue is " + updateturn);
        GetDown_VulnerableLastTurn(-updateturn);
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
        //StatusAdjustment.EnemyGetDefense(this, 5);
        SA_enemy.t_EnemyGetDefense(this, 5 , 1); //(target, value, lastturn)
        EnemySA.Set_UI_Defense(5,1);//(value,lastturn)
        ResetEnemyBehaviour(); // Set Next Pattern
    }

    void GetDown_DefenseLastTurn(int updateturn)
    {
        SA_enemy.t_EnemyGetDefense(this, 0, updateturn); //(target, value, lastturn)
        EnemySA.Set_UI_Defense(0, updateturn);//(value,lastturn)
    }
    void GetDown_VulnerableLastTurn(int updateturn)
    {
        SA_enemy.t_EnemyGetVulnerable(this, updateturn); //(target, value, lastturn)
        //EnemySA.Set_UI_Vulnerable(0);//(lastturn)
    }
    void Do_DeBuff()
    {
        Debug.Log("DoDeuff" );
        //StatusAdjustment.PlayerGetVulnerable(this, 5);
        //StatusAdjustment.EnemyGetVulnerable(this, 5);//enemy get debuff by himself just for Test
        SA_enemy.t_EnemyGetVulnerable(this, 2);
        //EnemySA.Set_UI_Vulnerable(5);
        ResetEnemyBehaviour();
    }

    void get_Debuff()
    {
        Debug.Log("Buff");
        SA_enemy.t_EnemyGetVulnerable(this, 2);
        EnemySA.Set_UI_Vulnerable(2);
        ResetEnemyBehaviour();
    }
    void Do_Buff()
    {
        //StatusAdjustment.EnemyGetpowerUp(this,5,10);
        SA_enemy.t_EnemyGetpowerUp(this, 5, 10);
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

