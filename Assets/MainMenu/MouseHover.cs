using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public Sprite sprite;
    public Sprite highlightSprite;

    void OnMouseOver()
    {
        transform.GetComponent<SpriteRenderer>().sprite = highlightSprite;
    }

    void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
