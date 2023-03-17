using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee_Prototype : MonoBehaviour
{
    public bool isMoving { get; private set; }
    Animator ani;
    const float maxVelocity = 4f;
    GameObject Player = null;   
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponentInChildren<Animator>();
        Player = GameObject.Find("PlayerCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = Input.GetAxisRaw("Horizontal") != 0;
        turn();
        if (Vector3.Distance(Player.transform.position, transform.position) <= 7)
            transform.position += Vector3.Normalize(Player.transform.position - transform.position)*Time.deltaTime*maxVelocity;
    }

    void turn()
    {
        if (Player.transform.position.x - transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    void run()
    {
        transform.Translate(Time.fixedDeltaTime * maxVelocity, 0f, 0f);
    }
}
