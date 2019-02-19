using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int playedTurns;
    public enum GameState { GameStart, PlayerOne, PlayerTwo, GameOver};
    GameState myState;
    float timer;
    [SerializeField] Image Quest;
    // Start is called before the first frame update
    void Start()
    {
        Quest.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 60)
        {
            Quest.enabled = true;
        }
        if (timer > 70)
        {
            Quest.enabled = false;
        }
        if (myState == GameState.GameOver)
        {
            Application.Quit();
        }
    }

    public void newTurn()
    {
        playedTurns++;
    }

    public void SetState(GameState newState)
    {
        myState = newState;
    }

    public GameState GetState()
    {
        return myState;
    }
}
