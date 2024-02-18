using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Player player;
    [SerializeField]
    private TextMeshProUGUI hpText;
    [SerializeField]
    private TextMeshProUGUI defenseText;
    [SerializeField]
    private TextMeshPro EnergyText;
    state currentState;
   // [SerializeField]
   // private Image stateImage;
    enum state
    {
        normal,
        defense
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = state.normal;
        hpBar.value=(player.getCurrentHp() / player.getMaxHp());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mathf.Lerp(hpBar.value, (player.getCurrentHp() / player.getMaxHp()), Time.deltaTime * 10));
        changeState();
        setHpText();
        setHpBarValue();
        setEnergyText();
        switch(currentState)
        {
            case state.normal:
                {
                    defenseText.enabled = false;
                    //stateImage=Resources.Load("Texture/Icon/heart.png",typeof(Texture2D)) as Image;
                    //Texture2D texture = Resources.Load("Texture/Icon/heart") as Texture2D;

                    // Texture2D를 Sprite로 변환
                    //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    // Sprite를 Image에 할당 (예시로 Image 변수를 만들어야 함)
                    //stateImage.sprite = sprite;
                }
                break;
            case state.defense:
                {
                    defenseText.enabled = true;
                    setDefenseText();
                    //Texture2D texture = Resources.Load("Texture/Icon/heart-shield") as Texture2D;

                    // Texture2D를 Sprite로 변환
                    //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    // Sprite를 Image에 할당 (예시로 Image 변수를 만들어야 함)
                    //stateImage.sprite = sprite;
                    //stateImage = Resources.Load("Icon/heart-shield.png", typeof(Texture2D)) as Texture2D;
                }
                break;
        }
        
    }
    public void setHpText() { hpText.text = player.getCurrentHp() + "/" + player.getMaxHp(); }
    public void setDefenseText() { defenseText.text = player.getDefense().ToString();  }
    public void setHpBarValue() { hpBar.value = Mathf.Lerp(hpBar.value,(player.getCurrentHp() / player.getMaxHp()),Time.deltaTime*10);  }
    public void changeState()
    {
        switch(currentState) 
        {
            case state.normal:
                {
                    if(player.getDefense()>0)
                        currentState = state.defense;
                }
                break;
            case state.defense:
                {
                    if (player.getDefense() == 0)
                        currentState = state.normal;
                }
                break;
        }
    }
    public void setEnergyText()
    {
        EnergyText.text = "Energy:" + CardManager.Inst.GetEnegy();
    }
}
