using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public string LevelToLoad;
    public void LoadLv(){
        SceneManager.LoadScene(LevelToLoad);
    }
    public void isExit() {
        Application.Quit();
    }

}
