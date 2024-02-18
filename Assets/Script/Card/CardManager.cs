using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;


public class CardManager : MonoBehaviour
{
    public TurnManager turnManager;
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
    int MaxEnergy;
    int Energy;
    List<Card> Deck;
    List<Card> BurnPile;//�Ҹ�
    List<Card> DiscardPile;//����� ī��
    List<Card> DrawPile;
    int curturn;

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
        MaxEnergy = 3;
        Energy = MaxEnergy;
        istriggered = false;
        MaxHandleCardCount = 10;
        FirstSetup();
        DeckShuffle();
        CurrentState = State.Normal;

        DeckToDraw();
        MyTurn();
        curturn = 0;
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
    public  int  GetEnergy(){ return Energy; }
    public void PlusEnergy(int plus) { Energy += plus; }

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
        //DrawPile.Clear();
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
                }
                break;
        }

        {//if(Input.GetKeyDown(KeyCode.E)) { Debug.Log("E"); }
            if (Input.GetKeyDown(KeyCode.A)) { if (HandOfCards.Count < 10) DrawCard(1);/*CardInstance(DrawPile[DrawPile.Count])*/; }
            if (Input.GetKeyDown(KeyCode.S)) { DeckToDraw(); }
            if (Input.GetKeyDown(KeyCode.M)) { MyTurn(); }
            if (Input.GetKeyDown(KeyCode.D))
            {
                DrawPileRestart();
                PartialShuffle();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Deck.Add(CardSO.cards[0]);
                //CardInstance(Deck[0]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Deck.Add(CardSO.cards[1]);
                //CardInstance(Deck[0]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Deck.Add(CardSO.cards[2]);
                //CardInstance(Deck[0]);
            }
            //attackdraw
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Deck.Add(CardSO.cards[3]);
                //CardInstance(Deck[0]);
            }
            //tempattackpower
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Deck.Add(CardSO.cards[4]);
                //CardInstance(Deck[0]);
            }
            //defensediscard
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Deck.Add(CardSO.cards[5]);
                //CardInstance(Deck[0]);
            }
            //alldamage
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                Deck.Add(CardSO.cards[6]);
                //CardInstance(Deck[0]);
            }
            //draw two
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                Deck.Add(CardSO.cards[7]);
                //CardInstance(Deck[0]);
            }
        }

    }
    public void DeckToDraw()
    {
        DeckShuffle();
        for(int i=0; i < Deck.Count; i++) 
        {
            DrawPile.Add(Deck[i]);
        }
        CurrentState= State.Battle;
    }

    public void TurnOver()
    {
        for(int i=0;i<HandOfCards.Count;i++) 
        {
            GotoDiscardPile(HandOfCards[i].GetComponent<CardInterface>());
        }
        int count = HandOfCards.Count;
        for(int i=0;i<count;i++) 
        {
            DestroyHandofCard(HandOfCards[i]);
        }
        HandOfCards.Clear();
    }

    public void BattleEnd() 
    {
        DrawPile.Clear();
        BurnPile.Clear();
        DiscardPile.Clear();
        HandOfCards.Clear();
    }

    public void DrawCard(int count)
    {
        if (DrawPile == null)
            return;
        if (DrawPile.Count == 0||DrawPile.Count<count)
        {
            if (DiscardPile.Count == 0)
                return;
            DrawPileRestart();
            PartialShuffle();
        }
        for ( int i=0;i<count;i++)
        {
            if (HandOfCards.Count >= 10)
            {
                if(DrawPile.Count == 0) { return; }
                if(DiscardPile.Count == 0) { return;}
                DrawPileGotoDiscardPile(DrawPile[DrawPile.Count - 1]);
                DrawPile.RemoveAt(DrawPile.Count - 1);
                continue; 
            }

            CardInstance(DrawPile[DrawPile.Count-1]);
            DrawPile.RemoveAt(DrawPile.Count-1);
        }
    }
    public void MyTurn()
    {
        DrawCard(5);
        Energy = MaxEnergy;
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
                    var TargetCard= CardObject.GetComponent<TargetCard>();
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

    public void RemoveHandofCard(Object card) { HandOfCards.Remove(card); }
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
        List<PRS>results=new List<PRS>(count);//���۽�Ƽ
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
        //�� ������
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

    void DrawPileGotoDiscardPile(Card card)
    {
        DiscardPile.Add(card);
        DrawPile.RemoveAt(0);
    }
    public void GotoDiscardPile(CardInterface card)
    {
        Card temp=card.getCard();
        DiscardPile.Add(temp);
    }
    public void GotoBurnPile(CardInterface card)
    {
        Card temp = card.getCard();
        BurnPile.Add(temp);
    }
    public void LagerCard(CardInterface cardinstance)
    {
        bool isTargetCard = IsTargetCard(cardinstance);
        //var card = isTargetCard ? cardinstance as TargetCard : cardinstance as NonTargetCard;

        if (isTargetCard) 
        {
            var card = cardinstance as TargetCard;        
            Vector3 large = new Vector3(card.originPRS.pos.x, card.originPRS.pos.y + 2f, -5f);
            card.MoveTransform(new PRS(large, Quaternion.identity, Vector3.one * 2.3f), false);
            card.GetComponent<Order>().SetMostFrontOrder(true);
        }
        else
        {
            var card = cardinstance as NonTargetCard;
            Vector3 large = new Vector3(card.originPRS.pos.x, card.originPRS.pos.y + 2f, -5f);
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

    public void RandomDiscard(CardInterface card) 
    {
        
        if (HandOfCards.Count == 1)
            return;
        int value=Random.RandomRange(0, HandOfCards.Count);

        while (HandOfCards[value] == card)
            value = Random.RandomRange(0, HandOfCards.Count);//값이 같을경우에 다른값으로 바꾸기

        BurnPile.Add(HandOfCards[value].GetComponent<CardInterface>().getCard());
        
        DestroyHandofCard(HandOfCards[value]);
        HandOfCards.RemoveAt(value);
        //TargetCard obj;
        //NonTargetCard obj1;

        //if (IsTargetCard(HandOfCards[value] as CardInterface))
        //{
        //    obj = HandOfCards[value] as TargetCard;
        //    HandOfCards.RemoveAt(value);
        //    obj.SetStateDestroy();
        //}
        //else
        //{
        //    obj1 = HandOfCards[value] as NonTargetCard;
        //    HandOfCards.RemoveAt(value);
        //    obj1.SetStateDestroy();
        //}
        


        //Destroy(obj);
        //Object temp = HandOfCards[value];
        //HandOfCards[value] = HandOfCards[HandOfCards.Count - 1];

        //HandOfCards.RemoveAt(HandOfCards.Count - 1);
    }
    void DestroyHandofCard(Object card)
    {
        if (IsTargetCard(card as CardInterface))
        {
            TargetCard obj = card as TargetCard;
            //HandOfCards.Remove(card);
            obj.SetStateDestroy();
        }
        else
        {
            NonTargetCard obj1 = card as NonTargetCard;
            //HandOfCards.Remove(card);
            obj1.SetStateDestroy();
        }
    }

}
