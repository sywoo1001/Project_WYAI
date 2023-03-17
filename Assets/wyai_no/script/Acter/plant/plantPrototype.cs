using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantPrototype : MonoBehaviour
{
    Animator ani;
    GameObject Player = null;
    public GameObject Slug;
    public int D = 1;
    bool isAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponentInChildren<Animator>();
        Player = GameObject.Find("PlayerCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        turn();
        if (Vector3.Distance(Player.transform.position, transform.position) <= 9)
            StartCoroutine("attack");
    }
    void turn()
    {
        if (Player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    IEnumerator attack()
    {
        if (isAttack == true)
        {
            ani.SetTrigger("atk");
           
            isAttack = false;
            yield return new WaitForSeconds(3f);
            isAttack = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponentInChildren<playerHitBox>()) collision.gameObject.GetComponentInChildren<playerHitBox>().BeShot(D, transform.position.x);
        }

    }
    void Shoot()
    {
        Instantiate(Slug, transform.position, transform.rotation);
    }
}
