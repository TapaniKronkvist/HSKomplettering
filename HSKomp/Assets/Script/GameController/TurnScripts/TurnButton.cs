using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnButton : MonoBehaviour
{

    float maxTurnTime;
    [SerializeField] float turnTimer;
    [SerializeField] bool myTurn;
    GameObject myPlayer;
    GameObject aiPlayer;

    Text uiTimer;
    GameObject gameController;
    GameObject ai;

    float buttonAngle;

    int startTurn;
    public System.Random coinFlip = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        ai = GameObject.Find("AIEasy");
        gameController = GameObject.Find("GameController");
        myPlayer = transform.parent.gameObject;
        aiPlayer = transform.parent.gameObject;
        maxTurnTime = 30;
        turnTimer = maxTurnTime;
        myTurn = true;
        uiTimer = GameObject.Find("UITimer").GetComponent<Text>();
        startTurn = coinFlip.Next(1, 3);

        /*if (startTurn == 1)
        {
            myTurn = false;
        }
        if (startTurn == 2)
        {
            myTurn = true;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        UIUpdate();
        if(!GameOver())
        {
            if (turnTimer <= 0)
            {
                if (myTurn)
                {
                    EndTurn(); //if my turn and timer is 0, force end turn.
                }
                else
                {
                    turnTimer = 0; //if not my turn and timer is <= 0. keep timer at 0
                }
            }
            else
            {
                turnTimer -= Time.deltaTime; // countdown turntimer
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(buttonAngle, 180, 0)), 0.2f);
        }
    }

    private void OnMouseUp()
    {
        if (myTurn)
        {
            print("click");
            if (Input.GetMouseButtonUp(0))
            {
                EndTurn();
            }
        }
    }
    void EndTurn()
    {
        if (!GameOver())
        {
            foreach (GameObject card in GameObject.FindGameObjectsWithTag("PlayerOneCard"))
            {
                print(card.GetComponent<Card>().cardName);
                if (card.GetComponent<Card>().GetState() == Card.CardState.Played)
                {
                    
                    card.GetComponent<Card>().sleep = false;
                    card.GetComponent<Card>().hasAttacked = false;
                }
            }
            myTurn = false;
            ai.GetComponent<AI>().aiTurn = true;
            buttonAngle = 180;
            gameController.GetComponent<GameController>().newTurn();
            gameController.GetComponent<GameController>().SetState(GameController.GameState.PlayerTwo);
        }
    }

    public void ResetTurn()
    {
        if (!GameOver())
        {
            print("reset");
            buttonAngle = 0;
            turnTimer = maxTurnTime;
            myTurn = true;
            myPlayer.GetComponentInChildren<CardHand>().AddCardFromDeck();
            if (myPlayer.GetComponent<PlayerScript>().maxMana < 10)
            {
                myPlayer.GetComponent<PlayerScript>().maxMana++;
            }
            myPlayer.GetComponent<PlayerScript>().mana = myPlayer.GetComponent<PlayerScript>().maxMana;
            gameController.GetComponent<GameController>().SetState(GameController.GameState.PlayerOne);
        }

    }
    void UIUpdate()
    {
        uiTimer.text = $"{(int)turnTimer}";
        if(turnTimer < 10)
        {
            uiTimer.color = new Color(255, 0, 0);
        }
        else
        {
            uiTimer.color = new Color(255, 255, 255);
        }
    }

    bool GameOver()
    {
        if(gameController.GetComponent<GameController>().GetState() == GameController.GameState.GameOver)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
