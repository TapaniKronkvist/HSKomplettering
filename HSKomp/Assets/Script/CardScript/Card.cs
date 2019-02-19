using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Card : MonoBehaviour
{   //This is a prefab for all the cards in the game. Includes variables that effects card in play.
    //Written by Tapani Kronvkist

    public enum CardState { InDeck, InHand, PickedUp, Released, Played, InGraveyard };

    [SerializeField] CardState myState;

    public string cardName, cardType, cardText;
    public int hp, attack, manaCost;
    public Sprite frame, portrait;
    Text showHp, showAttack, showCardName, showCardText, showManaCost, showCardType;
    Image showFrame, showPortrait;

    PlayerScript myPlayer;
    GameObject gameController;
    public GameObject myHand;
    Collider myCollider;

    public bool sleep;
    public bool isPlayed;
    RaycastHit hit;

    bool isColliding;
    public bool isMouseDown;

    private Vector3 mOffset;

    private float mZCoord;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        myCollider = GetComponent<Collider>();
        isPlayed = false;
        sleep = true;
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        //health
        hp = gameController.GetComponent<ReadCardData>().GetCardData(cardName).health;
        showHp = GameObject.Find($"{gameObject.name}/Canvas/DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;

        //attack
        attack = gameController.GetComponent<ReadCardData>().GetCardData(cardName).attack;
        showAttack = GameObject.Find($"{gameObject.name}/Canvas/DisplayAttack").GetComponent<Text>();
        showAttack.text = "" + attack;

        //name
        showCardName = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardName").GetComponent<Text>();
        showCardName.text = cardName;

        //Description text
        cardText = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardText;
        showCardText = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardText").GetComponent<Text>();
        showCardText.text = cardText;

        //manacost
        manaCost = gameController.GetComponent<ReadCardData>().GetCardData(cardName).manacost;
        showManaCost = GameObject.Find($"{gameObject.name}/Canvas/DisplayManaCost").GetComponent<Text>();
        showManaCost.text = "" + manaCost;

        //cardtype
        cardType = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardType;
        showCardType = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardType").GetComponent<Text>();
        showCardType.text = cardType;

        //card frame
        frame = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardFrame;
        showFrame = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardFrame").GetComponent<Image>();
        showFrame.sprite = frame;

        //card portrait
        portrait = gameController.GetComponent<ReadCardData>().GetCardData(cardName).cardPortrait;
        showPortrait = GameObject.Find($"{gameObject.name}/Canvas/DisplayPortrait").GetComponent<Image>();
        showPortrait.sprite = portrait;

        Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);
        showHp = GameObject.Find("DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;

        if(!isColliding && myState == CardState.Released && !isPlayed)
        {
            SetState(CardState.InHand);
        }
        if (isMouseDown)
        {
            transform.position = GetMouseWorldPos() + mOffset;
        }
    }

    public void SetState(CardState newState)
    {
        myState = newState;
    }

    public CardState GetState()
    {
        return myState;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        if (myState != CardState.Played)
        {
            SetState(CardState.PickedUp);
            //gameObject.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            mOffset = gameObject.transform.position - GetMouseWorldPos();

            isMouseDown = true;
        }
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && myState != CardState.Played)
        {
           
            SetState(CardState.Released);
            isMouseDown = false;
            if (isPlayed)
            {
                MoveCardToHolder();
            }
            else
            {
                if (hit.collider != null && hit.collider.tag == "CardHolder" && hit.collider.transform.childCount == 0 && myPlayer.mana >= manaCost)
                {
                    myPlayer.RemoveMana(manaCost);
                    print(myPlayer.mana + "mana cost" + manaCost);
                    SetState(CardState.Played);
                    transform.SetParent(hit.transform);
                    MoveCardToHolder();
                    myHand.GetComponent<CardHand>().RemoveCardFromHand(gameObject);
                    isPlayed = true;
                    //tag = "Untagged";
                }
                if (myState == CardState.InHand)
                {
                    transform.parent.gameObject.GetComponent<CardHand>().SortCards();
                }
            }
        }
    }
    void MoveCardToHolder()
    {
        transform.position = transform.parent.position + new Vector3(0,2,0);
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }


    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}
