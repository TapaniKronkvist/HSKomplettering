using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDeckButton : MonoBehaviour
{
    public void CreateNewDeck (GameObject Deckcreationpanel)
    {
        if (Deckcreationpanel != null)
        {
            bool isActive = Deckcreationpanel.activeSelf;
            Deckcreationpanel.SetActive(!isActive);
        }
    }
}
