﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class controlled the movement of cards Instantiated by the ai onto the board.

public class AICard : MonoBehaviour
{
    public int hp = 4;
    public int attack = 2;
    public int manaCost = 1;
    public string cardName, cardText;
    Text showHp, showAttack, showManaCost, showName, showCardName, showCardText;
    public bool sleep = true;


    void Start()
    {
        //HP
        showHp = GameObject.Find($"{gameObject.name}/Canvas/DisplayHp").GetComponent<Text>();
        showHp.text = "" + hp;
        //Attack
        showAttack = GameObject.Find($"{gameObject.name}/Canvas/DisplayAttack").GetComponent<Text>();
        showAttack.text = "" + attack;
        //Mana cost
        showManaCost = GameObject.Find($"{gameObject.name}/Canvas/DisplayManaCost").GetComponent<Text>();
        showManaCost.text = "" + manaCost;
        //Name
        showCardName = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardName").GetComponent<Text>();
        showCardName.text = cardName;
        //Card text
        showCardText = GameObject.Find($"{gameObject.name}/Canvas/DisplayCardText").GetComponent<Text>();
        showCardText.text = cardText;

    }
}
