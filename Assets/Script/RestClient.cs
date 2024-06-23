using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace API
{
    class LoginReq
    {
        public string user_id { get; set; }
        public string password { get; set; }
    }



    class LoginRes
    {

        public string error { get; set; }
        public string? user_id { get; set; }
        public string? user_name { get; set; }

        public List<string>? characters { get; set; }
        public string? msg { get; set; }
    }



    class CommonReq
    {
        public string error { get; set; }
        public string? msg { get; set; }
    }

    class CommonRes
    {
        public string error { get; set; }
        public string? msg { get; set; }
    }

    class CharacterInfo
    {
        public string class_name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public int level { get; set; }
        public int max_hp { get; set; }
        public int max_mana { get; set; }

        public int max_exp { get; set; }
    }

    class MyCharacterInfo
    {
        public string class_name { get; set; }
        public int level { get; set; }
        public int hp { get; set; }
        public int mana { get; set; }
        public int exp { get; set; }
    }

    class ListCharactersRes
    {
        public string error { get; set; }
        public string? msg { get; set; }

        public List<CharacterInfo> characters { get; set; }
        public List<MyCharacterInfo>? my_characters { get; set; }
    }

    class CreateCharacterReq
    {
        public string class_name { get; set; }
        public int level { get; set; }
        public int max_hp { get; set; }
        public int max_mana { get; set; }
        public string character_name { get; set; }
    }

    class CreateCharacterRes
    {
        public string error { get; set; }
        public int? new_character { get; set; }
    }


    class CheckCharacterReq
    {
         public string user_id { get; set; }
    }

    class CheckCharacterRes
    {
        public string error { get; set; }
        public string? user_id { get; set; }
        public string? name { get; set; }
    }

    class RestClient : Singleton<RestClient>
    {
        internal const string PortalURL = "http://localhost/api/";

        public delegate void Callback<T>(T res);

        public IEnumerator Request<T>(string request,
            Callback<T> callback,
            string input = null,
            List<string> urlInputs = null)
        {
            Debug.Log("REST Request: url = " + PortalURL + request);
            var url = PortalURL + request;
            Debug.Log("input: " + input);

            if (urlInputs != null)
            {
                foreach (var val in urlInputs)
                {
                    url += "/" + UnityWebRequest.EscapeURL(val);
                }
            }

            using (UnityWebRequest www = UnityWebRequest.Post(url, input, "application/json"))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("response: " + www.downloadHandler.text);
                    var res = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
                    callback?.Invoke(res);
                }
                else
                {

                }

            }
        }
    }
}