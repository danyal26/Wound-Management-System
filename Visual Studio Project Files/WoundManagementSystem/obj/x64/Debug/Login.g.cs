﻿#pragma checksum "C:\Users\danny\Desktop\VS Projects\WoundManagementSystem\WoundManagementSystem\Login.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1CD1A8C847EB02D93DF0D7CB17AAB19E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WoundManagementSystem
{
    partial class Login : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.PanelError = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 2:
                {
                    this.TxtErrorMessage = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    this.BtnErrorClose = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 66 "..\..\..\Login.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnErrorClose).Click += this.BtnErrorClose_Click;
                    #line default
                }
                break;
            case 4:
                {
                    this.TxtStaffID = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.TxtPassword = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 6:
                {
                    this.BtnLogin = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 44 "..\..\..\Login.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnLogin).Click += this.BtnLogin_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

