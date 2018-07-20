using Prism.Interactivity.InteractionRequest;
using Prism.InteractivityExtension;
using SimplifySettingsSample.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimplifySettingsSample.Actions
{
    public class PopupSubWindowActoin : PopupWindowActionBase
    {
        protected override Window CreateWindow(INotification notification)
        {
            return new SubWindow();
        }
        protected override void ApplyNotificationToWindow(Window window, INotification notification)
        {
            window.Title = notification.Title;
        }
    }
}
