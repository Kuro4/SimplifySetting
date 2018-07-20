using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.InteractivityExtension;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SimplifySettingsSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private double windowWidth;
        public double WindowWidth
        {
            get { return windowWidth; }
            set { SetProperty(ref windowWidth, value); }
        }

        private double windowHeight;
        public double WindowHeight
        {
            get { return windowHeight; }
            set { SetProperty(ref windowHeight, value); }
        }

        private double windowLeft;
        public double WindowLeft
        {
            get { return windowLeft; }
            set { SetProperty(ref windowLeft, value); }
        }

        private double windowTop;
        public double WindowTop
        {
            get { return windowTop; }
            set { SetProperty(ref windowTop, value); }
        }

        public ReactiveCommand SaveSettingsCommand { get; } = new ReactiveCommand();
        public ReactiveCommand ReloadSettingsCommand { get; } = new ReactiveCommand();
        public ReactiveCommand ResetSetttingsCommand { get; } = new ReactiveCommand();
        public ReactiveCommand ShowSubWindowCommand { get; } = new ReactiveCommand();

        public InteractionRequest<Notification> ShowSubWindowRequest { get; } = new InteractionRequest<Notification>();

        private readonly SimplifySettings.SettingsHelper<Properties.Settings> settingsHelper = new SimplifySettings.SettingsHelper<Properties.Settings>();
        private readonly SimplifySettings.SettingsHelper<Properties.WindowSettings> windowSettingsHelper = new SimplifySettings.SettingsHelper<Properties.WindowSettings>();

        public MainWindowViewModel()
        {
            //設定値の読み込み処理
            this.LoadSettings();

            //保存処理の定義
            this.SaveSettingsCommand.Subscribe(() =>
            {
                this.settingsHelper.SaveSettings(settings =>
                {
                    settings.UserName = this.UserName;
                    settings.Password = this.Password;
                });
                this.windowSettingsHelper.SaveSettings(settings =>
                {
                    settings.WindowWidth = this.WindowWidth;
                    settings.WindowHeight = this.WindowHeight;
                    settings.WindowLeft = this.WindowLeft;
                    settings.WindowTop = this.WindowTop;
                });
            });
            //リロード処理の定義
            this.ReloadSettingsCommand.Subscribe(() =>
            {
                this.settingsHelper.ReloadSettings();
                this.windowSettingsHelper.ReloadSettings();
                this.LoadSettings();
            });
            //リセット処理の定義
            this.ResetSetttingsCommand.Subscribe(() =>
            {
                this.settingsHelper.ResetSettings();
                this.windowSettingsHelper.ResetSettings();
                this.LoadSettings();
            });
            //SubWindowの表示をリクエストする処理の定義
            this.ShowSubWindowCommand.Subscribe(() => this.ShowSubWindowRequest.RaiseEx("SimplifySettingsSample-UseInView", ""));
        }
        /// <summary>
        /// 設定を読み込む
        /// </summary>
        private void LoadSettings()
        {
            this.UserName = this.settingsHelper.Settings.UserName;
            this.Password = this.settingsHelper.Settings.Password;
            this.WindowWidth = this.windowSettingsHelper.Settings.WindowWidth;
            this.WindowHeight = this.windowSettingsHelper.Settings.WindowHeight;
            this.WindowLeft = this.windowSettingsHelper.Settings.WindowLeft;
            this.WindowTop = this.windowSettingsHelper.Settings.WindowTop;
        }
    }
}
