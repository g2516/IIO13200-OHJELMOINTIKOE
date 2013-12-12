using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable()]
[XmlRoot("Tunnit")]
public class TuntiKirjaukset
{
    [XmlElement("Kirjaus")]
    public List<TuntiKirjaus> KirjauksetLista { get; set; }

    public TuntiKirjaukset()
    {
        KirjauksetLista = new List<TuntiKirjaus>();
    }
}