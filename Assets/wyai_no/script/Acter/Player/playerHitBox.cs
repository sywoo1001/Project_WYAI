using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitBox : MonoBehaviour
{
    PlayerCharacter parent;
    private void Start()
    {
        parent = gameObject.transform.parent.GetComponentInChildren<PlayerCharacter>();
    }
    public void BeShot(int D,float PM)
    {
        parent.BeShot(D, PM);
    }

}
