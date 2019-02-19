using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    //Viktor
    public string myName;
    CardData myCard;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameObject.Find("CardGameObject").GetComponent<ReadCardData>().cardlist.Count; i++)
        {
            myCard = GameObject.Find("CardGameObject").GetComponent<ReadCardData>().GetCardData(myName);
        }

        if (myCard != null)
        {

            transform.Find("health").GetComponent<Text>().text = $"{myCard.health}";
            transform.Find("mana").GetComponent<Text>().text = $"{myCard.manacost}";
            transform.Find("damage").GetComponent<Text>().text = $"{myCard.attack}";
            transform.Find("description").GetComponent<Text>().text = $"{myCard.cardText}";
            transform.Find("cardName").GetComponent<Text>().text = $"{myCard.name}";
            transform.Find("Frame").GetComponent<Image>().sprite = myCard.cardFrame;
            transform.Find("Portrait").GetComponent<Image>().sprite = myCard.cardPortrait;

        }
    }
    
}
