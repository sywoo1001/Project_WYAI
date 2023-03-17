using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : Singleton<GameManagerScript>
{
    protected GameManagerScript() { }
    SettingVal setting;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (!PlayerPrefs.HasKey("width"))
        {
            PlayerPrefs.SetInt("width", 1920);
        }
        if (!PlayerPrefs.HasKey("hight"))
        {
            PlayerPrefs.SetInt("hight", 1080);
        }
        if (!PlayerPrefs.HasKey("fullScreen"))
        {
            PlayerPrefs.SetInt("fullScreen", 0);
        }
        
    }
    public void ChangeRes(int w,int h)
    {
        Screen.SetResolution(w, h, PlayerPrefs.GetInt("fullScreen")==0);
        PlayerPrefs.SetInt("width", w);
        PlayerPrefs.SetInt("hight", h);
    }
    public void ChangeRes(bool f)
    {
        Screen.SetResolution(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("hight"), f);
    }
    public void ChangeRes(int w, int h, bool f)
    {
        Screen.SetResolution(w, h, f);
        PlayerPrefs.SetInt("width",w);
        PlayerPrefs.SetInt("hight", h);
    }
    public void OpenSelectStage()
    {
        SceneManager.LoadScene("choose stage");
    }
    
    

}
