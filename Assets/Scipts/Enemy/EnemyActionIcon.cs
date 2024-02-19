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
        //start를 통해서 text 내용을 초기화 시켜주려고 했더니 
        //부모 객체의 Start함수가 끝난뒤에 해당 함수가 실행되는 듯 하다.
        //그래서 먼저 초기화를 하는게 아닌 나중에 초기화가 되면서 값이 날아간다. 
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
        //몬스터의 다음 행동에 저장되어있는 이미지 가지고오기
        //다음 행동의 패턴 이미지 넣기
        //다음 행동이 공격일 때 공격력 표시하기
        //다음 행동이 버프일 때 이미지 표시하기
    }
}
