using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get;private set;}
    // Start is called before the first frame update
    private void Awake()
    {
        Inst = this;
    }
    [SerializeField] CardSO CardSO;
    [SerializeField] GameObject AttackCardPrefeb;
    [SerializeField] GameObject SkillCardPrefeb;
    [SerializeField] GameObject PowerCardPrefeb;
    List<Card> Deck;
    List<Card> BurnPile;//소멸
    List<Card> DiscardPile;//사용한 카드
    List<Card> DrawPile;
    
    public Transform CardSpawnPoint;

    List<GameObject> HandOfCards;
    State CurrentState;
    int MaxHandleCardCount;
    enum State
    {
        NonShuffle
        ,Shuffled
        ,Battle
    }
    private void Start()
    {
        MaxHandleCardCount = 10;
        FirstSetup();
        DeckShuffle();
        CurrentState = State.Shuffled;
    }
    void FirstSetup() 
    {
        Deck = new List<Card>();
        BurnPile = new List<Card>();
        DrawPile = new List<Card>();
        DiscardPile = new List<Card>();

        HandOfCards = new List<GameObject>();
        for(int i=0;i<4;i++)
        {
            Deck.Add(CardSO.cards[0]);
        }
        for (int i = 0; i < 5; i++)
        {
            Deck.Add(CardSO.cards[1]);
        }
        Deck.Add(CardSO.cards[2]);

    }

    void DeckShuffle()
    {
        for(int i=0;i< Deck.Count;i++)
        {
            int random=Random.Range(i, Deck.Count);
            Card flash = Deck[i];
            Deck[i] = Deck[random];
            Deck[random] = flash;
        }
    }

    void PartialShuffle()
    {
        for(int i=0;i<DrawPile.Count;i++) 
        {
            int random = Random.Range(i, DrawPile.Count);
            Card flash = DrawPile[i];
            DrawPile[i] = DrawPile[random];
            DrawPile[random] = flash;
        }
    }

    void DrawPileRestart()
    {
        DrawPile.Clear();
        for(int i=0;i<DiscardPile.Count;i++)
            DrawPile.Add(DiscardPile[i]);
        DiscardPile.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        ChangeState();
        switch (CurrentState)
        {
            case State.NonShuffle:
                {
                    
                }
                break;
            case State.Shuffled:
                break;
            case State.Battle:
                {
                    if (DrawPile.Count == 0)
                    {
                        DrawPileRestart();
                        PartialShuffle();
                    }
                }
                break;
        }

        if(Input.GetKeyDown(KeyCode.A)) { CardInstance(Deck[0]); }
    }
    void ChangeState()
    {
        switch (CurrentState) 
        {
            case State.NonShuffle:
                {
                    DeckShuffle();
                    CurrentState = State.Shuffled;
                }
                break;
            case State.Shuffled:
                {
                    //if() 적 이벤트 발생시에 battle로 전환
                }
                break;
            case State.Battle:
                {
                    //전투 끝나면 종료
                    CurrentState = State.NonShuffle;
                }
                break;
        }
    }

    //public Card PopCard()
    //{
    //    //if (Deck.Count <= 0)
    //    //{
    //    //    SetupBuffer();
    //    //}
    //    Card card = Deck[0];
    //    Deck.RemoveAt(0);
    //    return card;
    //}
    void CardInstance(Card card)
    {
        switch (card.type)
        {
            case "Attack":
                {
                    var CardObject = Instantiate(AttackCardPrefeb, CardSpawnPoint.position, Quaternion.identity);
                    var Attackcard = CardObject.GetComponent<AttackCard>();
                    Attackcard.setUp(card);
                    HandOfCards.Add(CardObject);
                }
                break;
            case "Skill":
                {
                    var CardObject = Instantiate(AttackCardPrefeb, CardSpawnPoint.position, Quaternion.identity);
                    var Skillcard = CardObject.GetComponent<SkillCard>();
                    //Skillcard.setUp(card);
                    HandOfCards.Add(CardObject);
                }
                break;
            case "Power":
                {
                    var CardObject = Instantiate(AttackCardPrefeb, CardSpawnPoint.position, Quaternion.identity);
                    var PowerCard = CardObject.GetComponent<PowerCard>();
                    //PowerCard.setUp(card);
                    HandOfCards.Add(CardObject);
                }
                break;
        }
        SetOriginOrder();

    }
    void SetOriginOrder()
    {
        int count = HandOfCards.Count;
        for(int i=0; i<count; i++) 
        {
            HandOfCards[i].GetComponent<Order>().SetOriginOrder(i);
        }
    }
    public void drawCard(int drawcount)
    {
        //int i = 0;
        //if (HandleCardCount == 10) return;
        //for (; i < drawcount; i++)
        //{
        //    CurrentHandPile[HandleCardCount] = DrawPile[i];
        //    if (HandleCardCount > 10)
        //        HandleCardCount++;
        //}
        //if (HandleCardCount <= 10)
        //{
        //    for (int j = 0; j < drawcount; j++)
        //        DrawPile.RemoveAt(j);
        //}
        //else
        //{
        //    for (int j = 0; j < drawcount; j++)
        //    {
        //        if(j>=i)
        //        {
        //            DiscardPile.Add(DrawPile[j]);
        //        }
        //        DrawPile.RemoveAt(j);
        //    }
        //}

    }
}
