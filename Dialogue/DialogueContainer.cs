using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

//Loads in a list of all the dialogue elements from the xml file 
[XmlRoot("DialogueCollection")]
public class DialogueContainer
{
    [XmlElement("Dialogue")]
    public List<Dialogue> dialogues = new List<Dialogue>();

    public static DialogueContainer Load(){
        TextAsset _xml = Resources.Load<TextAsset>("test");
        using(TextReader textReader = new StringReader(_xml.text))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DialogueContainer));
         
            DialogueContainer XmlData = serializer.Deserialize(textReader) as DialogueContainer;
 
            return XmlData;
        }
    }

}
