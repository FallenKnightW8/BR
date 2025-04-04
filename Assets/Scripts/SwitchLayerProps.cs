using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayerProps : MonoBehaviour
{
    public Renderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = this.GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("SwitchLayerProps");
        spriteRenderer.sortingLayerName = "Props";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        spriteRenderer.sortingLayerName = "Scene";
    }
}
