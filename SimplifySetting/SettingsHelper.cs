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
    /// <para>ジェネリックメソッドはリフレクションによりインスタンスを取得しているのでご利用は計画的に</para>
    /// </summary>
    public static class SettingsHelper
    {
        #region Method
        /// <summary>
        /// 設定を再度読み込む
        /// </summary>
        /// <param name="settings"></param>
        public static void ReloadSettings(ApplicationSettingsBase settings)
        {
            settings.Reload();
        }
        /// <summary>
        /// 設定を規定値にリセットする
        /// </summary>
        /// <param name="settings"></param>
        public static void ResetSettings(ApplicationSettingsBase settings)
        {
            settings.Reset();
        }
        /// <summary>
        /// 現在の設定値を保存する
        /// </summary>
        /// <param name="settings"></param>
        public static void SaveSettings(ApplicationSettingsBase settings)
        {
            settings.Save();
        }
        /// <summary>
        /// 指定したプロパティ名に指定値をセットして保存する
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="key">プロパティ名</param>
        /// <param name="value">設定値</param>
        public static void SaveSettings(ApplicationSettingsBase settings, string key, object value)
        {
            settings[key] = value;
            settings.Save();
        }
        /// <summary>
        /// KeyValuePairで指定したプロパティ名へ値をセットして保存する
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="keyValue">プロパティ名と値のペア</param>
        public static void SaveSettings(ApplicationSettingsBase settings, KeyValuePair<string, object> keyValue)
        {
            SaveSettings(settings, keyValue.Key, keyValue.Value);
        }
        /// <summary>
        /// IEnumerableなKeyValuePairでそれぞれ指定したプロパティ名へ値をセットして保存する
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="keyValues">IEnumerableなプロパティ名と値のペア</param>
        public static void SaveSettings(ApplicationSettingsBase settings, IEnumerable<KeyValuePair<string, object>> keyValues)
        {
            foreach (var keyValue in keyValues)
            {
                settings[keyValue.Key] = keyValue.Value;
            }
            settings.Save();
        }
        /// <summary>
        /// 指定アクションを実行してからSettingsを保存する
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="action"></param>
        public static void SaveSettings(ApplicationSettingsBase settings, Action<ApplicationSettingsBase> action)
        {
            action.Invoke(settings);
            settings.Save();
        }
        /// <summary>
        /// Settingsをアップグレードして保存する
        /// </summary>
        /// <param name="settings"></param>
        public static void UpgradeSettings(ApplicationSettingsBase settings)
        {
            settings.Upgrade();
            settings.Save();
        }
        /// <summary>
        /// 指定したプロパティ名の値がfalseならSettingsをアップグレードして指定プロパティの値をtrueにして保存する
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="isUpgratedPropertyName">アップグレードするかのフラグを持ったプロパティ名</param>
        public static void UpgradeSettings(ApplicationSettingsBase settings, string isUpgratedPropertyName)
        {
            var isUpgrated = settings[isUpgratedPropertyName];
            if (!(isUpgrated is bool)) return;
            if (!(bool)isUpgrated)
            {
                settings.Upgrade();
                settings[isUpgratedPropertyName] = true;
                settings.Save();
            }
        }
        /// <summary>
        /// 現在のアセンブリバージョンが指定バージョンより高ければSettingsをアップグレードして保存する
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="version">比較するアセンブリバージョン</param>
        public static void UpgradeSettings(ApplicationSettingsBase settings, Version version)
        {
            var currentVersion = System.Reflection.Assembly.GetCallingAssembly().GetName().Version;
            if (version < currentVersion)
            {
                settings.Upgrade();
                settings.Save();
            }
        }
        #endregion
        #region GenericMethod
        /// <summary>
        /// TのDefaultプロパティからSettingsのインスタンスを取得して返す
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        /// <returns>Settings</returns>
        private static T GetDefault<T>() where T : ApplicationSettingsBase
        {
            return (T)typeof(T).GetProperty("Default").GetMethod.Invoke(null, null);
        }
        /// <summary>
        /// 設定を再度読み込む
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        public static void ReloadSettings<T>() where T : ApplicationSettingsBase
        {
            ReloadSettings(GetDefault<T>());
        }
        /// <summary>
        /// 設定を規定値にリセットする
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        public static void ResetSettings<T>() where T : ApplicationSettingsBase
        {
            ResetSettings(GetDefault<T>());
        }
        /// <summary>
        /// 現在の設定値を保存する
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        public static void SaveSettings<T>() where T : ApplicationSettingsBase
        {
            SaveSettings(GetDefault<T>());
        }
        /// <summary>
        /// 指定したプロパティ名に指定値をセットして保存する
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        /// <param name="key">プロパティ名</param>
        /// <param name="value">設定値</param>
        public static void SaveSettings<T>(string key, object value) where T : ApplicationSettingsBase
        {
            SaveSettings(GetDefault<T>(), key, value);
        }
        /// <summary>
        /// KeyValuePairで指定したプロパティ名へ値をセットして保存する
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        /// <param name="keyValue">プロパティ名と値のペア</param>
        public static void SaveSettings<T>(KeyValuePair<string, object> keyValue) where T : ApplicationSettingsBase
        {
            SaveSettings<T>(keyValue.Key, keyValue.Value);
        }
        /// <summary>
        /// IEnumerableなKeyValuePairでそれぞれ指定したプロパティ名へ値をセットして保存する
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        /// <param name="keyValues">IEnumerableなプロパティ名と値のペア</param>
        public static void SaveSettings<T>(IEnumerable<KeyValuePair<string, object>> keyValues) where T : ApplicationSettingsBase
        {
            SaveSettings(GetDefault<T>(), keyValues);
        }
        /// <summary>
        /// 指定アクションを実行してからSettingsを保存する
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        /// <param name="action"></param>
        public static void SaveSettings<T>(Action<T> action) where T : ApplicationSettingsBase
        {
            var settings = GetDefault<T>();
            action.Invoke(settings);
            settings.Save();
        }
        /// <summary>
        /// Settingsをアップグレードして保存する
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        public static void UpgradeSettings<T>() where T : ApplicationSettingsBase
        {
            UpgradeSettings(GetDefault<T>());
        }
        /// <summary>
        /// 指定したプロパティ名の値がfalseならSettingsをアップグレードして指定プロパティの値をtrueにして保存する
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        /// <param name="isUpgratedPropertyName">アップグレードするかのフラグを持ったプロパティ名</param>
        public static void UpgradeSettings<T>(string isUpgratedPropertyName) where T : ApplicationSettingsBase
        {
            UpgradeSettings(GetDefault<T>(), isUpgratedPropertyName);
        }
        /// <summary>
        /// 現在のアセンブリバージョンが指定バージョンより高ければSettingsをアップグレードして保存する
        /// </summary>
        /// <typeparam name="T">ApplicationSettingsBaseを継承したSettingsクラス</typeparam>
        /// <param name="version">比較するアセンブリバージョン</param>
        public static void UpgradeSettings<T>(Version version) where T : ApplicationSettingsBase
        {
            UpgradeSettings(GetDefault<T>(), version);
        }
        #endregion
    }
}
