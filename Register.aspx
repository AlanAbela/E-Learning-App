<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="UserInterface_Register" %>

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
            var confirmPassword = document.getElementById('<%=txtConfirmPassword.ClientID %>')
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
            
            if(args.IsValid && password.value != confirmPassword.value)
            {
                args.IsValid = false;
                source.innerHTML = source.innerHTML + "Passwords do not match. ";
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
        
            <div id="form" class="main">
             <div id="cont" class="form-center medium">
                 <div>
                     <asp:Label ID="lblTitle" runat="server" Text="Sign Up" Font-Size="XX-Large" Font-Bold="true" Width="174px" Height="46px"></asp:Label>
                 </div>
                 <div class="underline"></div>
                 <br />
                 <div>
                  <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
                     <br />
                  <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
                 <br />
                <div>
                  <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                    <br />
                  <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                  <br />
                 <div>
                  <asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label>
                     <br />
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                 </div>
                 <br />
               <div class="right-align">
                  <asp:Button ID="btnRegister" runat="server" Text="Sign Up" OnClick="btnRegister_Click" CssClass="btn btn-primary"/>
               </div>
                 <br />
                 <br />
                 <div class="underline"></div>
                 <br />
               <div class="right-align">
                   <asp:Label ID="Label4" runat="server" Text="Already registered?"></asp:Label>
                   <asp:Button ID="btnGoToLogin" runat="server" Text="Go to log in" CssClass="btn btn-info" CausesValidation="false" OnClick="btnGoToLogin_Click" />
               </div>
            </div>
          </div>
            <div>
         </div>
        <div style="width:100%; text-align:center; display:inline-block;">
            <asp:CustomValidator ID="valForm" runat="server"  OnServerValidate="valForm_ServerValidate" ClientValidationFunction="ValidateForm" CssClass="alert alert-danger" Display="Dynamic"></asp:CustomValidator>
         </div>
    </form>
</body>

</html>
