using System;
using System.Collections.Generic;
using System.Linq;

namespace ADRIFT
{
#if _MyType <> "Empty"

Namespace My
    ''' <summary>
    ''' Module used to define the properties that are available in the My Namespace for Web projects.
    ''' </summary>
    ''' <remarks></remarks>
    <Global.Microsoft.VisualBasic.HideModuleName()> _;
    internal static class MyWebExtension
    {
        private New ThreadSafeObjectProvider<Global.Microsoft.VisualBasic.Devices.ServerComputer> s_Computer;
        private New ThreadSafeObjectProvider<Global.Microsoft.VisualBasic.ApplicationServices.WebUser> s_User;
        private New ThreadSafeObjectProvider<Global.Microsoft.VisualBasic.Logging.AspLog> s_Log;
        private New ThreadSafeObjectProvider<MyApplication> s_Application;

        ''' <summary>
        ''' Returns information about the current application.
        ''' </summary>
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _;
        internal MyApplication Application { get; }
            {
                get
                {
                return s_Application.GetInstance();
            }
        }

        ''' <summary>
        ''' Returns information about the host computer.
        ''' </summary>
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _;
        internal Global.Microsoft.VisualBasic.Devices.ServerComputer Computer { get; }
            {
                get
                {
                return s_Computer.GetInstance();
            }
        }
        ''' <summary>
        ''' Returns information for the current Web user.
        ''' </summary>
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _;
        internal Global.Microsoft.VisualBasic.ApplicationServices.WebUser User { get; }
            {
                get
                {
                return s_User.GetInstance();
            }
        }
        ''' <summary>
        ''' Returns Request object.
        ''' </summary>
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _;
        <Global.System.ComponentModel.Design.HelpKeyword("My.Request")> _;
        internal Global.System.Web.HttpRequest Request { get; }
            <Global.System.Diagnostics.DebuggerHidden()> _;
            {
                get
                {
                private Global.System.Web.HttpContext CurrentContext = Global.System.Web.HttpContext.Current;
                if (CurrentContext IsNot null)
                {
                    return CurrentContext.Request;
                }
                return null;
            }
        }
        ''' <summary>
        ''' Returns Response object.
        ''' </summary>
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _;
         <Global.System.ComponentModel.Design.HelpKeyword("My.Response")> _;
         internal Global.System.Web.HttpResponse Response { get; }
            <Global.System.Diagnostics.DebuggerHidden()> _;
            {
                get
                {
                private Global.System.Web.HttpContext CurrentContext = Global.System.Web.HttpContext.Current;
                if (CurrentContext IsNot null)
                {
                    return CurrentContext.Response;
                }
                return null;
            }
        }
        ''' <summary>
        ''' Returns the Asp log object.
        ''' </summary>
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _;
        internal Global.Microsoft.VisualBasic.Logging.AspLog Log { get; }
            <Global.System.Diagnostics.DebuggerHidden()> _;
            {
                get
                {
                return s_Log.GetInstance();
            }
        }

        ''' <summary>
        ''' Provides access to WebServices added to this project.
        ''' </summary>
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _;
        <Global.System.ComponentModel.Design.HelpKeyword("My.WebServices")> _;
        internal MyWebServices WebServices { get; }
            <Global.System.Diagnostics.DebuggerHidden()> _;
            {
                get
                {
                return m_MyWebServicesObjectProvider.GetInstance();
            }
        }

        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _;
        <Global.Microsoft.VisualBasic.MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")> _;
        <Global.System.Runtime.CompilerServices.CompilerGenerated()> _;
internal sealed class MyWebServices
        {

            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never), Global.System.Diagnostics.DebuggerHidden()> _;
            public override bool Equals(object o)
            {
                return MyBase.Equals(o);
            }
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never), Global.System.Diagnostics.DebuggerHidden()> _;
            public override int GetHashCode()
            {
                return MyBase.GetHashCode;
            }
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never), Global.System.Diagnostics.DebuggerHidden()> _;
            Friend Overloads Function [GetType]() As Global.System.Type;
                return GetType(MyWebServices);
            }
            <Global.System.ComponentModel.EditorBrowsable(Global.System.ComponentModel.EditorBrowsableState.Never), Global.System.Diagnostics.DebuggerHidden()> _;
            public override string ToString()
            {
                return MyBase.ToString;
            }

            <Global.System.Diagnostics.DebuggerHidden()> _;
            private static void Create__Instance__(Of T As {New})
            {
                if (instance Is null)
                {
                    return new T();
                Else
                    return instance;
                }
            }

            <Global.System.Diagnostics.DebuggerHidden()> _;
            private void Dispose__Instance__(Of T)
            {
                instance = Nothing
            }

            <Global.System.Diagnostics.DebuggerHidden()> _;
            <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> _;
            public void New()
            {
                MyBase.New();
            }
        }

        <Global.System.Runtime.CompilerServices.CompilerGenerated()> Private ReadOnly m_MyWebServicesObjectProvider As New ThreadSafeObjectProvider(Of MyWebServices);
    }

    <Global.System.Runtime.CompilerServices.CompilerGenerated(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Never)> Partial Friend Class MyApplication;
        Inherits Global.Microsoft.VisualBasic.ApplicationServices.ApplicationBase;
    }

}

#endif
}