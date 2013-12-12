using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

[Serializable()]
[XmlRoot("Users")]
public class UserLista
{
    [XmlElement("User")]
    public List<User> users { get; set; }

    public UserLista()
    {
        users = new List<User>();
    }
}