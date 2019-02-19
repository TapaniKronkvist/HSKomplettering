using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour {
    [SerializeField] List<GameObject> myCards = new List<GameObject>();
    [SerializeField] float xOffset;
    [SerializeField] float zOffset;
    [SerializeField] float rotationZOffset;

    GameObject myPlayer;
    RaycastHit hit;
    Ray ray;
    GameObject myDeck;
    GameObject inspectedCard;
    GameObject gameController;
    GameObject instancedCard;
    GameObject inspectCard;
    bool isCreated;
    int fatigue;

    // Use this for initialization
    void Start ()
    {
        fatigue = 1;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        myPlayer = transform.parent.gameObject;
        isCreated = false;
        inspectCard = GameObject.Find("InspectCard");
        myDeck = GameObject.Find("CardDeckPlayerOne");
        myDeck.GetComponent<InitiateDeck>().ShuffleDeck();
        for (int i = 0; i < 3; i++)
        {
            AddCardFromDeck();
            print(myCards[i].GetComponent<Card>().GetState());

        }
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
       
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<Card>() != null && hit.collider.GetComponent<Card>().GetState() == Card.CardState.InHand || hit.collider.GetComponent<Card>() != null && hit.collider.GetComponent<Card>().GetState() == Card.CardState.Played)
            {
                if (inspectedCard != hit.collider.gameObject)
                {
                    SortCards();
                    Destroy(instancedCard);
                    isCreated = false;
                }
                    inspectedCard = hit.collider.gameObject;
                    InspectCard(hit.collider.gameObject);
            }
            else
            {
                Destroy(instancedCard);
                isCreated = false;
            }
        }
        else
        {
            Destroy(instancedCard);
            isCreated = false;
        }
    }


    public void AddCardFromDeck()
    {
        if (myDeck.transform.childCount > 0)
        {
            AddCardToHand(myDeck.transform.GetChild(0).gameObject);
            fatigue = 1;
        }
        else
        {
            myPlayer.GetComponent<PlayerScript>().RemoveHealth(fatigue);
            fatigue += 1;
        }
    }
    public void RemoveCardFromHand(GameObject removeCard)
    {
        for (int i = 0; i < myCards.Count; i++)
        {
            if(myCards[i].name == removeCard.name)
            {
                myCards.RemoveAt(i);
            }
        }
    }

    public void AddCardToHand(GameObject newCard)
    {
        if(myCards.Count < 10)
        {
            newCard.GetComponent<Card>().SetState(Card.CardState.InHand);
            newCard.transform.SetParent(transform);
            newCard.GetComponent<Card>().myHand = gameObject;
            myCards.Add(newCard);
            SortCards();
        } else
        {
            Destroy(newCard);
        }
    }
    public void SortCards()
    {
        if (myCards.Count != 0)
        {
            rotationZOffset = -100 / (myCards.Count);

            for (int i = 0; i < myCards.Count; i++)
            {
                myCards[i].transform.position = new Vector3(transform.position.x + xOffset * i, transform.position.y + (0.1f * i), transform.position.z + zOffset * i);
                myCards[i].transform.localRotation = Quaternion.Euler(90, 0, rotationZOffset * i);
            }
            transform.rotation = Quaternion.Euler(0, (rotationZOffset / 2) * (myCards.Count - 1), 0);
        }
    }
    void InspectCard(GameObject card)
    {
        if (!isCreated)
        {
            SortCards();
            instancedCard = Instantiate(card, inspectCard.transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
            instancedCard.transform.localScale = new Vector3(3,3,3);
            instancedCard.transform.parent = inspectCard.transform;
            isCreated = true;
        }
    }


}
