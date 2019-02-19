using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateDeck : MonoBehaviour
{

    string[] deckArray;
    List<string> myDeck = new List<string>();
    [SerializeField] string deckName;
    [SerializeField] List<string> shuffledDeck = new List<string>();


    // Start is called before the first frame update
    public void ShuffleDeck()
    {
        deckArray = txtToString.Convert("Player1", "User");
        for (int i = 0; i < deckArray.Length; i++)
        {
            myDeck.Add(deckArray[i]);
        }

        while (myDeck.Count > 0)
        {
            int random = Random.Range(0, myDeck.Count);
            shuffledDeck.Add(myDeck[random]);
            myDeck.RemoveAt(random);
        }

        for (int i = 0; i < transform.childCount; i++)
        {

            transform.GetChild(i).GetComponent<Card>().cardName = shuffledDeck[i];
        }

    }
}
