using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusAdjustmentInformationNameSpace;

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
            case "Defense":
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
    

    public enum AdjustmentType
    {
        None,
        PowerUp,
        GetDefence,
    }

    public static void EnemypowerUp(EnemyBase enemy)
    {
        enemy.power += 10;
    }

}