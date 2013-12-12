using System;
using System.IO;
using System.Xml.Serialization;

public class Serialisointi
{
    // Serialisointi
    public static void SerialisoiXml(string tiedosto, TuntiKirjaukset tunnit)
    {
        XmlSerializer xs = new XmlSerializer(tunnit.GetType());
        TextWriter tw = new StreamWriter(tiedosto);
        try
        {
            xs.Serialize(tw, tunnit);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            tw.Close();
        }
    }

    // Deserialisointi
    public static void DeSerialisoiXml(string filePath, ref TuntiKirjaukset tunnit)
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(TuntiKirjaukset));
        try
        {
            FileStream xmlFile = new FileStream(filePath, FileMode.Open);
            tunnit = (TuntiKirjaukset)deserializer.Deserialize(xmlFile);
            xmlFile.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    
    // Käyttäjien deserialisointi
    public static void DeSerialisoiKayttajat(string filePath, ref UserLista kayttajat)
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(UserLista));
        try
        {
            FileStream xmlFile = new FileStream(filePath, FileMode.Open);
            kayttajat = (UserLista)deserializer.Deserialize(xmlFile);
            xmlFile.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
}