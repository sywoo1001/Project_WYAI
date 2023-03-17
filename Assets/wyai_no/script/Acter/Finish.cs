using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite close;
    public Sprite Open;
    public SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.sprite = Open;
            Invoke("Endgame", 1f);
        }
    }
    public void Endgame()
    {
        GameManagerScript.Instance.OpenSelectStage();
    }
}
