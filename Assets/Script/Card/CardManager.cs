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
    public Transform CardLeft;
    public Transform CardRight;

    List<GameObject> HandOfCards;
    State CurrentState;
    int MaxHandleCardCount;
    public bool istriggered;
    enum State
    {
        NonShuffle
        ,Shuffled
        ,Battle
    }
    private void Start()
    {
        istriggered = false;
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

        HandOfCards = new List<GameObject>(10);
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

        if(Input.GetKeyDown(KeyCode.A)) { CardInstance(Deck[0]); }//이게 덱에서 나오는게 아니라 뽑힐 카드에서 나오게 해야함.
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
        CardAlignment();
    }


    void SetOriginOrder()
    {
        int count = HandOfCards.Count;
        for(int i=0; i<count; i++) 
        {
            HandOfCards[i].GetComponent<Order>().SetOriginOrder(i);
        }
    }


    void CardAlignment()
    { 
        List<PRS> originCardPRSs=new List<PRS>();
        originCardPRSs = RoundAlignment(CardLeft, CardRight, HandOfCards.Count, 0.5f, Vector3.one * 0.9f);
        for(int i=0;i<HandOfCards.Count;i++) 
        {
            var card = HandOfCards[i].GetComponent<AttackCard>();
            card.originPRS = originCardPRSs[i];//new PRS(Vector3.zero, Quaternion.identity, Vector3.one * 1.9f);
            card.MoveTransform(card.originPRS,true,0.7f);
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

    
    public void LagerCard(AttackCard card)
    {
        Vector3 large = new Vector3(card.originPRS.pos.x, card.originPRS.pos.y, -50f);
        card.MoveTransform(new PRS(large, Quaternion.identity, Vector3.one * 1.3f), false);
        card.GetComponent<Order>().SetMostFrontOrder(true);
    }
    public  void SmallerCard(AttackCard card)
    {
        
        card.MoveTransform(card.originPRS, false);
        card.GetComponent<Order>().SetMostFrontOrder(false);
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
