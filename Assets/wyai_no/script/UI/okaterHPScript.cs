using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class okaterHPScript : MonoBehaviour
{
    PlayerCharacter player;
    public Image image;
    public int playerHp=3;
    public Sprite hud0;
    public Sprite hud1;
    public Sprite hud2;
    public Sprite hud3;

    private void Start()
    {
        player = GameObject.Find("PlayerCharacter").GetComponentInChildren<PlayerCharacter>();
        image = GetComponentInChildren<Image>();
       


    }
    private void Update()
    {
        playerHp = player.hp;
        switch (playerHp)
        {
            case 0:
                image.sprite = hud0 as Sprite;
                break;
            case 1:
                image.sprite = hud1 as Sprite;
                break;
            case 2:
                image.sprite = hud2 as Sprite;
                break;
            case 3:
                image.sprite = hud3 as Sprite;
                break;
        }
    }

}
