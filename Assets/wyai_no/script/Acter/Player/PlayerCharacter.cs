using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    bool alive = true;
    bool ShootAble=true;
    public GameObject carrot;
    enum State { idle, ladder};
    State state = State.idle;
    Vector2 playerFront;
    Vector2 lastNormal;
    bool istouching;
    bool OnLadder;
    private bool invincible = false;
    public float invincibliltyTime = 3f;
    public int hp = 3;
    float Delay = 0.5f;
    public bool OnGround;
    public float MaxDeg=45;
    const float jumpForce=8;
    const float maxVelocity=5f;
    public LayerMask IgnorePlayer;
    public LayerMask Ladder;
    GameObject ladder;
    PlayerComponentManager pcm;
    PlayerAnimationManager pam;
    public bool isMoving { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        pam = GetComponentInChildren<PlayerAnimationManager>();
        pcm = GetComponentInChildren<PlayerComponentManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastNormal = collision.contacts[0].normal;
        if (collision.contacts.Length>1)
        {
            for (int i = 1; i < collision.contacts.Length; i++)
            {
                if (lastNormal.y > collision.contacts[i].normal.y)
                {
                    lastNormal = collision.contacts[i].normal;
                }
            } 
        }
        
        istouching = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
        istouching = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (alive)
        {
            OnGround = checkOnAir();
            playerFront = getPlayerFront();
            isMoving = Input.GetAxisRaw("Horizontal") != 0;
            Debug.DrawLine(transform.position, transform.position + (Vector3)(playerFront));
            if (checkLadder(ref ladder))
            {
                OnLadder = ((ladder != null) && Input.GetKey(KeyCode.W));
            }
            else OnLadder = false;
            if (Input.GetKeyDown(KeyCode.J))
            {
                Shoot();

            }
            if (OnGround)
            {
                pcm.rigid.sharedMaterial.friction = 20f;
            }
            else pcm.rigid.sharedMaterial.friction = 5f;
            switch (state)
            {
                case State.idle:
                    pcm.rigid.gravityScale = 1f;
                    pcm.col.isTrigger = false;
                    break;
                case State.ladder:
                    pcm.rigid.gravityScale = 0f;
                    pcm.col.isTrigger = true;
                    pcm.rigid.velocity = new Vector3(0, 0, 0);

                    break;
            } 
        }


    }
    void Shoot()
    {

        
        if (ShootAble)
        {
            Instantiate(carrot,transform.position,transform.rotation);
            StartCoroutine(ShootDelay());
        }
        
    }
    void FixedUpdate()
    {
        if (alive)
        {
            switch (state)
            {
                case State.idle:
                    if (isMoving) run();
                    turn();
                    jump();
                    if (OnLadder)
                    {
                        state = State.ladder;
                        int x = ladder.GetComponentInChildren<Tilemap>().WorldToCell(transform.position).x;
                        transform.position = new Vector2(ladder.GetComponentInChildren<Tilemap>().CellToWorld(new Vector3Int(x, 0, 0)).x + ladder.GetComponentInChildren<Tilemap>().cellSize.x * 0.5f, transform.position.y);
                        pam.OnLadderEnter();

                    }
                    break;
                case State.ladder:
                    Debug.Log("asdf");
                    transform.Translate(0f, Input.GetAxisRaw("Vertical") * Time.deltaTime * maxVelocity, 0);
                    if (Input.GetKeyDown(KeyCode.Space) || ladder == null || (OnGround && Input.GetKey(KeyCode.S)))
                    {
                        state = State.idle;
                        pam.OnLadderExit();
                    }
                    break;
            }

        }
    }
   
    
    void turn()
    {
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    void run()
    {
        if (OnGround) {
            if(Mathf.Sin(MaxDeg*Mathf.Deg2Rad)>=playerFront.y&&playerFront.x>=0)
            transform.Translate((Time.fixedDeltaTime * maxVelocity * playerFront));
        }
        else
        {
            if(!istouching)transform.Translate(Time.deltaTime * maxVelocity, 0, 0);
            else if(Mathf.Sin(MaxDeg * Mathf.Deg2Rad)<Mathf.Abs(lastNormal.y)){

                Vector2 result = Vector3.Cross(lastNormal, transform.forward);
                float tmp = result.x;
                result.x = Mathf.Abs(tmp);
                transform.Translate(Time.deltaTime * maxVelocity * result);
            }
        }
    }
    public void BeShot(int D, float PM)
    {
        var x = 0;
        if (pcm.rigid.gameObject.transform.position.x > PM)
          x = 5;
        else if (pcm.rigid.gameObject.transform.position.x < PM)
          x = -5;
        if(!invincible)
        {
            
            hp -= D;
            if (hp <= 0)
            {
                playerDie();
            }
            else
            {
                pam.Duck();
                pcm.rigid.velocity = new Vector2(x, 5);
                StartCoroutine(Invulnerability());
            }
            
        }
    }
    void playerDie()
    {
        alive = false;
        pam.Die();
        Invoke("GameOver", 0.5f);
    }
    void GameOver()
    {
        jump();
        pcm.col.enabled = false;
        Invoke("restart", 0.5f);
    }
    void restart()
    {
        SceneManager.LoadScene("FirstStage(+Basic)");
    }
    IEnumerator Invulnerability()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibliltyTime);
        invincible = false;
    }
    IEnumerator ShootDelay()
    {
        ShootAble = false;
        yield return new WaitForSeconds(Delay);
        ShootAble = true;
    }
    void jump()
    {
        
        if (OnGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                pcm.rigid.velocity =new Vector2(0,jumpForce);
            }
        }
    }
    
    bool checkOnAir()
    {
        RaycastHit2D hitInfo1 = Physics2D.Linecast(transform.position, transform.position+ Vector3.down*0.8f,IgnorePlayer);
        RaycastHit2D hitInfo2 = Physics2D.Linecast(transform.position + transform.right.x * new Vector3(0.3f, 0f, 0f), transform.position + transform.right.x * new Vector3(0.3f, 0f, 0f) + Vector3.down * 0.8f,IgnorePlayer);
        RaycastHit2D hitInfo3 = Physics2D.Linecast(transform.position + transform.right.x * new Vector3(-0.4f,0f, 0f), transform.position + transform.right.x * new Vector3(-0.4f, 0f, 0f)+ Vector3.down * 0.8f,IgnorePlayer);
        
        Debug.DrawRay(transform.position, new Vector2(0f,-0.8f), Color.red);
        Debug.DrawRay(transform.position + transform.right.x * new Vector3(-0.4f, 0f, 0f), new Vector2(0f, -0.8f), Color.red);
        Debug.DrawRay(transform.position + transform.right.x * new Vector3(0.25f, 0f, 0f), new Vector2(0f, -0.8f), Color.red);
        return hitInfo1 || hitInfo2 || hitInfo3;
    }
    bool checkLadder(ref GameObject LadderGameObject)
    {
        RaycastHit2D hitInfo1 = Physics2D.Linecast(transform.position, transform.position + Vector3.down * 0.8f, Ladder);
        RaycastHit2D hitInfo2 = Physics2D.Linecast(transform.position + transform.right.x * new Vector3(0.3f, 0f, 0f), transform.position + transform.right.x * new Vector3(0.3f, 0f, 0f) + Vector3.down * 0.8f, Ladder);
        RaycastHit2D hitInfo3 = Physics2D.Linecast(transform.position + transform.right.x * new Vector3(-0.4f, 0f, 0f), transform.position + transform.right.x * new Vector3(-0.4f, 0f, 0f) + Vector3.down * 0.8f, Ladder);
        if (hitInfo1 || hitInfo2 || hitInfo3)
        {
            if (hitInfo1.collider != null)
            {
                LadderGameObject = hitInfo1.collider.gameObject;
            }
            else if (hitInfo2.collider != null)
            {
                LadderGameObject = hitInfo2.collider.gameObject;
            }
            else if (hitInfo3.collider != null)
            {
                LadderGameObject = hitInfo3.collider.gameObject;
            } 
        }
        else LadderGameObject = null;
        return hitInfo1 || hitInfo2 || hitInfo3;
    }
    Vector2 getPlayerFront()
    {
        Vector2 result;
        RaycastHit2D hitInfo = Physics2D.CircleCast((Vector2)transform.position, 0.4f, Vector2.down, 2f,IgnorePlayer);
        
        if (OnGround && hitInfo)
        {
            result = Vector3.Cross(hitInfo.normal, transform.forward);
            float tmp = result.x;
            result.x = tmp * transform.right.x;
            return result;
        }
       
        else return transform.right;
    }
   
}
