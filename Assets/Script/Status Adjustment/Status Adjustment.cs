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
        Player a = (Player)gameObject; //왜 안넘어가지?
        
        switch (sAInformation.category)
        {
            case "attack":
                {
                    Debug.Log("공격");
                }
                break;
            case "DefenseValue":
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