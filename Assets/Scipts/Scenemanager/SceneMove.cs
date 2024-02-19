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
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
    public void LoadMapScene()
    {
        SceneManager.LoadScene("Map");
    }
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle");
    }
    public void ResetButtonState()
    {
        Debug.Log("Damm");
    }
}
