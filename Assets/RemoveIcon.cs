using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveIcon : MonoBehaviour
{
    public void RemoveThisObj()
    {
        this.gameObject.SetActive(false);
    }
}
