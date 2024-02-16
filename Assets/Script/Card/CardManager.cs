using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        Inst = this;
    }
    [SerializeField] CardSO CardSO;
    //[SerializeField] GameObject AttackCardPrefeb;
    //[SerializeField] GameObject SkillCardPrefeb;
    //[SerializeField] GameObject PowerCardPrefeb;
    [SerializeField] GameObject TargetCardPrefeb;
    [SerializeField] GameObject NonTargetCardPrefeb;
    int Enegy;
    List<Card> Deck;
    List<Card> BurnPile;//소멸
    List<Card> DiscardPile;//사용한 카드
    List<Card> DrawPile;

    public Transform CardSpawnPoint;
    public Transform CardLeft;
    public Transform CardRight;

    List<Object> HandOfCards;
    State CurrentState;
    int MaxHandleCardCount;
    public bool istriggered;
    enum State
    {
        Normal
        , Battle
    }
    private void Start()
    {
        Enegy = 3;
        istriggered = false;
        MaxHandleCardCount = 10;
        FirstSetup();
        DeckShuffle();
        CurrentState = State.Normal;
    }
    void FirstSetup()
    {
        Deck = new List<Card>();
        BurnPile = new List<Card>();
        DrawPile = new List<Card>();
        DiscardPile = new List<Card>();

        HandOfCards = new List<Object>(10);
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
    public  int  GetEnegy(){ return Enegy; }
    public void PlusEnegy(int plus) { Enegy += plus; }

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
        switch (CurrentState)
        {
            case State.Normal:
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
        if (Input.GetKeyDown(KeyCode.A)) { if(HandOfCards.Count<10)CardInstance(DrawPile[0]); }//이게 덱에서 나오는게 아니라 뽑힐 카드에서 나오게 해야함.
        if (Input.GetKeyDown(KeyCode.S)) { BattleStart(); }//이게 덱에서 나오는게 아니라 뽑힐 카드에서 나오게 해야함.
        if (Input.GetKeyDown(KeyCode.D)) { PartialShuffle(); }//이게 덱에서 나오는게 아니라 뽑힐 카드에서 나오게 해야함.

    }
    public void BattleStart()
    {
        DeckShuffle();
        for(int i=0; i < Deck.Count; i++) 
        {
            DrawPile.Add(Deck[i]);
        }
        CurrentState= State.Battle;
    }

    public void BattleEnd() 
    {
        
    }

    public void MyTurn()
    {
        DrawPile
    }
    public void SwapPop(Object _card)
    {
        int index=HandOfCards.FindIndex(card => card == _card);
        if(index+1==HandOfCards.Count)
        {
            HandOfCards.RemoveAt(index);
        }
        else
        {
            HandOfCards[index] = HandOfCards[HandOfCards.Count-1];
            HandOfCards.RemoveAt(HandOfCards.Count-1);
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
        switch (card.mode)
        {
            case "Target":
                {
                    var CardObject = Instantiate(TargetCardPrefeb, CardSpawnPoint.position, Quaternion.identity);
                    var  TargetCard= CardObject.GetComponent<TargetCard>();
                    TargetCard.setUp(card);
                    HandOfCards.Add(TargetCard);
                }
                break;
            case "NonTarget":
                {
                    var CardObject = Instantiate(NonTargetCardPrefeb, CardSpawnPoint.position, Quaternion.identity);
                    var nonTargetCard = CardObject.GetComponent<NonTargetCard>();
                    nonTargetCard.setUp(card);
                    HandOfCards.Add(nonTargetCard);
                }
                break;
        }
        SetOriginOrder();
        CardAlignment();
    }

    
    void SetOriginOrder()
    {
        int count = HandOfCards.Count;
        for(int i=0; i<count; i++) 
        {
            //bool isTargetCard = IsTargetCard(HandOfCards[i]);
            //if (isTargetCard)
            //{
            //    var ma=ga as TargetCard;
            //    var card = HandOfCards[i] as GameObject;
            //    card.
            //}
            HandOfCards[i].GetComponent<Order>().SetOriginOrder(i);
        }
    }


    public void CardAlignment()
    { 
        List<PRS> originCardPRSs=new List<PRS>();
        originCardPRSs = RoundAlignment(CardLeft, CardRight, HandOfCards.Count, 0.5f, Vector3.one * 0.9f);
        for(int i=0;i<HandOfCards.Count;i++) 
        {
            bool isTargetCard = IsTargetCard(HandOfCards[i] as CardInterface);
            if (isTargetCard)
            {
                var card = HandOfCards[i].GetComponent<TargetCard>();
                card.originPRS = originCardPRSs[i];//new PRS(Vector3.zero, Quaternion.identity, Vector3.one * 1.9f);
                card.MoveTransform(card.originPRS, true, 0.7f);
            }
            else
            {
                var card = HandOfCards[i].GetComponent<NonTargetCard>();
                card.originPRS = originCardPRSs[i];//new PRS(Vector3.zero, Quaternion.identity, Vector3.one * 1.9f);
                card.MoveTransform(card.originPRS, true, 0.7f);
            }
                
        }
    }

    List<PRS> RoundAlignment(Transform left,Transform right,int count,float height, Vector3 scale)
    {
        float[]objLerps=new float[count];
        List<PRS>results=new List<PRS>(count);//케퍼시티
        switch (count)
        {
            case 1: objLerps = new float[] { 0.5f };break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (count - 1);
                for (int i = 0; i < count; i++)
                    objLerps[i] = interval * i;
                break;
        }
        //원 방정식
        for(int i = 0; i < count;i++)
        {
            var pos = Vector3.Lerp(left.position, right.position, objLerps[i]);
            var rot = Quaternion.identity;
            if(count>=3)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2)) - Mathf.Pow(objLerps[i]-0.5f,2);
                curve=height>=0?curve:-curve;
                pos.y += curve;
                rot = Quaternion.Slerp(left.rotation, right.rotation, objLerps[i]);
            }
            results.Add(new PRS(pos, rot, scale));
        }
        return results;
    }

    
    public void LagerCard(CardInterface cardinstance)
    {
        bool isTargetCard = IsTargetCard(cardinstance);
        //var card = isTargetCard ? cardinstance as TargetCard : cardinstance as NonTargetCard;

        if (isTargetCard) 
        {
            var card = cardinstance as TargetCard;        
            Vector3 large = new Vector3(card.originPRS.pos.x, card.originPRS.pos.y + 2f, -50f);
            card.MoveTransform(new PRS(large, Quaternion.identity, Vector3.one * 2.3f), false);
            card.GetComponent<Order>().SetMostFrontOrder(true);
        }
        else
        {
            var card = cardinstance as NonTargetCard;
            Vector3 large = new Vector3(card.originPRS.pos.x, card.originPRS.pos.y + 2f, -50f);
            card.MoveTransform(new PRS(large, Quaternion.identity, Vector3.one * 2.3f), false);
            card.GetComponent<Order>().SetMostFrontOrder(true);
        }

    }
    public bool IsTargetCard(CardInterface cardinstance)
    { return cardinstance is TargetCard; }
    public  void SmallerCard(CardInterface cardinstance)
    {
        bool isTargetCard =IsTargetCard(cardinstance);
        //var card = isTargetCard ? cardinstance as TargetCard : cardinstance as NonTargetCard;

        if (isTargetCard)
        {
            var card = cardinstance as TargetCard;
            card.MoveTransform(card.originPRS, false);
            card.GetComponent<Order>().SetMostFrontOrder(false);
        }
        else
        {
            var card = cardinstance as NonTargetCard;
            card.MoveTransform(card.originPRS, false);
            card.GetComponent<Order>().SetMostFrontOrder(false);
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
