<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G2516_T3b.aspx.cs" Inherits="G2516_T3b" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tehtävä 3b: Tuntikirjanpito</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Tehtävä 3b: Tuntikirjanpito</h1>
        <img src="Images/PuolipilkunViilaajat.png" />
        
        <!-- Login -->
        <asp:Login ID="UserLogin" runat="server"
            OnAuthenticate="UserLogin_Authenticate"
            OnLoginError="UserLogin_LoginError"
            OnLoggedIn="UserLogin_LoggedIn"
            RememberMeText="Remember me.">
        </asp:Login>
        <br />
        <asp:Button ID="btnLogout" runat="server" Text="Logout"  OnClick="btnLogout_Click" />
        <br /><br />

        <!-- Tuntikirjaukset -->
        <h2><asp:Label ID="lbTuntikirjanpito" runat="server" Text="Tuntikirjanpito"></asp:Label></h2>
        <asp:Label ID="lbKirjaustenLkm" runat="server" Text=""></asp:Label>
        <br /><br />
        <asp:Label ID="lbNimi" runat="server" Text="Nimi:"></asp:Label><br />
        <asp:textbox ID="tbNimi" runat="server"></asp:textbox>
        <br />
        <asp:Label ID="lbPvm" runat="server" Text="Päivämäärä:"></asp:Label><br />
        <asp:textbox ID="tbPvm" runat="server"></asp:textbox>
        <br />
        <asp:Label ID="lbAika" runat="server" Text="Koodausaika (min):"></asp:Label><br />
        <asp:textbox ID="tbAika" runat="server"></asp:textbox> <br />
        <asp:Button ID="btnLisaa" runat="server" OnClick="btnLisaa_Click" Text="Tallenna" />
        <br /><br />
        <asp:GridView ID="gvKirjaukset" runat="server"></asp:GridView>
        <br />
        <asp:Label ID="lbVirhe" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lbTunnitYht" runat="server"></asp:Label>
    </form>
</body>
</html>
