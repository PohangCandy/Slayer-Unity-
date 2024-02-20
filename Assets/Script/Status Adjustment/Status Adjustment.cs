using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusAdjustmentInformationNameSpace;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using TMPro;

public static class StatusAdjustment
{

    //public struct SAInformation
    //{
    //    public SAInformation(string _target, int _turn, string _category, int _damage)
    //    {
    //        target = _target;
    //        turn = _turn;
    //        category = _category;
    //        damage = _damage;
    //    }
    //    public string target;
    //    public int turn;
    //    public string category;
    //    public int damage;
    //}
    //Entity obj;
    //SAInformation sai;

    //StatusAdjustment(Entity _obj, _sai)
    //{
    //    Obj = obj;
    //    sai = _sai;
    //}
    

    static bool IsPlayer(Object targetObject) { return targetObject is Player; }
    static bool IsEnemy(Object targetObject) { return targetObject is EnemyBase; }

    public static void SetFunction(CardInterface card,Object targetObject,SAInformation sAInformation,Player player,bool onemore)
    {
        string[] effects = sAInformation.category.Split(' ');
        foreach (string word in effects)
        {
            switch (word)
            {
                case "attack":
                    {
                        Attack(targetObject, sAInformation,player);
                    }
                    break;
                case "defensestatup":
                    {                     
                        DefenseUP(targetObject, sAInformation);
                       
                    }
                    break;
                case "weakness":
                    {
                        Weakness(targetObject, sAInformation);
                    }
                    break;
                case "defense":
                    {
                        Defense(targetObject, sAInformation);
                    }
                    break;
                case "drawonce":
                    {
                        CardManager.Inst.DrawCard(1);
                    }
                    break;
                case "tempattackstatplus":
                    {
                        TempAttackStatPlus(targetObject, sAInformation);
                    }
                    break;
                case "discardonce":
                    {
                        CardManager.Inst.RandomDiscard(card);
                    }
                    break;
                case "playerhpminusone":
                    {
                        PlayerHpMinusOne(targetObject, sAInformation);
                    }
                    break;
                case "allenemydamage":
                    {
                        //Weaknesee(d, sAInformation);
                        AllEnemyDamage(sAInformation);
                    }
                    break;
                case "drawcardcount&putbackone":
                    {
                        //Weaknesee(d, sAInformation);
                        CardManager.Inst.DrawCard(sAInformation.amount);
                        
                    }
                    break;
            }
            if (!onemore) return;
        }
    }
    public static void SetFunction(PotionInterface potion, Object targetObject, SAInformation sAInformation)
    {
        string[] effects = sAInformation.category.Split(' ');
        foreach (string word in effects)
        {
            switch (word)
            {
                case "attackdamagenuff":
                    {
                        attackdamagenuff(sAInformation, targetObject);
                    }
                    break;
                case "slowrecovery":
                    {
                        slowrecovery(sAInformation,targetObject);
                    }
                    break;
                case "defense":
                    {
                        Defense(targetObject, sAInformation);
                    }break;
            }
           
        }
    }

    private static void slowrecovery(SAInformation sAInformation, Object targetObject)
    {
        if(IsPlayer(targetObject))
        {
            Player p=targetObject as Player;
            p.healHp(sAInformation.turn);
            if(sAInformation.turn <= 1) 
            { 
                p.playerSAUI.SA_UI_Obj[1].SetActive(false);
                return;
            }
            p.playerSAUI.SA_UI_Obj[1].SetActive(true);
            p.playerSAUI.SA_UI_Obj[1].GetComponentInChildren<TextMeshProUGUI>().text=sAInformation.turn.ToString();
        }
    }

    private static void attackdamagenuff(SAInformation sAInformation, Object targetObject)
    {
        SA_enemy.t_EnemyGetWeak(targetObject as EnemyBase, sAInformation.amount);
    }

