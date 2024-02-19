using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EnemyBase;

public class EnemyActionIcon : MonoBehaviour
{
    [SerializeField]
    public GameObject NextActionIcon;
    [SerializeField]
    public Texture2D[] NextAction;
    [SerializeField]
    public GameObject PowertxtObj;
    private TextMeshProUGUI powertext;
    int Enemypower;
    void Start()
    {
        //start�� ���ؼ� text ������ �ʱ�ȭ �����ַ��� �ߴ��� 
        //�θ� ��ü�� Start�Լ��� �����ڿ� �ش� �Լ��� ����Ǵ� �� �ϴ�.
        //�׷��� ���� �ʱ�ȭ�� �ϴ°� �ƴ� ���߿� �ʱ�ȭ�� �Ǹ鼭 ���� ���ư���. 
    }

    public void SetNextActionImage(EnemyBase.EnemyPatternType nextpattern)
    {
        switch (nextpattern)
        //ChargeDefense, ReadyAttack, ChargeVulnerable, ChargePower
        {
            case EnemyPatternType.ChargeDefense:
                PowertxtObj.SetActive(false);
                ChangeImage(NextAction[0]);
                //DefensetxtObj.SetActive(true);
                //Defensetext.text = Defense.ToString();
                break;
            case EnemyPatternType.ReadyAttack:
                ChangeImage(NextAction[1]);
                PowertxtObj.SetActive(true);
                //powertext.text = Enemypower.ToString();
                SetPowerTxt(Enemypower);
                break;
            case EnemyPatternType.ChargeVulnerable:
                PowertxtObj.SetActive(false);
                ChangeImage(NextAction[2]);
                break;
            case EnemyPatternType.ChargePower:
                PowertxtObj.SetActive(false);
                ChangeImage(NextAction[3]);
                break;

            default:
                Debug.Log("Wrong EnemyPattern is deliver to Action Icon");
                break;
        }
    }
    
    public void GetEnemyPower(int enemypower)
    {
        Enemypower = enemypower;
        SetPowerTxt(Enemypower);
    }

    void SetPowerTxt(int enemypower)
    {
        PowertxtObj.SetActive(true);
        powertext = PowertxtObj.GetComponent<TextMeshProUGUI>();
        powertext.text = enemypower.ToString();
    }
    void ChangeImage(Texture2D NextImage)
    {
        NextActionIcon.GetComponent<RawImage>().texture = NextImage;
        //NextAction.
        //������ ���� �ൿ�� ����Ǿ��ִ� �̹��� ���������
        //���� �ൿ�� ���� �̹��� �ֱ�
        //���� �ൿ�� ������ �� ���ݷ� ǥ���ϱ�
        //���� �ൿ�� ������ �� �̹��� ǥ���ϱ�
    }
}
