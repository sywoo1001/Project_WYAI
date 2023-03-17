using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    Vector2 collisionNormal;
    Vector2 playerFront;
    public bool InAir;
    bool isTouching;
    public float MaxDeg=45;
    const float jumpForce=3;
    PlayerComponentManager pcm;
    public bool isMoving { get; private set; }

    const float maxVelocity=5f;
    // Start is called before the first frame update
    void Start()
    {
        pcm = GetComponentInChildren<PlayerComponentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = Input.GetAxisRaw("Horizontal")!=0;
        Debug.DrawLine(transform.position, transform.position + (Vector3)( playerFront));
        InAir=(Mathf.Abs(Vector2.Dot(transform.up, collisionNormal))>=Mathf.Sin(Mathf.Deg2Rad*MaxDeg)&&isTouching)||pcm.rigid.velocity.y==0;
    }
    void FixedUpdate()
    {
        if (isMoving) run();
        turn();
        jump();
    }
   
    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouching = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        isTouching = true;
        playerFront = Vector3.Cross((Vector3)collision.contacts[0].normal, transform.forward);
        float tmp = playerFront.x;
        playerFront.x = tmp * transform.right.x;

        
        collisionNormal = collision.contacts[0].normal;
    }
    void turn()
    {
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }else if(Input.GetAxisRaw("Horizontal") > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    void run()
    {
        if (InAir) transform.Translate((Time.fixedDeltaTime * maxVelocity * playerFront));
        else if(!isTouching)transform.Translate(maxVelocity * Time.fixedDeltaTime,0,0);
    }
    void jump()
    {
        
        if (InAir)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                pcm.rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}
