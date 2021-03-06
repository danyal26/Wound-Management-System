﻿#pragma checksum "C:\Users\danny\Desktop\VS Projects\WoundManagementSystem\WoundManagementSystem\PriceLists.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "598A65792B9D6E1B5CFCD6DDDA205B26"
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
    partial class PriceLists : 
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

        private class PriceLists_obj59_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IPriceLists_Bindings
        {
            private global::WoundManagementSystem.Medicine dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj60;
            private global::Windows.UI.Xaml.Controls.TextBlock obj61;
            private global::Windows.UI.Xaml.Controls.TextBlock obj62;

            public PriceLists_obj59_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 60:
                        this.obj60 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 61:
                        this.obj61 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 62:
                        this.obj62 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
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

            // IPriceLists_Bindings

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

            // PriceLists_obj59_Bindings

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
                    }
                }
            }
            private void Update_id(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj60, obj, null);
                }
            }
            private void Update_name(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj61, obj, null);
                }
            }
            private void Update_price(global::System.Double obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj62, obj.ToString(), null);
                }
            }
        }

        private class PriceLists_obj73_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IPriceLists_Bindings
        {
            private global::WoundManagementSystem.Medicine dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj74;
            private global::Windows.UI.Xaml.Controls.TextBlock obj75;
            private global::Windows.UI.Xaml.Controls.TextBlock obj76;

            public PriceLists_obj73_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 74:
                        this.obj74 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 75:
                        this.obj75 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 76:
                        this.obj76 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
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

            // IPriceLists_Bindings

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

            // PriceLists_obj73_Bindings

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
                    }
                }
            }
            private void Update_id(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj74, obj, null);
                }
            }
            private void Update_name(global::System.String obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj75, obj, null);
                }
            }
            private void Update_price(global::System.Double obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj76, obj.ToString(), null);
                }
            }
        }

        private class PriceLists_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IPriceLists_Bindings
        {
            private global::WoundManagementSystem.PriceLists dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.ListView obj54;
            private global::Windows.UI.Xaml.Controls.ListView obj68;

            public PriceLists_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 54:
                        this.obj54 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    case 68:
                        this.obj68 = (global::Windows.UI.Xaml.Controls.ListView)target;
                        break;
                    default:
                        break;
                }
            }

            // IPriceLists_Bindings

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

            // PriceLists_obj1_Bindings

            public void SetDataRoot(global::WoundManagementSystem.PriceLists newDataRoot)
            {
                this.dataRoot = newDataRoot;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::WoundManagementSystem.PriceLists obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_sellingList(obj.sellingList, phase);
                        this.Update_buyingList(obj.buyingList, phase);
                    }
                }
            }
            private void Update_sellingList(global::System.Collections.ObjectModel.ObservableCollection<global::WoundManagementSystem.Medicine> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj54, obj, null);
                }
            }
            private void Update_buyingList(global::System.Collections.ObjectModel.ObservableCollection<global::WoundManagementSystem.Medicine> obj, int phase)
            {
                if((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_ItemsControl_ItemsSource(this.obj68, obj, null);
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
                    this.MainSplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 3:
                {
                    this.BtnCloseNav = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 15 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnCloseNav).Click += this.BtnToggleNav_Click;
                    #line default
                }
                break;
            case 4:
                {
                    this.GridLogo = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5:
                {
                    this.NavPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 6:
                {
                    this.NavLogout = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 118 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavLogout).Click += this.NavLogout_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.LogoutIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 8:
                {
                    this.NavPatients = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 39 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavPatients).Click += this.NavPatients_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.NavStaff = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 50 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavStaff).Click += this.NavStaff_Click;
                    #line default
                }
                break;
            case 10:
                {
                    this.NavStock = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 59 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavStock).Click += this.NavStock_Click;
                    #line default
                }
                break;
            case 11:
                {
                    this.NavPriceLists = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 68 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavPriceLists).Click += this.NavPriceLists_Click;
                    #line default
                }
                break;
            case 12:
                {
                    this.NavServices = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 77 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavServices).Click += this.NavServices_Click;
                    #line default
                }
                break;
            case 13:
                {
                    this.NavContacts = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 86 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavContacts).Click += this.NavContacts_Click;
                    #line default
                }
                break;
            case 14:
                {
                    this.NavFinances = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 97 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavFinances).Click += this.NavFinances_Click;
                    #line default
                }
                break;
            case 15:
                {
                    this.NavForms = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 106 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.NavForms).Click += this.NavForms_Click;
                    #line default
                }
                break;
            case 16:
                {
                    this.FormsIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 17:
                {
                    this.FinancesIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 18:
                {
                    this.ContactsIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 19:
                {
                    this.ServicesIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 20:
                {
                    this.PriceListsIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 21:
                {
                    this.StockIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 22:
                {
                    this.StaffIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 23:
                {
                    this.PatientIcon = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 24:
                {
                    this.logo = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 25:
                {
                    this.GridTitle = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 26:
                {
                    this.MainPanel = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 27:
                {
                    this.PanelError = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 28:
                {
                    this.PanelAddModify = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 29:
                {
                    this.BtnAddThisBuying = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 681 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnAddThisBuying).Click += this.BtnAddThisBuying_Click;
                    #line default
                }
                break;
            case 30:
                {
                    this.BtnModifyThisBuying = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 689 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnModifyThisBuying).Click += this.BtnModifyThisBuying_Click;
                    #line default
                }
                break;
            case 31:
                {
                    this.BtnAddBuyingCancel = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 697 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnAddBuyingCancel).Click += this.BtnAddBuyingCancel_Click;
                    #line default
                }
                break;
            case 32:
                {
                    this.BtnAddThisSelling = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 706 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnAddThisSelling).Click += this.BtnAddThisSelling_Click;
                    #line default
                }
                break;
            case 33:
                {
                    this.BtnModifyThisSelling = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 714 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnModifyThisSelling).Click += this.BtnModifyThisSelling_Click;
                    #line default
                }
                break;
            case 34:
                {
                    this.BtnAddSellingCancel = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 722 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnAddSellingCancel).Click += this.BtnAddSellingCancel_Click;
                    #line default
                }
                break;
            case 35:
                {
                    this.TxtName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 36:
                {
                    this.TxtPrice = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 37:
                {
                    this.TxtErrorMessage = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 38:
                {
                    this.BtnErrorClose = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 643 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnErrorClose).Click += this.BtnErrorClose_Click;
                    #line default
                }
                break;
            case 39:
                {
                    this.Overlay = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 40:
                {
                    this.PanelDialog = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 41:
                {
                    this.PanelDialog2 = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 42:
                {
                    this.TxtDialogMessage2 = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 43:
                {
                    this.BtnDialogOK2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 610 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnDialogOK2).Click += this.BtnDialogOK2_Click;
                    #line default
                }
                break;
            case 44:
                {
                    this.BtnDialogCancel2 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 617 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnDialogCancel2).Click += this.BtnDialogCancel2_Click;
                    #line default
                }
                break;
            case 45:
                {
                    this.TxtDialogMessage = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 46:
                {
                    this.BtnDialogOK = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 583 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnDialogOK).Click += this.BtnDialogOK_Click;
                    #line default
                }
                break;
            case 47:
                {
                    this.PanelBuyingList = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 48:
                {
                    this.Overlay1 = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 49:
                {
                    this.PanelSellingList = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 50:
                {
                    this.Overlay2 = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 51:
                {
                    this.TitleSellingList = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 52:
                {
                    this.Div2 = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 53:
                {
                    this.PanelSearchSelling = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 54:
                {
                    this.SellingListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 418 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.SellingListView).ItemClick += this.SellingListView_ItemClick;
                    #line default
                }
                break;
            case 55:
                {
                    this.PanelNoResult2 = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 56:
                {
                    this.PanelButtonsSelling = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 57:
                {
                    this.BtnModifySelling = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 522 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnModifySelling).Click += this.BtnModifySelling_Click;
                    #line default
                }
                break;
            case 58:
                {
                    this.BtnRemoveSelling = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 535 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnRemoveSelling).Click += this.BtnRemoveSelling_Click;
                    #line default
                }
                break;
            case 63:
                {
                    this.BtnAddSellingMedicine = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 396 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnAddSellingMedicine).Click += this.BtnAddSellingMedicine_Click;
                    #line default
                }
                break;
            case 64:
                {
                    this.TxtSearchSelling = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 411 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.TxtSearchSelling).TextChanged += this.TxtSearchSelling_TextChanged;
                    #line default
                }
                break;
            case 65:
                {
                    this.TitleBuyingList = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 66:
                {
                    this.Div1 = (global::Windows.UI.Xaml.Shapes.Rectangle)(target);
                }
                break;
            case 67:
                {
                    this.PanelSearch = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 68:
                {
                    this.BuyingListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 234 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.BuyingListView).ItemClick += this.BuyingListView_ItemClick;
                    #line default
                }
                break;
            case 69:
                {
                    this.PanelNoResult = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 70:
                {
                    this.PanelButtonsBuying = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 71:
                {
                    this.BtnModifyBuying = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 338 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnModifyBuying).Click += this.BtnModifyBuyingMedicine_Click;
                    #line default
                }
                break;
            case 72:
                {
                    this.BtnRemoveBuying = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 351 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnRemoveBuying).Click += this.BtnRemoveBuyingMedicine_Click;
                    #line default
                }
                break;
            case 77:
                {
                    this.BtnAddBuyingMedicine = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 212 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnAddBuyingMedicine).Click += this.BtnAddBuyingMedicine_Click;
                    #line default
                }
                break;
            case 78:
                {
                    this.TxtSearchBuying = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 227 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.TxtSearchBuying).TextChanged += this.TxtSearchBuying_TextChanged;
                    #line default
                }
                break;
            case 79:
                {
                    this.BtnCancel = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 148 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnCancel).Click += this.BtnCancel_Click;
                    #line default
                }
                break;
            case 80:
                {
                    this.BtnOpenNav = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 154 "..\..\..\PriceLists.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.BtnOpenNav).Click += this.BtnToggleNav_Click;
                    #line default
                }
                break;
            case 81:
                {
                    this.IconFinances = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 82:
                {
                    this.LblTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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
                    PriceLists_obj1_Bindings bindings = new PriceLists_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            case 59:
                {
                    global::Windows.UI.Xaml.Controls.RelativePanel element59 = (global::Windows.UI.Xaml.Controls.RelativePanel)target;
                    PriceLists_obj59_Bindings bindings = new PriceLists_obj59_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::WoundManagementSystem.Medicine) element59.DataContext);
                    element59.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element59, bindings);
                }
                break;
            case 73:
                {
                    global::Windows.UI.Xaml.Controls.RelativePanel element73 = (global::Windows.UI.Xaml.Controls.RelativePanel)target;
                    PriceLists_obj73_Bindings bindings = new PriceLists_obj73_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot((global::WoundManagementSystem.Medicine) element73.DataContext);
                    element73.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element73, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

