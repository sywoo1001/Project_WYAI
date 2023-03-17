using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private PlayerComponentManager pcm;
    private PlayerCharacter pc;
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        pcm = GetComponentInChildren<PlayerComponentManager>();
        ani = GetComponentInChildren<Animator>();
        pc = GetComponentInChildren<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetFloat("ladderVelocity", Input.GetAxis("Vertical"));
        ani.SetBool("isMoving", pc.isMoving);
        ani.SetBool("OnAir", !pc.OnGround);
        ani.SetFloat("VelocityY", pcm.rigid.velocity.y);
    }
    public void OnLadderEnter()
    {
        ani.SetTrigger("ladderEnter");
    }
    public void OnLadderExit()
    {
        ani.SetTrigger("ladderExit");
    }
    public void Duck()
    {
        ani.SetTrigger("Duck");
    }
    public void Die()
    {
        ani.SetTrigger("die");
    }
}
