using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponentManager : MonoBehaviour
{
    public SpriteRenderer rend          {get; private set;}
    public Rigidbody2D rigid            {get; private set;}
    public Collider2D col               {get; private set;}
    public CharacterController con      {get; private set;}
    
    private void Awake()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponentInChildren<Rigidbody2D>();
        col = GetComponentInChildren<Collider2D>();
        con = GetComponentInChildren<CharacterController>();
    }
}