    private static void AllEnemyDamage(SAInformation sAInformation)
    {
        GameObject[]enemys =GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            
            if (enemys[i].GetComponent<EnemyBase>()!=null)
                enemys[i].GetComponent<EnemyBase>().GetAttack(5);
        }
    }

    private static void PlayerHpMinusOne(Object targetObject, SAInformation sAInformation)
    {
        if (IsPlayer(targetObject))
        {
            Player player = targetObject as Player;
            player.pureHptakeDamage(5);
        }
    }

    private static void TempAttackStatPlus(Object targetObject, SAInformation sAInformation)
    {
        if (IsPlayer(targetObject))
        {
            if (sAInformation.turn > 0)
            {
                Player player = targetObject as Player;
                player.setAttackstat(2);
                player.playerSAUI.SA_UI_Obj[3].SetActive(true);
                player.playerSAUI.SA_UI_Obj[3].GetComponentInChildren<TextMeshProUGUI>().text = sAInformation.amount.ToString();
            }
            else
            {
                Player player = targetObject as Player;
                player.setAttackstat(-2);
                player.playerSAUI.SA_UI_Obj[3].SetActive(false);
            }
        }
    }

    private static void DefenseUP(Object targetObject, SAInformation sAInformation)
    {
        if (IsPlayer(targetObject))
        {
            Player player = targetObject as Player;
            player.setAddtionalDefense(sAInformation.amount);
            
        }
    }

    private static void Weakness(Object targetObject, SAInformation sAInformation)
    {
        if (IsEnemy(targetObject))
        {
            EnemyBase enemy = targetObject as EnemyBase;
            SA_enemy.t_EnemyGetVulnerable(enemy, 2);
            //enemy.SetWeak(50);
        }
    }

    static void Defense(Object targetObject, SAInformation sAInformation)
    {
        if (IsPlayer(targetObject))
        {
            Player player = targetObject as Player;
            player.setDefense(sAInformation.amount);
            player.playerSAUI.SA_UI_Obj[6].SetActive(true);
        }
    }

    private static void Attack(Object targetObject, SAInformation sAInformation, Player p)
    {
        if(IsEnemy(targetObject))
        {
            EnemyBase enemy=targetObject as EnemyBase;
            enemy.GetAttack((int)((sAInformation.amount+p.getAttackstat())*p.getattackdamagepercent()));
        }
    }

    //void sdf()
    //{
    //    switch(sai.category)
    //    {
    //        case "attack":
    //            {
    //                Debug.Log("공격");
    //            }break;
    //        case ""
    //    }//entity를 가져오지 않았다면, 카테고리를 통해서 함수를 선택할 수 있다. 그리고 이것을 플레이어,카드,적에게 적용하기위해서는 해당하는 것이 플레이어,적,카드에 있어야함. 
    //그러므로 앞에 객체를 가져와야한다. 이는 이벤트를 넘겨줄 때 해당하는 객체를 찾아서 넘거야한다는 뜻(이 객체들은 gameobject로 부터 나오기에 게임매니저에게 게임오브젝트를 관리하는 list를 넣어서 모든것을 관리하도록 시킨다.(포션,카드,플레이어,적)

    //}




    // 매개변수를 Object로 받으면..
    //-> 모든 매개변수를 object로 통일하면 무슨 값이든 다 받을 수 있지만 문제는
    //받은자료를 사용하기 위해선 캐스팅을 매번 해줘야 한다는 문제가 생긴다.

    //이를 해결하기 위해 c#에서는 템플릿 대신해서 제너릭 사용,
    //하지만 제너릭 사용 시 해당 클래스의 맴버 변수에 접근하지 못하는 문제 발생
    //-> 일반화된 타입의 파라미터에 해당 맴버 변수가 없기 때문
    // public static void EnemyGetpowerUp<T>(T Object,int Maxpower,int Minpower)
    //{
    //    Object.power += Random.Range(Minpower, Maxpower);
    //}
    //때문에 델리게이트를 이용해서 플레이어와 적을 위한 버프 작업 따로 진행

    public static int VulnerablePercentage = 50;
    public static int WeakPercentage = 25;

//public enum AdjustmentType
//    {
//        None,
//        PowerUp,
//        GetDefence,
//    }

//    delegate void EnemyDelegate(EnemyBase a, int min, int max);

    public static void EnemyGetpowerUp(EnemyBase enemy,int Maxpower,int Minpower)
    {
        int EnemyPowerUPValue = Random.Range(Minpower, Maxpower);
        enemy.ApplyPowerUP(EnemyPowerUPValue);
    }


    public static void EnemyGetWeak(EnemyBase enemy, int lastTurn)
    {
        enemy.SetWeak(WeakPercentage);
    }


    public static void EnemyGetVulnerable(EnemyBase enemy, int lastturn)
    {
        float VulnerablePercent_float = (float)(VulnerablePercentage / 100);
        enemy.ApplyVulnerable(VulnerablePercent_float,lastturn);
    }


    public static void EnemyGetDefense(EnemyBase enemy, int value)
    {
        enemy.SetDefense(value);
    }


}