using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SA_enemy
{
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
