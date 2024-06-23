using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegisterManager :MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] TMP_InputField userId;
    [SerializeField] TMP_InputField password;
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_InputField Name;
    // Update is called once per frame

    public async void OnRegisterPressed()
    {
        if (string.IsNullOrWhiteSpace(userId.text))
        {
            Debug.LogError("Please enter a valid id");
            return;
        }

        if (string.IsNullOrWhiteSpace(password.text))
        {
            Debug.LogError("Please enter a valid password");
            return;
        }
        if (string.IsNullOrWhiteSpace(email.text))
        {
            Debug.LogError("Please enter a valid email");
            return;
        }
        if (string.IsNullOrWhiteSpace(Name.text))
        {
            Debug.LogError("Please enter a valid name");
            return;
        }
        if (await MySqlManager.RegisterUser(userId.text, email.text, password.text, Name.text))
        {
            Debug.Log("등록완료");
        }
        else
        {
            Debug.Log("같은 아이디가 이미 존재합니다");
            Debug.Log("등록실패");
        }
    }


}
