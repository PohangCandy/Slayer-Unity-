using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using API;
using TMPro;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {

	RestClient rc = null;
    [SerializeField] SceneMove Scenemove;

    [SerializeField] TMP_InputField ID;
    [SerializeField] TMP_InputField Password;
    
    public string class_name= "vaccine";


    private void Start()
    {
        rc = RestClient.Instance;
    }
    public void Login()
	{
        var input = new LoginReq { user_id = ID.text, password = Password.text };
        StartCoroutine(
            rc.Request<LoginRes>("login.php", (res) => {
                Debug.Log(JsonConvert.SerializeObject(res));
				if (res.error == "no")
					Scenemove?.Invoke("Login", 0.5f);
             
                PlayerPrefs.SetString("user_id", res.user_id);
                PlayerPrefs.SetString("user_name",res.user_name);
			},
            JsonConvert.SerializeObject(input))
        );
    }
    public void Check_character()
    {
        var input = new CheckCharacterReq
        {
            user_id = PlayerPrefs.GetString("user_id")
        };

        StartCoroutine(rc.Request<CheckCharacterRes>("check_character.php", (res) =>
        {
            Debug.Log(JsonConvert.SerializeObject(res));
            Debug.Log("my character: " + res.name);
            if (res.error == "no")
            {
                Scenemove?.Invoke("LoadMapScene", 0f);
            }
        },
            JsonConvert.SerializeObject(input)
        ));
    }
    public void Character_start()
    {
        var create_param = new CreateCharacterReq
        {
            class_name = "vaccine",
            level = 0,
            max_hp = 100,
            max_mana = 0,
            character_name = "vaccine"
        };

        StartCoroutine(rc.Request<CreateCharacterRes>("create_character.php", (res) =>
        {
            Debug.Log("new character: " + res.new_character);
            //if (res.error == "no"||res.error=="null")
            {
                Scenemove?.Invoke("LoadMapScene", 0.5f);
                PlayerPrefs.SetString("scene", "Map");
            }
        },
            JsonConvert.SerializeObject(create_param)
        ));
    }

        public void Logout()
    {
        StartCoroutine(rc.Request<CommonRes>("logout.php", (res) =>
        {
            Debug.Log(res.msg);
            Debug.Log(JsonConvert.SerializeObject(res));
            Scenemove?.Invoke("Logout", 0.5f);
            PlayerPrefs.DeleteAll();
        }));
    }

    //void OnGUI() {

    //	if (GUI.Button(new Rect(0, 0, 100, 100), "로그인"))
    //	{
    //		var input = new LoginReq { user_id = "bseo", password = "1234"};

    //		StartCoroutine(
    //			rc.Request<LoginRes>("login.php", (res) => {
    //				Debug.Log(JsonConvert.SerializeObject(res));
    //			},
    //			JsonConvert.SerializeObject(input))
    //		);
    //	}

    //	if (GUI.Button(new Rect(100, 0, 100, 100), "로그아웃"))
    //	{
    //		StartCoroutine(rc.Request<CommonRes>("logout.php", (res)=> {
    //			Debug.Log(res.msg);
    //			Debug.Log(JsonConvert.SerializeObject(res));
    //		}));
    //	}

    //	if (GUI.Button(new Rect(200, 0, 100, 100), "사용자 캐릭터 정보 보기"))
    //	{
    //		StartCoroutine(rc.Request<ListCharactersRes>("list_characters.php", (res) => {
    //			Debug.Log(JsonConvert.SerializeObject(res));
    //			for (int i=0; i < res.characters.Count; i++) {
    //				Debug.Log(i + ": " + "class_name: " + res.characters[i].class_name);
    //				Debug.Log(i + ": " + "max_mana: " + res.characters[i].max_mana);

    //			}
    //			for (int i=0; i < res.my_characters.Count; i++) {
    //				Debug.Log(i + ": " + "class_name: " + res.my_characters[i].class_name);
    //				Debug.Log(i + ": " + "mana: " + res.my_characters[i].mana);
    //			}
    //		}));
    //	}
    //	if (GUI.Button(new Rect(300, 0, 100, 100), "새로운 캐릭터 만들기"))
    //	{
    //		var create_param = new CreateCharacterReq {
    //               class_name = "goblin warrior",
    //               level = 10,
    //               max_hp = 200,
    //			max_mana = 50,
    //			character_name = "내 고블린 전사"
    //           };

    //		StartCoroutine(rc.Request<CreateCharacterRes>("create_character.php", (res) => {
    //				Debug.Log("new character: " + res.new_character );
    //			},
    //			JsonConvert.SerializeObject(create_param)
    //		));
    //	}

    //}
}
