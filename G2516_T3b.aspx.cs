using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class G2516_T3b : System.Web.UI.Page
{
    // Validointikriteerit
    Regex regexNum = new Regex(@"[\d]");
    Regex regexAlph = new Regex(@"^[a-zA-Z0-9]+$");
    Regex regexPvm = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$$");
    Regex regexUser = new Regex(@"^[a-zA-Z0-9]{1,15}$");
    Regex regexPassword = new Regex(@"^[\S*\s*]{1,20}$");
    
    List<TuntiKirjaus> kirjaukset = new List<TuntiKirjaus>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        initStuff();
    }

    private void initStuff()
    {
        if (Session["UserAuthentication"] != null)
        {
            UserLogin.Visible = false;
            btnLogout.Visible = true;
            lbKirjaustenLkm.Visible = true;
            lbTuntikirjanpito.Visible = true;
            lbNimi.Visible = true;
            lbPvm.Visible = true;
            lbAika.Visible = true;
            tbNimi.Visible = true;
            tbPvm.Visible = true;
            tbAika.Visible = true;
            gvKirjaukset.Visible = true;
            lbTunnitYht.Visible = true;
            btnLisaa.Visible = true;
            lbVirhe.Visible = true;

            fillStuff();
        }
        else
        {
            Session["UserAuthentication"] = null;
            UserLogin.Visible = true;
            btnLogout.Visible = false;
            lbKirjaustenLkm.Visible = false;
            lbTuntikirjanpito.Visible = false;
            lbNimi.Visible = false;
            lbPvm.Visible = false;
            lbAika.Visible = false;
            tbNimi.Visible = false;
            tbPvm.Visible = false;
            tbAika.Visible = false;
            gvKirjaukset.Visible = false;
            lbTunnitYht.Visible = false;
            btnLisaa.Visible = false;
            lbVirhe.Visible = false;
        }
    }

    private void fillStuff()
    {
        if (kirjaukset != null)
        {
            kirjaukset = HaeKirjaukset();
            tbNimi.Text = Session["currentUser"].ToString();
            var dateAndTime = DateTime.Now;
            tbPvm.Text = dateAndTime.ToString("dd/MM/yyy");
            OmatKirjaukset();
        }
        else 
        {
            lbKirjaustenLkm.Text = "Tiedoston lataus epäonnistui!";
        }
    }

    private void OmatKirjaukset()
    {
        lbKirjaustenLkm.Text = "Kirjauksia yhteensä: " + kirjaukset.Count().ToString();

        // käyttäjän omat kirjaukset
        List<TuntiKirjaus> temp = new List<TuntiKirjaus>();
        int tunnitYht = 0;
        for (int i = 0; i < kirjaukset.Count; i++)
        {
            if (kirjaukset[i].Koodaaja.Equals(Session["currentUser"]))
            {
                temp.Add(kirjaukset[i]);
                tunnitYht += int.Parse(kirjaukset[i].Aika);
            }
        }
        ViewState["temp"] = temp;
        gvKirjaukset.DataSource = temp;
        gvKirjaukset.DataBind();

        lbTunnitYht.Text = "Tunteja yhteensä: " + (tunnitYht / 60) + "h " + (tunnitYht % 60) + "min";

    }

    // Haetaan kirjaukset xml tiedostosta
    public static List<TuntiKirjaus> HaeKirjaukset()
    {
        try
        {
            TuntiKirjaukset kirjaukset = new TuntiKirjaukset();
            List<TuntiKirjaus> tuntiKirjaukset = new List<TuntiKirjaus>();

            Serialisointi.DeSerialisoiXml(HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["tunnit"]), ref kirjaukset);

            for (int i = 0; i < kirjaukset.KirjauksetLista.Count; i++)
            {
                tuntiKirjaukset.Add(kirjaukset.KirjauksetLista[i]);
            }

            return tuntiKirjaukset;
        }
        catch
        {
            return null;
        }
    }

    // Tallenetaan xml tiedostoon
    public static void TallennaKirjaukset(List<TuntiKirjaus> kirjaukset)
    {
        TuntiKirjaukset lista = new TuntiKirjaukset();
        foreach (TuntiKirjaus item in kirjaukset)
        {
            lista.KirjauksetLista.Add(item);
        }
        Serialisointi.SerialisoiXml(HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["tunnit"]), lista);
    }


    // Login
    protected void UserLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string uName = regexUser.Match(UserLogin.UserName).ToString();
        string uPass = regexPassword.Match(UserLogin.Password).ToString();


        if (AuthenticateUser(uName, uPass))
        {
            e.Authenticated = true;
            Session["currentUser"] = uName;
        }
        else
        {
            e.Authenticated = false;
            Session["currentUser"] = "";
        }
    }

    protected void UserLogin_LoginError(object sender, EventArgs e)
    {
        Session["UserAuthentication"] = null;
    }

    protected void UserLogin_LoggedIn(object sender, EventArgs e)
    {
        Session["UserAuthentication"] = UserLogin.UserName.ToString();
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["UserAuthentication"] = null;
        Response.Redirect(Request.RawUrl);
    }

    // Käyttäjän autentikointi
    public static bool AuthenticateUser(string username, string password)
    {
        // Haetaan käyttäjät xml-tiedostosta
        UserLista users = new UserLista();
        Serialisointi.DeSerialisoiKayttajat(HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["users"]), ref users);
        // Suolaus
        byte[] saltBytes = Encoding.UTF8.GetBytes("suolaa");
        byte[] saltedHashBytesUserName = new HMACMD5(saltBytes).ComputeHash(Encoding.UTF8.GetBytes(username));
        byte[] saltedHashBytesPassword = new HMACMD5(saltBytes).ComputeHash(Encoding.UTF8.GetBytes(password));
        string saltedHashStringUserName = Convert.ToBase64String(saltedHashBytesUserName);
        string saltedHashStringPassword = Convert.ToBase64String(saltedHashBytesPassword);

        // Tarkistetaan käyttäjät
        for (int i = 0; i < users.users.Count; i++)
        {
            if (saltedHashStringUserName == users.users[i].UserName && saltedHashStringPassword == users.users[i].Password)
            {
                return true;
            }
        }
        return false;
    }

    protected void btnLisaa_Click(object sender, EventArgs e)
    {
        TuntiKirjaus uusi = new TuntiKirjaus();
        bool passed = true;
        if (regexAlph.IsMatch(tbNimi.Text) && !string.IsNullOrEmpty(tbNimi.Text))
        {
            uusi.Koodaaja = tbNimi.Text;
        }
        else
        {
            lbVirhe.Text = "Nimi ei kelpaa!";
            passed = false;
        }

        if (regexPvm.IsMatch(tbPvm.Text) && !string.IsNullOrEmpty(tbPvm.Text))
        {
            uusi.Pvm = tbPvm.Text;
        }
        else
        {
            lbVirhe.Text = "Pvm ei kelpaa!";
            passed = false;
        }

        if (regexNum.IsMatch(tbAika.Text) && !string.IsNullOrEmpty(tbAika.Text))
        {
            uusi.Aika = tbAika.Text;
        }
        else
        {
            lbVirhe.Text = "Aika ei kelpaa!";
            passed = false;
        }

        if (passed)
        {
            lbVirhe.Text = "";
            kirjaukset.Add(uusi);
            TallennaKirjaukset(kirjaukset);
            OmatKirjaukset();
        }
    }
}