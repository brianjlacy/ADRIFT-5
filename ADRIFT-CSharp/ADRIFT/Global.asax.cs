using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;

namespace ADRIFT
{

public class Global_asax
{
    Inherits System.Web.HttpApplication;

    private void Application_Start(object sender, EventArgs e)
    {
        ' Fires when the application is started
    }

    private void Session_Start(object sender, EventArgs e)
    {
        ' Fires when the session is started
    }

    private void Application_BeginRequest(object sender, EventArgs e)
    {
        ' Fires at the beginning of each request
    }

    private void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        ' Fires upon attempting to authenticate the use
    }

    private void Application_Error(object sender, EventArgs e)
    {
        ' Fires when an error occurs
    }

    private void Session_End(object sender, EventArgs e)
    {
        ' Fires when the session ends
    }

    private void Application_End(object sender, EventArgs e)
    {
        ' Fires when the application ends
    }

}
}