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
        Player a = (Player)gameObject; //왜 안넘어가지?
        switch (sAInformation.category)
        {
            case "attack":
                {
                    Debug.Log("공격");
                }
                break;
            case "Defense":
                {
                    Debug.Log("방어력 상승");
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
    //                Debug.Log("공격");
    //            }break;
    //        case ""
    //    }//entity를 가져오지 않았다면, 카테고리를 통해서 함수를 선택할 수 있다. 그리고 이것을 플레이어,카드,적에게 적용하기위해서는 해당하는 것이 플레이어,적,카드에 있어야함. 
    //그러므로 앞에 객체를 가져와야한다. 이는 이벤트를 넘겨줄 때 해당하는 객체를 찾아서 넘거야한다는 뜻(이 객체들은 gameobject로 부터 나오기에 게임매니저에게 게임오브젝트를 관리하는 list를 넣어서 모든것을 관리하도록 시킨다.(포션,카드,플레이어,적)

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