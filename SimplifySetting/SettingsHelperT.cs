using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifySettings
{
    /// <summary>
    /// Settingsへの処理を補助するクラス
    /// </summary>
    /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
    public class SettingsHelper<T> where T : ApplicationSettingsBase
    {
        #region Property
        private static T settings;
        /// <summary>
        /// <see cref="T"/>のDefaultプロパティより取得したインスタンス
        /// </summary>
        public T Settings => settings;
        #endregion
        #region Constructor
        /// <summary>
        /// <see cref="SettingsHelper{T}"/>クラスをインスタンス化する
        /// </summary>
        public SettingsHelper()
        {
            //DefaultプロパティよりSettingsを取得してキャッシュする
            settings = settings ?? (T)typeof(T).GetProperty("Default").GetMethod.Invoke(null, null);
        }
        /// <summary>
        /// <see cref="SettingsHelper{T}"/>クラスをインスタンス化する
        /// こっちのが軽い
        /// </summary>
        public SettingsHelper(T defaultSettings)
        {
            settings = settings ?? defaultSettings;
        }
        #endregion
        #region Method
        /// <summary>
        /// 設定を再度読み込む
        /// </summary>
        public void ReloadSettings()
        {
            this.Settings.Reload();
        }
        /// <summary>
        /// 設定を規定値にリセットする
        /// </summary>
        public void ResetSettings()
        {
            this.Settings.Reset();
        }
        /// <summary>
        /// 現在の設定値を保存する
        /// </summary>
        public void SaveSettings()
        {
            this.Settings.Save();
        }
        /// <summary>
        /// 指定したプロパティ名に指定値をセットして保存する
        /// </summary>
        /// <param name="key">プロパティ名</param>
        /// <param name="value">設定値</param>
        public void SaveSettings(string key,object value)
        {
            this.Settings[key] = value;
            this.Settings.Save();
        }
        /// <summary>
        /// KeyValuePairで指定したプロパティ名へ値をセットして保存する
        /// </summary>
        /// <param name="keyValue">プロパティ名と値のペア</param>
        public void SaveSettings(KeyValuePair<string,object> keyValue)
        {
            this.SaveSettings(keyValue.Key, keyValue.Value);
        }
        /// <summary>
        /// IEnumerableなKeyValuePairでプロパティ名と値を指定し、全てSettingsへセットして保存する
        /// </summary>
        /// <param name="keyValues">IEnumerableなプロパティ名と値のペア</param>
        public void SaveSettings(IEnumerable<KeyValuePair<string, object>> keyValues)
        {
            foreach (var keyValue in keyValues)
            {
                this.Settings[keyValue.Key] = keyValue.Value;
            }
            this.Settings.Save();
        }
        /// <summary>
        /// 指定アクションを実行してからSettingsを保存する
        /// </summary>
        /// <param name="action"></param>
        public void SaveSettings(Action<T> action)
        {            
            action.Invoke(this.Settings);
            this.Settings.Save();
        }
        /// <summary>
        /// Settingsをアップグレードして保存する
        /// </summary>
        public void UpgradeSettings()
        {
            this.Settings.Upgrade();
            this.Settings.Save();
        }
        /// <summary>
        /// 指定したプロパティ名の値がfalseならSettingsをアップグレードして指定プロパティの値をtrueにして保存する
        /// </summary>
        /// <param name="isUpgratedPropertyName">アップグレードするかのフラグを持ったプロパティ名</param>
        public void UpgradeSettings(string isUpgratedPropertyName)
        {
            var isUpgrated = this.Settings[isUpgratedPropertyName];
            if (!(isUpgrated is bool)) return;
            if (!(bool)isUpgrated)
            {
                this.Settings.Upgrade();
                this.Settings[isUpgratedPropertyName] = true;
                this.Settings.Save();
            }
        }
        /// <summary>
        /// 現在のアセンブリバージョンが指定バージョンより高ければSettingsをアップグレードして保存する
        /// </summary>
        /// <param name="version"></param>
        public void UpgradeSettings(Version version)
        {   
            //リフレクションで現在のアセンブリバージョン取得
            var currentVersion = System.Reflection.Assembly.GetCallingAssembly().GetName().Version;
            if(version < currentVersion) this.UpgradeSettings();
        }
        #endregion
        #region Indexer
        public object this[string propertyName]
        {
            get => this.Settings[propertyName];
            set => this.Settings[propertyName] = value;
        }
        #endregion
    }
}
