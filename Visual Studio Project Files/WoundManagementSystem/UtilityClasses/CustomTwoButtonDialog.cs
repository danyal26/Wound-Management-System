using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WoundManagementSystem
{
    public class CustomTwoButtonDialog : ContentDialog
    {
        public CustomTwoButtonDialog(string title, string closeText, string primaryText, string text)
        {
            this.Title = title;
            this.CloseButtonText = closeText;
            this.PrimaryButtonText = primaryText;
            this.Content = new TextBlock
            {
                Text = text,
                FontSize = 18
            };
        }
    }
}
