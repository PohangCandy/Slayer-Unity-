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

    public static int VulnerablePercentage = 50;
    public static int WeakPercentage = 25;

    //public enum AdjustmentType
    //    {
    //        None,
    //        PowerUp,
    //        GetDefence,
    //    }

    //    delegate void EnemyDelegate(EnemyBase a, int min, int max);

    public static void EnemyGetpowerUp(EnemyBase enemy, int Maxpower, int Minpower)
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
        enemy.ApplyVulnerable(VulnerablePercent_float, lastturn);
    }


    public static void EnemyGetDefense(EnemyBase enemy, int value)
    {
        enemy.SetDefense(value);
    }


}
