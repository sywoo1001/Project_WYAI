using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slug_hitbox : MonoBehaviour
{
    slug_prototype slgHit;
    // Start is called before the first frame update
    void Start()
    {
        slgHit = gameObject.transform.parent.GetComponentInChildren<slug_prototype>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("carrot"))
        {
            slgHit.Hit();
        }
    }
}
