using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slug_prototype : MonoBehaviour
{
    public bool isIn { get; private set;}
    public bool isMoving { get; private set; }
    public bool IsEnd = true;
    Animator ani;
    const float distance = 5f;
    GameObject Player = null;
    public int D = 2;
    float sX;
    int life = 2;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponentInChildren<Animator>();
        Player = GameObject.Find("PlayerCharacter");
        sX = transform.position.x;
    }

    // Update is called once per frame
    private void Update()
    {
        isMoving = true;
        if (Vector3.Distance(Player.transform.position, transform.position) <= 4)
        {
            slug_out();
        }
        else
        {
            slug_in();

        }
        if (IsEnd == true && isIn == false)
        {
            transform.Translate(1f*Time.deltaTime,0f,0f);
            if (transform.position.x > sX + distance || transform.position.x < sX - distance)
            {
                sX = transform.position.x;
                IsEnd = false;
            }
        }
        else if(IsEnd == false && isIn == false)
        {
            turn();
            IsEnd = true;
        }
    }
    void FixedUpdate()
    {
        if (!isIn)
        {
            if (isMoving) run();
        }
        ani.SetBool("isIn", isIn);
        ani.SetBool("isMoving", isMoving);

    }
    void turn()
    {
        if (transform.rotation.y == 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    void run()
    {
        transform.Translate(Time.fixedDeltaTime * distance, 0f, 0f);
    }
   
    void slug_in()
    {
        isIn = false;

    }
    void slug_out()
    {
        isIn = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponentInChildren<playerHitBox>()) collision.gameObject.GetComponentInChildren<playerHitBox>().BeShot(D, transform.position.x);
        }
    }

    public void lifes()
    {
        life -= 1;
    }
    public void Hit()
    {
        if(isIn == false)
        {
            lifes();
            if (life == 0)
                ani.SetTrigger("Die");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
