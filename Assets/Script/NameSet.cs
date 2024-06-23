using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameSet : MonoBehaviour
{ 
    [SerializeField]
    TextMeshProUGUI username;
    // Start is called before the first frame update
    void Start()
    {
        username.text = PlayerPrefs.GetString("user_name");
    }


}
