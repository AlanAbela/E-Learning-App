<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="UserInterface_Result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery-3.1.0.min.js"></script>
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="position: relative; width: 75%; height: 75%; margin-left: auto; margin-right: auto; top: 50px;">
            <asp:GridView ID="gvResult" runat="server" AutoGenerateColumns="true" ShowHeader="true" CssClass="table table-striped" OnRowDataBound="gvResult_RowDataBound">
                <Columns>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
