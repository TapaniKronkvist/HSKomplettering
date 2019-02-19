using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    enum States { init, playCards, attack, resolveAttack, skill, endTurn };
    [SerializeField] States state;

    public bool aiTurn;
    public int hp, dmg, mana, cards, currentMana, spellDmg;
    [SerializeField] int cardCount;
    public List<GameObject> aiDeck;
    public List<GameObject> table;
    public List<GameObject> cardsOnTable;
    [SerializeField] List<GameObject> playerCardsOnTable;
    [SerializeField] GameObject activeCard;

    // Start is called before the first frame update
    void Start()
    {
        hp = 30;
        cards = 3;
        spellDmg = 2;
        state = States.init;
        aiTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiTurn)
        {
            switch (state)
            {
                case States.init:
                    print("Ai init");
                    init();
                    break;
                case States.playCards:
                    print("Ai playCards");
                    PlayCards();
                    break;
                case States.attack:
                    print("AI attacks");
                    foreach (GameObject card in GameObject.FindGameObjectsWithTag("PlayerOneCard")) //Counts how many cards player has on table.
                    {
                        if (card.GetComponent<Card>().isPlayed)
                        {
                            playerCardsOnTable.Add(card);

                        }
                    }
                    Attack();
                    break;
                case States.resolveAttack:
                    //ResolveAttack();
                    break;
                case States.skill:
                    print("AI use skill");
                    Invoke("Skill", 2);
                    break;
                case States.endTurn:
                    print("AI ends turn");
                    cardCount = 0; //Reset for next turn
                    playerCardsOnTable = new List<GameObject>();//Reset list to update next turn.
                    GameObject.Find("TurnButton").GetComponent<TurnButton>().ResetTurn();
                    state = States.init; //Reset state;
                    aiTurn = false;
                    break;
            }
        }
    }
    void init()
    {
        if (cardsOnTable.Count != 0)//Wake played cards
        {
            foreach (GameObject card in cardsOnTable)
            {
                card.GetComponent<AICard>().sleep = false;
            }
        }
        mana++;//Adds to mana pool.
        cards++;//Adds one card to hand
        currentMana = mana;//Current mana ai got to play with.
        state = States.playCards;
    }
    void PlayCards()
    {
        print("Inside method");
        if (aiDeck.Count != 0)
        {
            print("Inside test");
            for (int i = 0; i < table.Count; i++)
            {
                int random = Random.Range(0, aiDeck.Count);//Picks a random card
                activeCard = aiDeck[random];//Saves
                print(random);
                int checkCost = activeCard.GetComponent<AICard>().manaCost;
                if (table[i].GetComponent<tableSpot>().empty == true && currentMana >= checkCost)
                {
                    GameObject newCard = Instantiate(activeCard); //Adds card to table.
                    cardsOnTable.Add(newCard);//Adds card to list of played cards
                    newCard.transform.position = table[i].transform.position;//Add position
                    //aiDeck.RemoveAt(random);//Removes card from list.
                    currentMana -= checkCost;//Reduce mana.
                }
                if (currentMana <= 0)
                {
                    state = States.attack;
                }
            }
        }
        else
        {
            print("Attacking");
            hp -= 1;
            state = States.attack;
        }

    }

    void Attack()
    {
        print("Resolving attack");
        for (int i = 0; i < cardsOnTable.Count; i++)//Goes thru Ai cards on table.
        {
            if (cardsOnTable[i].GetComponent<AICard>().sleep == false)//If card is not sleeping it attacks.
            {

                if (playerCardsOnTable.Count != 0) //Attack card.
                {
                    GameObject target;
                    GameObject attacker;
                    int random = Random.Range(0, playerCardsOnTable.Count);//Picks  a random card to attack
                    target = playerCardsOnTable[random];
                    attacker = cardsOnTable[i];
                    target.GetComponent<Card>().hp -= attacker.GetComponent<AICard>().attack;//Resolve AIcard attack
                    attacker.GetComponent<AICard>().hp -= target.GetComponent<Card>().attack;//Reslove Card counter attack
                    attacker.GetComponent<AICard>().sleep = true;//Sets card to sleep so it is no longer part of iteration.
                    //Removes any dead cards.
                    if (target.GetComponent<Card>().hp <= 0)
                    {
                        Destroy(target);
                    }
                    if (attacker.GetComponent<AICard>().hp <= 0)
                    {
                        Destroy(attacker);
                        cardsOnTable.RemoveAt(i);
                    }
                }

            }
        }
        state = States.skill;
    }

    void Skill()
    {
        state = States.endTurn;
    }

}
