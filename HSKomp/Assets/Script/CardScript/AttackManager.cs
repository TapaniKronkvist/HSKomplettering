using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    [SerializeField] GameObject CardOne;
    [SerializeField] GameObject CardTwo;
    [SerializeField] bool firstSelected;


    string outputString;

    RaycastHit mouseOut;
    // Start is called before the first frame update
    void Start()
    {
        firstSelected = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out mouseOut);
            if (!firstSelected && mouseOut.collider != null && mouseOut.collider.tag == "PlayerOneCard" && mouseOut.collider.GetComponent<Card>().GetState() == Card.CardState.Played && !mouseOut.collider.GetComponent<Card>().sleep && !mouseOut.collider.GetComponent<Card>().hasAttacked)
            {
                print("Select card one");
                CardOne = mouseOut.collider.gameObject;
                firstSelected = true;
            }
            if (firstSelected && mouseOut.collider != null)
            {
                if (mouseOut.collider.GetComponent<AICard>() != null)
                {
                    
                    CardTwo = mouseOut.collider.gameObject;
                    CardTwo.GetComponent<AICard>().hp -= CardOne.GetComponent<Card>().attack;
                    CardOne.GetComponent<Card>().hp -= CardTwo.GetComponent<AICard>().attack;
                    if(CardOne.GetComponent<Card>().hp <= 0)
                    {
                        Destroy(CardOne);
                    }
                    CardOne.GetComponent<Card>().hasAttacked = true;
                    outputString = $"{CardOne.GetComponent<Card>().cardName} attacked {CardTwo.GetComponent<AICard>().cardName} and dealt {CardOne.GetComponent<Card>().attack} damage.";
                    print(outputString);
                    resetCards();
                }
                else if (mouseOut.collider.transform.parent != null && mouseOut.collider.transform.parent.name == "AIEasy")
                {
                    mouseOut.collider.transform.parent.GetComponent<AI>().hp -= CardOne.GetComponent<Card>().attack;
                    outputString = $"{CardOne.GetComponent<Card>().cardName} attacked Opponent hero and dealt {CardOne.GetComponent<Card>().attack} damage.";
                    print(outputString);
                    resetCards();
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            resetCards();
        }
    }
    void resetCards()
    {
        CardTwo = null;
        CardOne = null;
        firstSelected = false;
    }
}
