using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyDeck : MonoBehaviour
{
    public CardSO CardSO;
    public List<Card> Deck;
    public List<Potion> HasPotion;
    public float playerhp;
    public static DontDestroyDeck instance {get;private set;}

    void Awake()
    {
        // SoundManager 인스턴스가 이미 있는지 확인, 이 상태로 설정
        if (instance == null)
        {
            instance = this;
            Deck = new List<Card>();
            HasPotion = new List<Potion>();

            //for (int i = 0; i < 4; i++)
            //{
            //    Deck.Add(CardManager.Inst.CardSO.cards[0]);
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    Deck.Add(CardManager.Inst.CardSO.cards[1]);
            //}
            //Deck.Add(CardManager.Inst.CardSO.cards[2]);
        }

        // 인스턴스가 이미 있는 경우 오브젝트 제거
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //startgame();
    }
    //게임시작할때
    public void startgame()
    {
        for (int i = 0; i < 4; i++)
        {
            Deck.Add(CardSO.cards[0]);
        }
        for (int i = 0; i < 5; i++)
        {
            Deck.Add(CardSO.cards[1]);
        }
        Deck.Add(CardSO.cards[2]);
    }
    public void addDeck(Card card)
    {
        Deck.Add(card);
    }
    public void addPotion(Potion potion)
    {
        HasPotion.Add(potion);
    }
    //씬이 초기화 될때
    public void endgame()
    {
        Deck.Clear();
        HasPotion.Clear();
        playerhp = 80;
    }
}
