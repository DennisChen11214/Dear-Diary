using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

//Speaker group element in the xml file - contains the name of the speaker and the lines they say
public class Speaker
{
    [XmlElement("Name")]
    public string name;

    [XmlElement("Text")]
    public List<TextAndEmotion> text = new List<TextAndEmotion>();
}
