using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int defense;
    Renderer playerColor;
    // Start is called before the first frame update
    void Start()
    {
        playerColor = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setDefense(int amount)
    {
        defense = amount;
        Debug.Log("¹æ¾î·Â »ó½Â");
        playerColor.material.color = Color.gray;
    }
}
