using StatusAdjustmentInformationNameSpace;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class Cards : MonoBehaviour
{

    public Text description;
    public Text potionname;
    int rarity;
    State currentState;
    Collider2D collider;
    Vector3 point;
    [SerializeField]
    SpriteRenderer potionsprite;
    public GameObject button;
    public SAInformation saInformation;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void setUp(Card card)
    {
        description.text = card.description.ToString();
        potionname.text = card.name.ToString();
        rarity = card.rarity;
        potionsprite.sprite = card.sprite;
        saInformation = new SAInformation(card.target, card.turn, card.category, card.amount);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
