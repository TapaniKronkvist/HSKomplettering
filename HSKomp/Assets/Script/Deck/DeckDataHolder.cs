using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class DeckDataHolder : MonoBehaviour
{
    //Viktor
    public List<string> saveDeck;
    [SerializeField] private string username;
    [SerializeField] private string userDeck;
    [SerializeField] private string userdir;
    [SerializeField] public GameObject DeckButton;

    // Start is called before the first frame update
    void Start()
    {
        //string userdir = 
        saveDeck = new List<string>();
        Directory.CreateDirectory("testusers\\");
    }

    public void AddToList(string newCard)
    {
        saveDeck.Add(newCard);
        
    }

    public void RemoveFromlist(string removeCard)
    {
        for (int i = 0; i < saveDeck.Count; i++)
        {
            if (saveDeck[i] == removeCard)
            {
                saveDeck.RemoveAt(i);
                break;
            }
        }
    }

    public void Done()
    {
        
    }

    public void Save()
    {
        //\users\testUser\testUserDeck\testUserDeck.txt".
        //\users\testUser\testUserDeck\".

        userDeck = "testUserDeck";
        //userdir = @"users/" + username + "/" + userDeck + "/";
        username = "testUser";
        userdir = @"testusers/" + username + "/" + userDeck + "/";

        //string userDeck = username + /*ChangeDeckName(changeDeckName)*/ ".txt";
        
        string path = Path.Combine(userdir, username + ".txt");
        Directory.CreateDirectory(userdir);
        if (saveDeck.Count < 30)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    // saveDeck.ForEach(writer.Write);
                    foreach (string card in GameObject.Find("Data").GetComponent<DeckDataHolder>().saveDeck)
                    {
                        writer.WriteLine(card);
                    }
                    writer.Close();
                    Done();
                }
            }
            else
            {
                Debug.LogError("Deck already exists!");
            }
        }
        else
        {
            Debug.LogError("Not enough cards!");
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void Load()
    {
        
        string userdir = @"users/" + username + "/";
        string userDeck = username + ".txt";
        string path = Path.Combine(userdir, username + ".txt");
        if (File.Exists(path))
        {
            using (StreamWriter writer = File.AppendText(path))
            {
                
            }
        }
    }

    public void ChangeDeckName(GameObject changeDeckName)
    {
        string path = Path.Combine(userdir, username/*, userDeck*/ + ".txt");
        using (StreamWriter deckNameWriter = File.AppendText(path))
        {
            deckNameWriter.WriteLine(changeDeckName.GetComponent<InputField>().text);
            changeDeckName.GetComponent<InputField>().text = "";
            userDeck = changeDeckName.GetComponent<InputField>().text;
        }
    }
    
}
