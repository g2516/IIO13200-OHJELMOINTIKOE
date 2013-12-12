<%@ Page Language="C#" AutoEventWireup="true" CodeFile="G2516_Index.aspx.cs" Inherits="G2516_Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tehtävä 1: Index -sivu</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Tehtävä 1: Index -sivu</h1>
        Petri Matilainen <br />
        G2516<br />
        <img src="Images/OmaKuva.PNG" />
        <br />
        <asp:HyperLink id="pisteet" Text="Pisteet" NavigateUrl="~/G2516_Pisteet.aspx" runat="server" />
        <br />
        <br />
        <asp:HyperLink id="teht2" Text="Tehtävä 2" NavigateUrl="~/G2516_T2.aspx" runat="server" />
        <br />
        <asp:HyperLink id="teht3b" Text="Tehtävä 3b" NavigateUrl="~/G2516_T3b.aspx" runat="server" />
        <br />
        <asp:HyperLink id="teht4" Text="Tehtävä 4 (jäi tekemättä)" NavigateUrl="~/G2516_T4.aspx" runat="server" />
        <br />
    </div>
    </form>
</body>
</html>
