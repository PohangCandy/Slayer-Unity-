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
    //                Debug.Log("����");
    //            }break;
    //        case ""
    //    }//entity�� �������� �ʾҴٸ�, ī�װ��� ���ؼ� �Լ��� ������ �� �ִ�. �׸��� �̰��� �÷��̾�,ī��,������ �����ϱ����ؼ��� �ش��ϴ� ���� �÷��̾�,��,ī�忡 �־����. 
    //�׷��Ƿ� �տ� ��ü�� �����;��Ѵ�. �̴� �̺�Ʈ�� �Ѱ��� �� �ش��ϴ� ��ü�� ã�Ƽ� �Ѱž��Ѵٴ� ��(�� ��ü���� gameobject�� ���� �����⿡ ���ӸŴ������� ���ӿ�����Ʈ�� �����ϴ� list�� �־ ������ �����ϵ��� ��Ų��.(����,ī��,�÷��̾�,��)

    //}




    // �Ű������� Object�� ������..
    //-> ��� �Ű������� object�� �����ϸ� ���� ���̵� �� ���� �� ������ ������
    //�����ڷḦ ����ϱ� ���ؼ� ĳ������ �Ź� ����� �Ѵٴ� ������ �����.

    //�̸� �ذ��ϱ� ���� c#������ ���ø� ����ؼ� ���ʸ� ���,
    //������ ���ʸ� ��� �� �ش� Ŭ������ �ɹ� ������ �������� ���ϴ� ���� �߻�
    //-> �Ϲ�ȭ�� Ÿ���� �Ķ���Ϳ� �ش� �ɹ� ������ ���� ����
    // public static void EnemyGetpowerUp<T>(T Object,int Maxpower,int Minpower)
    //{
    //    Object.power += Random.Range(Minpower, Maxpower);
    //}
    //������ ��������Ʈ�� �̿��ؼ� �÷��̾�� ���� ���� ���� �۾� ���� ����

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