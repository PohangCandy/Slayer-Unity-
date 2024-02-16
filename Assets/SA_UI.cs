using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
public class SA_UI_Data
{
    public String SA_Name;
    string SA_Value;
    public Texture2D SAIconImg;
    bool isExist = false;
    //public TextMeshProUGUI Txt_SA_UIValue;

}
public class SA_UI : MonoBehaviour
{

    public List<SA_UI_Data> AllSAIconList, CurIconList;
    public GameObject[] SA_UI_Obj;//, UsedItemImag;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SA_UI_Obj.Length ;i++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
