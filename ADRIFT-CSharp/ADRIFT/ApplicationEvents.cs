using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
Namespace My

    ' The following events are availble for MyApplication:
    '
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication;

        private void MyApplication_UnhandledException(object sender, Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
        {

            ErrMsg("Critical Error", e.Exception);

            If MsgBox("Would you like to attempt to continue", MsgBoxStyle.OkCancel | MsgBoxStyle.Question) = MsgBoxResult.Ok Then e.ExitApplication = false
        }

        protected override void OnInitialize(System.Collections.ObjectModel.ReadOnlyCollection(Of String commandLineArgs)
        {

            ' Set the display time to 5000 milliseconds (5 seconds).

            'Me.MinimumSplashScreenDisplayTime = 5000

            return MyBase.OnInitialize(commandLineArgs);

        }

    }


}


}