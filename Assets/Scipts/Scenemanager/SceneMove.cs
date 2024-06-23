using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    int floor;

    void Start()
    {
        floor = 0;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Login")
        {
            if (PlayerPrefs.HasKey("user_name"))
                SceneManager.LoadScene("Title");
            return;
        }

        if (!PlayerPrefs.HasKey("user_name"))
            SceneManager.LoadScene("Login");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
    public void LoadMapScene()
    {
        SceneManager.LoadScene("Map");
    }
    public void LoadFirstFirstMapScene()
    {
        SceneManager.LoadScene("tMap1-1");
    }
    public void LoadFirstSecondMapScene()
    {
        SceneManager.LoadScene("tMap1-2");
    }
    public void LoadFirstThirdMapScene()
    {
        SceneManager.LoadScene("tMap1-3");
    }
    public void LoadSecondFirstMapScene()
    {
        SceneManager.LoadScene("tMap2-1");
    }
    public void LoadEliteandStoreMapScene()
    {
        SceneManager.LoadScene("tMap3-1");
    }
    public void LoadBattleFirstFirstScene()
    {
        SceneManager.LoadScene("tBattle1-1");
    }
    public void LoadBattleFirstSecondScene()
    {
        SceneManager.LoadScene("tBattle1-2");
    }
    public void LoadBattleFirstThridScene()
    {
        SceneManager.LoadScene("tBattle1-3");
    }
    public void LoadBattleSecondFirstScene()
    {
        SceneManager.LoadScene("tBattle2-1");
    }
    public void LoadBattleSecondSecondScene()
    {
        SceneManager.LoadScene("tBattle2-2");
    }

    public void LoadBattleThirdFirstScene()
    {
        SceneManager.LoadScene("tBattle3-1");
    }
    public void LoadBattleFouthFirstScene()
    {
        SceneManager.LoadScene("tBattle4-1");
    }
    public void ResetButtonState()
    {
        Debug.Log("Damm");
    }

    public void Logout()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("Login");
        if (asyncLoad.isDone)
        {
            Debug.Log("로그인 이동");
        }
        
    }

    public void Login()
    {
        var asyncLoad = SceneManager.LoadSceneAsync("Title");
        if (asyncLoad.isDone)
        {
            Debug.Log("타이틀 이동");
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
