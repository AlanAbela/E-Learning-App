<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Topic.aspx.cs" Inherits="UserInterface_Topic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page-topic">
            <!-- Page Header -->
            <div class="page-header" style="width: 100%; text-align: center;">
                <h1>
                    <asp:Label ID="lblLessonTitle" runat="server" Text="Header test"></asp:Label>
                </h1>
            </div>
            <!-- Page Body -->
            <div id="topic-body">
                <div class="topic-info">
                    <asp:Label ID="lblTopicText" runat="server" Text="Test Text"></asp:Label>
                </div>
             <div class ="video-holder">
                 <div class="spacer"></div>
                    <iframe id="videoSource" runat="server" class="video"
                        src="http://www.youtube.com/embed/XGSy3_Czz8k?autoplay=0"></iframe>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
