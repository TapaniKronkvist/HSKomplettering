using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClickDeckCard : MonoBehaviour
{
    //Viktor
    public void PressDeckCard()
    {
        GameObject.Find("Data").GetComponent<DeckDataHolder>().RemoveFromlist(transform.GetChild(0).GetComponent<Text>().text);
        Destroy(gameObject);
    }
}
