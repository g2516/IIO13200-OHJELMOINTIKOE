using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

[Serializable()]
public class User
{
    [XmlElement("Name")]
    public string Name { get; set; }
    [XmlElement("UserName")]
    public string UserName { get; set; }
    [XmlElement("Password")]
    public string Password { get; set; }

    public User()
    {
    }
}