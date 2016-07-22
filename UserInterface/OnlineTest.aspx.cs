using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserInterface_OnlineTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_Click(object sender, EventArgs e)
    {
        if(mv.ActiveViewIndex == 0)
        {
            mv.ActiveViewIndex = 1;
        }

       else if(mv.ActiveViewIndex == 1)
        {
            mv.ActiveViewIndex = 0;
        }

        Control c = mv.Controls[0];
        
    }
}