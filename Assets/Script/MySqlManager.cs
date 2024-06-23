using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;

public class MySqlManager
{
    [Serializable]
    public class Player
    {
        public string error { get; set; }
        public string user_id;
        public string name;
        public string email;
        public string password;
    }
    readonly static string SERVER_URL = "http://localhost/api";
    //readonly static string SERVER_URL = "localhost:3307/api";
    public static async Task<bool> RegisterUser(string userId,string email, string password, string userName)
    {
        string REGISTER_USER_URL = $"{SERVER_URL}/register.php";
        Player playerInstance = new Player();
        playerInstance.user_id = userId;
        playerInstance.name = userName;
        playerInstance.password = password;
        playerInstance.email = email;
        //JsonUtility.ToJson(playerInstance);
        return await SendPostRequest(REGISTER_USER_URL, JsonUtility.ToJson(playerInstance));
        //return await SendPostRequest(REGISTER_USER_URL, new Dictionary<string, string>()
        //{
        //    {"userId",userId },
        //    {"userName",userName},
        //    {"email",email},
        //    {"password",password}
        //});
    }
    // Start is called before the first frame update

    public static async Task<bool> SendPostRequest(string url, string data)
    {
        using (UnityWebRequest req = UnityWebRequest.Post(url, data, "application/json"))//url로 해당하는 데이터를 보낸다.
        {
            req.SendWebRequest();
            while (!req.isDone)//결과 전송이 아직 안되었다면 대기
            {
                await Task.Delay(100);
            }
            if(req.error!=null||!string.IsNullOrWhiteSpace(req.error))//에러가 있다고 판정해서 결과가 전송되지 않았음을 처리
            {
                return false;
            }
            Debug.Log("response: " + req.downloadHandler.text);
            var res =JsonConvert.DeserializeObject<Player>(req.downloadHandler.text);

            if(res.error=="no")
                return true;
            return false;
            //res.error=
            //JsonConvert.SerializeObject(res)
            //if (res.)

        }
    }


}

public class DatabaseUser
{
    public string UserId;
    public string Email;
    public string UserName;
    public string Password;
}
