using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

//Text and emotion group in the xml file - contains the sprite expression and what the speaker says
//Also contains whether or not there's a choice option or if there's a condition for the line to show
public class TextAndEmotion
{
    [XmlElement("Emotion")]
    public string emotion;

    [XmlElement("Line")]
    public string line;
    [XmlElement("Choice")]
    public string choice;
    [XmlElement("Condition")]
    public string condition;
}
