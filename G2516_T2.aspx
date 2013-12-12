<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G2516_T2.aspx.cs" Inherits="G2516_T2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tehtävä 2: Web.configin käyttö ja XML-tiedoston esittäminen</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Tehtävä 2: Web.configin käyttö ja XML-tiedoston esittäminen</h1>
        <asp:GridView ID="gwTyontekijat" runat="server"></asp:GridView>
        Vakituisten määrä:
        <asp:Label ID="lbVakituisia" runat="server" Text=""></asp:Label>
        <br />
        Vakituisten palkat:
        <asp:Label ID="lbPalkat" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
