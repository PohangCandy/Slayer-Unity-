using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject WorldObject;
    // Update is called once per frame
    void Update()
    {
        transform.position=Camera.main.WorldToScreenPoint(WorldObject.transform.position);
    }
}
