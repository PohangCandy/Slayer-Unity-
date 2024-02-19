using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SA_enemy
{
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

    public static int t_VulnerablePercentage = 50;
    public static int t_WeakPercentage = 25;
    public static int t_DefaultDefenseLaseturn = 1;

    //public enum AdjustmentType
    //    {
    //        None,
    //        PowerUp,
    //        GetDefence,
    //    }

    //    delegate void EnemyDelegate(EnemyBase a, int min, int max);

    public static void t_EnemyGetpowerUp(EnemyBase enemy, int Maxpower, int Minpower)
    {
        int EnemyPowerUPValue = Random.Range(Minpower, Maxpower);
        enemy.SetPowerUP(EnemyPowerUPValue);
    }


    public static void t_EnemyGetWeak(EnemyBase enemy, int lastTurn)
    {
        float WeakPercent_float = 1f - (t_WeakPercentage / 100f);//0.75
        enemy.ApplyWeak(WeakPercent_float, lastTurn);
    }


    public static void t_EnemyGetVulnerable(EnemyBase enemy, int lastturn)
    {
        float VulnerablePercent_float = (float)((t_VulnerablePercentage + 100f) / 100f);//0.5
        
        enemy.ApplyVulnerable(VulnerablePercent_float, lastturn);
    }


    public static void t_EnemyGetDefense(EnemyBase enemy, int value, int lastturn)
    {
        enemy.SetDefense(value, lastturn);
    }


}
