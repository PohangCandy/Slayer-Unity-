using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] Renderer[] backRenderers;
    [SerializeField] Renderer[] middldRenderers;
    [SerializeField] string sortingLayerName;
    int originOrder;
    // Start is called before the first frame update
    void Start()
    {
        //SetOrder(0);
    }
    public void SetOriginOrder(int originOrder)
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }

    public void SetMostFrontOrder(bool isMostFront)
    {
        SetOrder(isMostFront ? 100 : originOrder);
    }

    public void SetOrder(int order)
    {
        int mulOrder = order * 10;
        foreach(var renderer in backRenderers) 
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
        }

        foreach (var renderer in middldRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder+1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
