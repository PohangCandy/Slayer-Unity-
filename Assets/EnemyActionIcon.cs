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
        PowertxtObj.SetActive(false);
        Enemypower = 0;
        powertext = PowertxtObj.GetComponent<TextMeshProUGUI>();
    }

    public void SetNextActionImage(EnemyBase.EnemyPatternType nextpattern)
    {
        switch (nextpattern)
        //ChargeDefense, ReadyAttack, ChargeDeBuff, ChargeBuff
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
                Debug.Log("why null");
                //powertext.text = Enemypower.ToString();
                SetPowerTxt();
                Debug.Log("here?");
                break;
            case EnemyPatternType.ChargeDeBuff:
                PowertxtObj.SetActive(false);
                ChangeImage(NextAction[2]);
                break;
            case EnemyPatternType.ChargeBuff:
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
    }

    void SetPowerTxt()
    {
        PowertxtObj.SetActive(true);
        powertext.text = Enemypower.ToString();
    }
    void ChangeImage(Texture2D NextImage)
    {
        NextActionIcon.GetComponent<RawImage>().texture = NextImage;
        //NextAction.
        //몬스터의 다음 행동에 저장되어있는 이미지 가지고오기
        //다음 행동의 패턴 이미지 넣기
        //다음 행동이 공격일 때 공격력 표시하기
        //다음 행동이 버프일 때 이미지 표시하기
    }
}
