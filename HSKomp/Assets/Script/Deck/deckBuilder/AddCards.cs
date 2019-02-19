using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCards : MonoBehaviour
{
    //Viktor
    public List<GameObject> cardGrid = new List<GameObject>();

    public Transform LibraryContent;

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject card = Resources.Load<GameObject>("Menu/Deck/CardPrefab");

        for (int i = 0; i < GameObject.Find("CardGameObject").GetComponent<ReadCardData>().cardlist.Count; i++)
        {
            cardGrid.Add(Instantiate(card));
            cardGrid[i].transform.SetParent(LibraryContent, false);
            
        }
        for (int i = 0; i < GameObject.Find("CardGameObject").GetComponent<ReadCardData>().cardlist.Count; i++)
        {
            print(GameObject.Find("CardGameObject").GetComponent<ReadCardData>().cardlist[i].name);
            cardGrid[i].GetComponent<CardDisplay>().myName = GameObject.Find("CardGameObject").GetComponent<ReadCardData>().cardlist[i].name;
            cardGrid[i].transform.SetParent(transform, false);
        }
    }
}
        