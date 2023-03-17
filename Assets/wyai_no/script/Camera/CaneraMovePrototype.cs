using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneraMovePrototype : MonoBehaviour
{
    GameObject playerCharacter;
    public string playerName;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter=GameObject.Find(playerName);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y,-10f);
    }
}
