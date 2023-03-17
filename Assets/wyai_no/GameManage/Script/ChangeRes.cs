using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRes : MonoBehaviour
{
    public void Changeres(int i)
    {
        switch (i)
        {
            case 0:
                GameManagerScript.Instance.ChangeRes(1920, 1080);
                break;
            case 1:
                GameManagerScript.Instance.ChangeRes(1280, 720);
                break;
            case 2:
                GameManagerScript.Instance.ChangeRes(1600, 900);
                break;
            case 3:
                GameManagerScript.Instance.ChangeRes(1024, 768);
                break;
            case 4:
                GameManagerScript.Instance.ChangeRes(1360, 768);
                break;
            case 5:
                GameManagerScript.Instance.ChangeRes(800, 600);
                break;

        }
        
    }
}
