using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tableSpot : MonoBehaviour
{
    public bool empty;

    // Start is called before the first frame update
    void Start()
    {
        empty = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "AICard")
            empty = false;
        else
        {
            empty = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "AICard")
            empty = true;
    }

}
