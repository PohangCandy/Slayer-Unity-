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
    private int DefenseLastturn;
    private int PowerUpValue;

    public enum EnemyPatternType { ChargeDefense, ReadyAttack, ChargeVulnerable, ChargePower};
    EnemyPatternType CurPattern;
    EnemyPatternType NextPattern;
    enum EnemyPatternPercent { ChargeDefensePercent = 20 , ReadyAttackPercent = 40, ChargeDeBuffPercent = 20, ChargePowerPercent = 20 };
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();
        VulnerablePercent_to_float = 1f;
        VulnerableLastturn = 0;
        WeakPercent_to_float = 1f;
        WeakLastturn = 0;
        DefenseValue = 0;
        DefenseLastturn = 0;
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
    public void ApplyWeak(float weakpercentage, int lastturn)
    {
        SetWeakPercent(weakpercentage, lastturn);
        power = (int)(power * weakpercentage);
        EnemyActionIcon.GetEnemyPower(power);
        EnemySA.Set_UI_Weak(WeakLastturn);
    }

    public void ApplyDefense(float defensevalue, int lastturn)
    {
        SetDefenseValue(defensevalue, lastturn);
        EnemySA.Set_UI_Defense((int)defensevalue,DefenseLastturn);
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

    void SetWeakPercent(float weak, int lastturn)
    {

        if (WeakLastturn + lastturn > 0)
        {
            WeakLastturn += lastturn;
            WeakPercent_to_float = weak;

        }
        else
        {
            WeakLastturn = 0;
            WeakPercent_to_float = 1f;
        }

    }

    void SetDefenseValue(float defensevalue, int lastturn)
    {

        if (DefenseLastturn + lastturn > 0)
        {
            DefenseValue += defensevalue;
            DefenseLastturn += lastturn;

        }
        else
        {
            DefenseLastturn = 0;
            DefenseValue = 0f;
        }

    }

    float GetVunlerablePercent()
    {
        return VulnerablePercent_to_float;
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
        if (DefenseValue > damage * GetVunlerablePercent())
        {
            DefenseValue -= (int)(damage * GetVunlerablePercent());
            EnemySA.Set_UI_Defense((int)DefenseValue, DefenseLastturn);
        }
        else
        {
            Debug.Log("before DefenseValue is " + DefenseValue);

            Curhp -= (int)((damage - DefenseValue) * GetVunlerablePercent());

            DefenseValue = 0;
            DefenseLastturn = 0;

            Debug.Log("after DefenseValue is " + DefenseValue);

            EnemySA.Set_UI_Defense((int)DefenseValue, DefenseLastturn);
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
            
            NextPattern = EnemyPatternType.ChargeVulnerable;
        }
        else if(randompercentage < (int)EnemyPatternPercent.ChargeDefensePercent + (int)EnemyPatternPercent.ReadyAttackPercent + 
            (int)EnemyPatternPercent.ChargeDeBuffPercent + (int)EnemyPatternPercent.ChargePowerPercent)
        {
           
            NextPattern = EnemyPatternType.ChargePower;
        }

        SetNextpattern();
    }

    void SetNextpattern()
    {
        switch(NextPattern)
        //ChargeDefense, ReadyAttack, ChargeVulnerable, ChargePower
        {
            case EnemyPatternType.ChargeDefense:
                SetNextActionUI(EnemyPatternType.ChargeDefense);               
                break;
            case EnemyPatternType.ReadyAttack:
                SetNextActionUI(EnemyPatternType.ReadyAttack);
                break;
            case EnemyPatternType.ChargeVulnerable:
                SetNextActionUI(EnemyPatternType.ChargeVulnerable);
                break;
            case EnemyPatternType.ChargePower:
                SetNextActionUI(EnemyPatternType.ChargePower);
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
        GetDown_WeakLastTurn(-updateturn);
    }

    void SetCurAction()
    {
        CurPattern = NextPattern;
        DoCurPattern(CurPattern);
    }
    void DoCurPattern(EnemyPatternType curpattern)
    {
        switch (curpattern)
        //ChargeDefense, ReadyAttack, ChargeVulnerable, ChargePower
        {
            case EnemyPatternType.ChargeDefense:
                EnemyAnim.SetTrigger("ChargingDefense");//Do_Defense() is in event at anim
                break;
            case EnemyPatternType.ReadyAttack:
                Do_Attack();
                break;
            case EnemyPatternType.ChargeVulnerable:
                EnemyAnim.SetTrigger("ChargingDeBuff");
                break;
            case EnemyPatternType.ChargePower:
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
        SA_enemy.t_EnemyGetDefense(this, (int)DefenseValue, updateturn); //(target, value, lastturn)
        EnemySA.Set_UI_Defense((int)DefenseValue, updateturn);//(value,lastturn)
    }
    void GetDown_VulnerableLastTurn(int updateturn)
    {
        SA_enemy.t_EnemyGetVulnerable(this, updateturn); //(target, value, lastturn)
        //EnemySA.Set_UI_Vulnerable(0);//(lastturn)
    }
    void GetDown_WeakLastTurn(int updateturn)
    {
        SA_enemy.t_EnemyGetWeak(this, updateturn); //(target, value, lastturn)
        //EnemySA.Set_UI_Vulnerable(0);//(lastturn)
    }
    void Do_DeBuff()
    {
        Debug.Log("DoDeuff" );
        //StatusAdjustment.PlayerGetVulnerable(this, 5);
        //StatusAdjustment.EnemyGetVulnerable(this, 5);//enemy get debuff by himself just for Test
        //SA_enemy.t_EnemyGetVulnerable(this, 2);
        //EnemySA.Set_UI_Vulnerable(5);
        target.settakedamagepercent(1.5f); // Set player get vulnerable
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

