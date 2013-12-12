using System;
using System.Xml.Serialization;

[Serializable()]
public class TuntiKirjaus
{
    [XmlElement("koodaaja")]
    public string Koodaaja { get; set; }
    [XmlElement("pvm")]
    public string Pvm { get; set; }
    [XmlElement("aika")]
    public string Aika { get; set; }

    public TuntiKirjaus()
    {
    }
}