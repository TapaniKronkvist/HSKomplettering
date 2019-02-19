using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullNotification : MonoBehaviour
{
    [SerializeField] public GameObject Notification;

    public void Full()
    {
        if (GameObject.Find("Data").GetComponent<DeckDataHolder>().saveDeck.Count < 30)
        {
            Notification.SetActive(false);
        }
        if (GameObject.Find("Data").GetComponent<DeckDataHolder>().saveDeck.Count == 30)
        {
            Notification.SetActive(true);
        }
    }
}
