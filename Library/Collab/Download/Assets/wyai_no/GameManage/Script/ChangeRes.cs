using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRes : MonoBehaviour
{
    public string wid;
    public string hei;
    public void Changeres()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("ResolutionInfo");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("ResolutionInfo/Resolution");
        XmlNode resolution = nodes[0];

        resolution.SelectSingleNode("width").InnerText = wid;
        resolution.SelectSingleNode("height").InnerText = hei;

        foreach (XmlNode node in nodes)
        {
            Debug.Log("width :: " + node.SelectSingleNode("width").InnerText);
            Debug.Log("height :: " + node.SelectSingleNode("height").InnerText);
        }

        xmlDoc.Save("./Assets/wyai_no/Resources/ResolutionInfo.xml");
    }
}
