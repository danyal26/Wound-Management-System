﻿#pragma checksum "C:\Users\danny\Desktop\VS Projects\WoundManagementSystem\WoundManagementSystem\FormViewer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D148E9A16023DD36CDA24CF4A0181271"
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
    partial class FormViewer : 
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
                    this.GridTitle = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 2:
                {
                    this.BtnPrint = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 3:
                {
                    this.PDFViewer = (global::Syncfusion.Windows.PdfViewer.SfPdfViewerControl)(target);
                }
                break;
            case 4:
                {
                    this.BtnCancel = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 24 "..\..\..\FormViewer.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnCancel).Click += this.BtnCancel_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.IconForms = (global::Windows.UI.Xaml.Controls.Image)(target);
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

