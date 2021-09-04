using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//Dialogue group element in the xml file - contains who's speaking and the name of the dialogue
public class Dialogue
{
    [XmlElement("Speaker")]
    public List<Speaker> speakers = new List<Speaker>();
    [XmlElement("DialogueName")]
    public string dialogueName;
}
