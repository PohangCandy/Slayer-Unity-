using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusAdjustmentInformationNameSpace;
using UnityEngine.UIElements;

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
    static void reverse()
    {
        
    }

    static void Weaknesee()
    {

    }

    
    static void defense(Player game,SAInformation sAInformation)
    {
        game.setDefense(sAInformation.amount);
    }

    public static void SetFunction(Object gameObject,SAInformation sAInformation)
    {
        Player a = (Player)gameObject; //�� �ȳѾ��?
        
        switch (sAInformation.category)
        {
            case "attack":
                {
                    Debug.Log("����");
                }
                break;
            case "DefenseValue":
                {
                    Debug.Log("���� ���");
                    defense(a, sAInformation);
                }
                break;
            case "WEAKNESS":
                {
                    //Weaknesee(d, sAInformation);
                    
                }
                break;
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

public enum AdjustmentType
    {
        None,
        PowerUp,
        GetDefence,
    }

    delegate void EnemyDelegate(EnemyBase a, int min, int max);

    public static void EnemyGetpowerUp(EnemyBase enemy,int Maxpower,int Minpower)
    {
        int EnemyPowerUPValue = Random.Range(Minpower, Maxpower);
        enemy.SetPowerUP(EnemyPowerUPValue);
    }
    public static void Set_EnemySA_UI_powerUp(SA_UI enemyIcon, int value)
    {

    }


    public static void EnemyGetWeak(EnemyBase enemy, int lastTurn)
    {
        enemy.SetWeak(WeakPercentage);
    }

    public static void EnemyGetVulnerable(EnemyBase enemy, int lastTurn)
    {
        enemy.SetVulnerable(VulnerablePercentage);
    }

    public static void EnemyGetDefense(EnemyBase enemy, int value)
    {
        enemy.SetDefense(value);
    }

}