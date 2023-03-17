using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Xml();
    }

    void Xml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("ResolutionInfo");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("ResolutionInfo/Resolution");

        foreach (XmlNode node in nodes)
        {
            Debug.Log("width :: " + node.SelectSingleNode("width").InnerText);
            Debug.Log("height :: " + node.SelectSingleNode("height").InnerText);
        }
    }
}
