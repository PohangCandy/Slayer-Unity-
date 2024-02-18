using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
//public class SA_UI_Data
//{

//    public SA_UI_Data(string _SA_Name, string _SA_Value, Texture2D _SAIconImg, bool _isExist)
//    {SA_Name = _SA_Name; SA_Value = _SA_Value; SAIconImg = _SAIconImg; isExist = _isExist; }

//    public string SA_Name;
//    string SA_Value;
//    public Texture2D SAIconImg;
//    public bool isExist;

//}
public class SA_UI : MonoBehaviour
{

    //public List<SA_UI_Data> AllSAIconList, CurIconList;
    public GameObject[] SA_UI_Obj;//, UsedItemImag;
    // Start is called before the first frame update
    private Animator SA_anim;
    void Start()
    {

        SA_anim = GetComponentInChildren<Animator>();
        for(int i = 0; i < SA_UI_Obj.Length ;i++)
        {
            SA_UI_Obj[i].SetActive(false);
        }
    }
    public void Set_UI_feail(int feailvalue)
    {
        SA_UI_Obj[0].SetActive(true);
        SA_UI_Obj[0].GetComponentInChildren<TextMeshProUGUI>().text = feailvalue.ToString();
    }
    public void Set_UI_strength(int powervalue)
    {
        SA_UI_Obj[2].SetActive(true);
        SA_UI_Obj[2].GetComponentInChildren<TextMeshProUGUI>().text = powervalue.ToString();
    }
    public void Set_UI_Weak(int weakvalue)
    {
        SA_UI_Obj[5].SetActive(true);
        SA_UI_Obj[5].GetComponentInChildren<TextMeshProUGUI>().text = weakvalue.ToString();
    }
    public void Set_UI_Vulnerable(int vnlnerablevalue)
    {
        
        SA_UI_Obj[4].SetActive(true);
        SA_anim.SetTrigger("SetSA");
        Debug.Log("here act");
        SA_UI_Obj[4].GetComponentInChildren<TextMeshProUGUI>().text = vnlnerablevalue.ToString();
    }
    public void Set_UI_Defense(int defensevalue, int lastturn)
    {
        if(lastturn == 1)
        {
            SA_UI_Obj[6].SetActive(true);
            //SA_anim.SetTrigger("SetSA");
            SA_UI_Obj[6].GetComponentInChildren<TextMeshProUGUI>().text = defensevalue.ToString();
        }
        else if (lastturn > 1)
        {   
            SA_anim.SetTrigger("SetSA");
            SA_UI_Obj[6].GetComponentInChildren<TextMeshProUGUI>().text = defensevalue.ToString();
        }
        else
        {
            //SA_anim.SetTrigger("SetSA");
            SA_UI_Obj[6].SetActive(false);
        }
    }

    void UIBlur()
    {

    }

}
