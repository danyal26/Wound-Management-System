﻿#pragma checksum "C:\Users\danny\Desktop\VS Projects\WoundManagementSystem\WoundManagementSystem\Receipt.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C14C4E854107EEE36EE789C80E83C43B"
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
    partial class Receipt : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        internal class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(global::Windows.UI.Xaml.Controls.ItemsControl obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.ItemsSource = value;
            }
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        private class Receipt_obj17_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IReceipt_Bindings
        {
            private global::WoundManagementSystem.Service dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj18;
            private global::Windows.UI.Xaml.Controls.TextBlock obj19;
            private global::Windows.UI.Xaml.Controls.TextBlock obj20;
            private global::Windows.UI.Xaml.Controls.TextBlock obj21;

            public Receipt_obj17_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 18:
                        this.obj18 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 19:
                        this.obj19 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 20:
                        this.obj20 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 21:
                        this.obj21 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 global::WoundManagementSystem.Service data = args.NewValue as global::WoundManagementSystem.Service;
                 if (args.NewValue != null && data == null)
                 {
                    throw new global::System.ArgumentException("Incorrect type passed into template. Based on the x:DataType global::WoundManagementSystem.Service was expected.");
                 }
                 this.SetDataRoot(data);
                 this.Update();
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                switch(args.Phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(args.Item as global::WoundManagementSystem.Service);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.RelativePanel)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::WoundManagementSystem.Service) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
            }

            // IReceipt_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // Receipt_obj17_Bindings

            public void SetDataRoot(global::WoundManagementSystem.Service newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::WoundManagementSystem.Service obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_id(obj.id, phase);
                        this.Update_name(obj.name, phase);
                        this.Update_price(obj.price, phase);
                        this.Update_type(obj.type, phase);
                    }
                }
            }
            private void Update_id(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj18, obj, null);
                }
            }
            private void Update_name(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj19, obj, null);
                }
            }
            private void Update_price(global::System.Double obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj20, obj.ToString(), null);
                }
            }
            private void Update_type(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj21, obj, null);
                }
            }
        }

        private class Receipt_obj22_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IReceipt_Bindings
        {
            private global::WoundManagementSystem.Medicine dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj23;
            private global::Windows.UI.Xaml.Controls.TextBlock obj24;
            private global::Windows.UI.Xaml.Controls.TextBlock obj25;
            private global::Windows.UI.Xaml.Controls.TextBlock obj26;

            public Receipt_obj22_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 23:
                        this.obj23 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 24:
                        this.obj24 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 25:
                        this.obj25 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 26:
                        this.obj26 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 global::WoundManagementSystem.Medicine data = args.NewValue as global::WoundManagementSystem.Medicine;
                 if (args.NewValue != null && data == null)
                 {
                    throw new global::System.ArgumentException("Incorrect type passed into template. Based on the x:DataType global::WoundManagementSystem.Medicine was expected.");
                 }
                 this.SetDataRoot(data);
                 this.Update();
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                switch(args.Phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(args.Item as global::WoundManagementSystem.Medicine);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            ((global::Windows.UI.Xaml.Controls.RelativePanel)args.ItemContainer.ContentTemplateRoot).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::WoundManagementSystem.Medicine) args.Item, 1 << (int)args.Phase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
            }

            // IReceipt_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // Receipt_obj22_Bindings

            public void SetDataRoot(global::WoundManagementSystem.Medicine newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::WoundManagementSystem.Medicine obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_id(obj.id, phase);
                        this.Update_name(obj.name, phase);
                        this.Update_price(obj.price, phase);
                        this.Update_quantity(obj.quantity, phase);
                    }
                }
            }
            private void Update_id(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj23, obj, null);
                }
            }
            private void Update_name(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj24, obj, null);
                }
            }
            private void Update_price(global::System.Double obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj25, obj.ToString(), null);
                }
            }
            private void Update_quantity(global::System.Int32 obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj26, obj.ToString(), null);
                }
            }
        }

        private class Receipt_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IReceipt_Bindings
        {
            private global::WoundManagementSystem.Receipt dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListView obj12;
            private global::Windows.UI.Xaml.Controls.ListView obj14;

            public Receipt_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 12:
                        this.obj12 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    case 14:
                        this.obj14 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    default:
                        break;
                }
            }

            // IReceipt_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            // Receipt_obj1_Bindings

            public void SetDataRoot(global::WoundManagementSystem.Receipt newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::WoundManagementSystem.Receipt obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_medList(obj.medList, phase);
                        this.Update_servicesList(obj.servicesList, phase);
                    }
                }
            }
            private void Update_medList(global::System.Collections.ObjectModel.ObservableCollection<global::WoundManagementSystem.Medicine> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj12, obj, null);
                }
            }
            private void Update_servicesList(global::System.Collections.ObjectModel.ObservableCollection<global::WoundManagementSystem.Service> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj14, obj, null);
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2:
                {
                    this.PanelMain = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 3:
                {
                    this.PanelTop = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 4:
                {
                    this.BtnPrint = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 265 "..\..\..\Receipt.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnPrint).Click += this.BtnPrint_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.PanelReceipt = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 6:
                {
                    this.GridLogo = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 7:
                {
                    this.LogoText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8:
                {
                    this.Div1 = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 9:
                {
                    this.PanelDetails = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 10:
                {
                    this.Div2 = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 11:
                {
                    this.TitleMedication = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12:
                {
                    this.ListViewMedication = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 13:
                {
                    this.TitleServices = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 14:
                {
                    this.ListViewServices = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 15:
                {
                    this.Div3 = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 16:
                {
                    this.LblTotalValue = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 27:
                {
                    this.LblInvoiceNo = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 28:
                {
                    this.LblDate = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 29:
                {
                    this.LblPatientID = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 30:
                {
                    this.LblName = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 31:
                {
                    this.LblType = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 32:
                {
                    this.logo = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 33:
                {
                    this.BtnCancelPrint = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 18 "..\..\..\Receipt.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnCancelPrint).Click += this.BtnCancelPrint_Click;
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
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    Receipt_obj1_Bindings bindings = new Receipt_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            case 17:
                {
                    global::Windows.UI.Xaml.Controls.RelativePanel element17 = (global::Windows.UI.Xaml.Controls.RelativePanel)target;
                    Receipt_obj17_Bindings bindings = new Receipt_obj17_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::WoundManagementSystem.Service) element17.DataContext);
                    element17.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element17, bindings);
                }
                break;
            case 22:
                {
                    global::Windows.UI.Xaml.Controls.RelativePanel element22 = (global::Windows.UI.Xaml.Controls.RelativePanel)target;
                    Receipt_obj22_Bindings bindings = new Receipt_obj22_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::WoundManagementSystem.Medicine) element22.DataContext);
                    element22.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element22, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

