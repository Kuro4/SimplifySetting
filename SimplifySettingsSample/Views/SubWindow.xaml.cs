using System;
using System.Windows;

namespace SimplifySettingsSample.Views
{
    /// <summary>
    /// Interaction logic for SubWindow.xaml
    /// </summary>
    public partial class SubWindow : Window
    {
        public SubWindow()
        {
            InitializeComponent();
            //設定の読み込み
            Settings.SubWindowSettingsHelper.LoadSettings(this);
        }

        protected override void OnClosed(EventArgs e)
        {
            //設定の保存
            Settings.SubWindowSettingsHelper.SaveSettings(this);
            base.OnClosed(e);
        }
    }
}
