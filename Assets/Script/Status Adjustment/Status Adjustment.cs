using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusAdjustmentInformationNameSpace;
using UnityEngine.UIElements;
using Unity.VisualScripting;

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

    public static void SetFunction(Object targetObject,SAInformation sAInformation)
    {
        string[] effects = sAInformation.category.Split(' ');
        foreach (string word in effects)
        {
            switch (word)
            {
                case "attack":
                    {
                        Attack(targetObject, sAInformation);
                    }
                    break;
                case "defensestatup":
                    {
                        Debug.Log("���� ���");
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

                        CardManager.Inst.RandomDiscard();

                    }
                    break;
                case "playerhpminusone":
                    {
                        //Weaknesee(d, sAInformation);

                    }
                    break;
                case "allenemydamage":
                    {
                        //Weaknesee(d, sAInformation);

                    }
                    break;
                case "drawcardcount":
                    {
                        //Weaknesee(d, sAInformation);

                    }
                    break;
                case "putbackone":
                    {
                        //Weaknesee(d, sAInformation);

                    }
                    break;
            }
        }
    }

    private static void TempAttackStatPlus(Object targetObject, SAInformation sAInformation)
    {
        throw new System.NotImplementedException();
    }

    private static void DefenseUP(Object targetObject, SAInformation sAInformation)
    {
        throw new System.NotImplementedException();
    }

    private static void Weakness(Object targetObject, SAInformation sAInformation)
    {
        if (IsEnemy(targetObject))
        {
            EnemyBase enemy = targetObject as EnemyBase;
            enemy.SetWeak(50);
        }
    }

    static void Defense(Object targetObject, SAInformation sAInformation)
    {
         
        if (IsPlayer(targetObject))
        {
            Player player = targetObject as Player;
            player.setDefense(sAInformation.amount);
        }
    }

    private static void Attack(Object targetObject, SAInformation sAInformation)
    {
        if(IsEnemy(targetObject))
        {
            EnemyBase enemy=targetObject as EnemyBase;
            enemy.GetAttack(sAInformation.amount);
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
        enemy.SetPowerUP(EnemyPowerUPValue);
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