using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyDeck : MonoBehaviour
{
    public List<Card> Deck;
    public static DontDestroyDeck instance {get;private set;}

    void Awake()
    {
        // SoundManager �ν��Ͻ��� �̹� �ִ��� Ȯ��, �� ���·� ����
        if (instance == null)
        {
            instance = this;
            Deck = new List<Card>();
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

        // �ν��Ͻ��� �̹� �ִ� ��� ������Ʈ ����
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        startgame();
    }
    //���ӽ����Ҷ�
    public void startgame()
    {
        for (int i = 0; i < 4; i++)
        {
            Deck.Add(CardManager.Inst.CardSO.cards[0]);
        }
        for (int i = 0; i < 5; i++)
        {
            Deck.Add(CardManager.Inst.CardSO.cards[1]);
        }
        Deck.Add(CardManager.Inst.CardSO.cards[2]);
    }
    public void addDeck(Card card)
    {
        Deck.Add(card);
    }
    //���� �ʱ�ȭ �ɶ�
    public void endgame()
    {
        Deck.Clear();
    }
}