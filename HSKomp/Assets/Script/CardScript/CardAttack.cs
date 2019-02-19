using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttack : MonoBehaviour
{
    LineRenderer myLineRenderer;
    Vector3 startPos, endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        myLineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        myLineRenderer.SetPositions(new Vector3[] {startPos, endPos});
    }
}
