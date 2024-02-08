using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderaerAtoB : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    public void Play(Vector3 from,Vector3 to)
    {
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }
    public void stop()
    {
        lineRenderer.enabled=false;
    }
}
