using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle");
    }
    public void ResetButtonState()
    {
        Debug.Log("Damm");
    }
}
