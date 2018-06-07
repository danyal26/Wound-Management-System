using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WoundManagementSystem
{
    public static class CustomMessageDialog
    {

        public static async void createDialog(string title, string buttonText, string text)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = title,
                CloseButtonText = buttonText,
                Content = new TextBlock
                {
                    Text = text,
                    FontSize = 24,
                },
            };

            await dialog.ShowAsync();
        }
        
    }
}
