using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CardClick : MonoBehaviour
{
    //Viktor
    [SerializeField] public GameObject DeckButton;
    [SerializeField] public Text Placeholder;
    [SerializeField]public bool cardLimit;

    DeckDataHolder script;
    
    void Start()
    {
        script = GameObject.Find("Data").GetComponent<DeckDataHolder>();
        cardLimit = false;
    }
    
    public void PressCard()
    {
        if (GameObject.Find("Data").GetComponent<DeckDataHolder>().saveDeck.Count < 30)
        {
            DeckButton = Instantiate(Resources.Load("Menu/Deck/DeckButton")) as GameObject;
            DeckButton.transform.GetChild(0).GetComponent<Text>().text = GetComponent<CardDisplay>().myName;

            DeckButton.transform.SetParent(GameObject.Find("ContentDeckCreation").transform, false);
            GameObject.Find("Data").GetComponent<DeckDataHolder>().AddToList(GetComponent<CardDisplay>().myName);
        }
    }
}


