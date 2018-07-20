using SimplifySettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimplifySettingsSample.Settings
{
    public static class SubWindowSettingsHelper
    {
        public static SettingsHelper<Properties.SubWindowSettings> Helper { get; } = new SettingsHelper<Properties.SubWindowSettings>(Properties.SubWindowSettings.Default);
        /// <summary>
        /// 指定Windowの状態を保存する
        /// </summary>
        /// <param name="window"></param>
        public static void SaveSettings(Window window)
        {
            Helper.SaveSettings(settings =>
            {
                settings.WindowWidth = window.ActualWidth;
                settings.WindowHeight = window.ActualHeight;
                settings.WindowLeft = window.Left;
                settings.WindowTop = window.Top;
                settings.IsMaximized = window.WindowState == WindowState.Maximized;
            });
        }
        /// <summary>
        /// 保存した状態を指定Windowに復元する
        /// </summary>
        /// <param name="window"></param>
        public static void LoadSettings(Window window)
        {
            window.Width = Helper.Settings.WindowWidth;
            window.Height = Helper.Settings.WindowHeight;
            window.Left = Helper.Settings.WindowLeft;
            window.Top = Helper.Settings.WindowTop;
            window.WindowState = Helper.Settings.IsMaximized ? WindowState.Maximized : WindowState.Normal;
        }
    }
}
