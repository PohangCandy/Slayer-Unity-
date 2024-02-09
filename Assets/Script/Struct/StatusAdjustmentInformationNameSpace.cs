//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class StatusAdjustmentInformationNameSpace : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
namespace StatusAdjustmentInformationNameSpace
{
    public struct SAInformation
    {
        public SAInformation(string _target, int _turn, string _category, int _amount)
        {
            target = _target;
            turn = _turn;
            category = _category;
            amount = _amount;
        }
        public string target;
        public int turn;
        public string category;
        public int amount;
    }
}
