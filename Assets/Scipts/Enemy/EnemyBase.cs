using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    int hp;
    [SerializeField]
    public Slider slider;
    void Start()
    {
        hp = 100;
        slider.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("v");
            Attack(10); }
    }

    public void Attack(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            slider.value = hp;
        }
    }
}
