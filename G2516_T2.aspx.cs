using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class G2516_T2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        myStuff();
    }

    private void myStuff()
    {
        DataSet tyontekijat = new DataSet();
        string filePath = Server.MapPath(WebConfigurationManager.AppSettings["tyontekijat2013"]).ToString();
        try
        {
            // Bindataan xml tauluun
            tyontekijat.ReadXml(filePath);
            gwTyontekijat.DataSource = tyontekijat.Tables[0].DefaultView;
            gwTyontekijat.DataBind();
        }
        catch 
        { 
        }

        // Lasketaan vakituisten määrä ja heidän palkat yhteen
        string temp;
        int lkm = 0;
        int palkat = 0;
        try
        {
            for (int i = 0; i < tyontekijat.Tables[0].Rows.Count; i++)
            {
                temp = tyontekijat.Tables[0].Rows[i][3].ToString();
                if (temp.Equals("vakituinen"))
                {
                    lkm++;
                    palkat += int.Parse(tyontekijat.Tables[0].Rows[i][4].ToString());
                }
            }
        }
        catch
        {
        }

        lbVakituisia.Text = lkm.ToString();
        lbPalkat.Text = palkat.ToString();
    }
}