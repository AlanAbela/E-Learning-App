<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
       <script type="text/javascript">

        // Check if username is valid.
        function ValidateForm(source, args)
        {
            args.IsValid = true;

            var username = document.getElementById('<%= txtUsername.ClientID %>');
            var password = document.getElementById('<%= txtPassword.ClientID %>');
          
            source.innerHTML = "";

            if(username.value.length < 5)
            {
                args.IsValid = false;
                source.innerHTML = "Username must be 5 or more characters long. ";
            }
            
            if(password.value.length < 8)
            {
                args.IsValid = false;
                source.innerHTML = source.innerHTML + "Password must be 8 or more characters long. ";
            }

            if(hasWhiteSpace(password.value))
            {
                args.IsValid = false;
                source.innerHTML = source.innerHTML + "Password must not contain white spaces. ";
            }        
        }

        // Check for white spaces in password field.
        function hasWhiteSpace(val)
        {
            return val.indexOf(' ') != -1;
        }
    </script>
</head>
<body>
  <form id="form1" runat="server">
   <div id="form" class="main-login">
    <div class="form-center medium">
        <div>
            <asp:Label ID="lblTitle" runat="server" Text="User Login" Font-Size="XX-Large" Font-Bold="true" Width="174px" Height="46px"></asp:Label>
        </div>
        <br />
        <div>
            <asp:TextBox ID="txtUsername" runat="server" placeholder="User name" CssClass="form-control"></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <br />
        <div class="left-align" style="float:left;">
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary"/>
        </div>
        <div style="text-align:right;">
        <asp:Label runat="server" Text="create account?" Font-Size="Small"><a href="Register.aspx">create account?</a></asp:Label>
        </div>
        </div>
     </div>
         <div style="width:100%; text-align:center; display:inline-block;">
            <asp:CustomValidator ID="valForm" runat="server" OnServerValidate="valForm_ServerValidate" ClientValidationFunction="ValidateForm" CssClass="alert alert-danger" Display="Dynamic"></asp:CustomValidator>
    </div>
    </form>
</body>
</html>
