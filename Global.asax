<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
       // Code that runs when an unhandled error occurs
        if (HttpContext.Current.Server.GetLastError() != null)
        {
            // Reference the last exception.
            Exception exception = HttpContext.Current.Server.GetLastError().GetBaseException();

            // Reference url of this request.
            string page = Request.Url.ToString();

            // Reference exception message.
            string message = exception.Message;

            // Referance stacktrace.
            string stackTrace = exception.StackTrace;

            // Reference query string if present.
            string query = Request.QueryString.ToString();

            // Log the referenced data.
           ErrorMessage.LogExceptions(page, message, stackTrace, query);
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started.
        if(Context.Session["UserID"] != null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    void Session_End(object sender, EventArgs e)
    {


        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
