using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    
    Res resolution;
    struct Res
    {
        public int h;
        public int w;
        public bool fullScreen;
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        
    }
    // Start is called before the first frame update
    void changeRes()
    {
        XmlDocument setting = new XmlDocument();
        setting.LoadXml("./Assets/wyai_no/Resources/ResolutionInfo.xml");
        resolution.h = XmlConvert.ToInt32(setting.SelectSingleNode("width").InnerText);
        resolution.w = XmlConvert.ToInt32(setting.SelectSingleNode("height").InnerText);
        Screen.SetResolution(resolution.w, resolution.h, resolution.fullScreen);
    }
    void changeFullScreen()
    {
        resolution.fullScreen = !resolution.fullScreen;
        Screen.SetResolution(resolution.w, resolution.h, resolution.fullScreen);
    }
}
