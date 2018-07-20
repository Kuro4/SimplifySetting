using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplifySettings;
using SimplifySettingsTests.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifySettings.Tests
{
    [TestClass()]
    public class SettingsHelperTTests
    {
        private SettingsHelper<Test> helper = new SettingsHelper<Test>();

        [TestMethod()]
        public void SettingsHelperTest()
        {
            //TからSettingsが取れているかどうか
            Assert.AreEqual(Test.Default, helper.Settings);
        }

        [TestMethod()]
        public void SettingsHelperTest1()
        {
            //Settingsが取れているかどうか
            helper = null;
            helper = new SettingsHelper<Test>(Test.Default);
            Assert.AreEqual(Test.Default, helper.Settings);
        }

        [TestMethod()]
        public void ReloadSettingsTest()
        {
            //Settingsがリロードできているかどうか
            helper.Settings.Reset();
            helper.Settings.IsReloaded = false;
            helper.ReloadSettings();
            Assert.IsTrue(helper.Settings.IsReloaded);
        }

        [TestMethod()]
        public void ResetSettingsTest()
        {
            //Settingsが規定値にリセットできているかどうか
            helper.Settings.Reset();
            helper.Settings.IsReset = true;
            helper.Settings.Save();
            helper.ResetSettings();
            Assert.IsTrue(helper.Settings.IsReset);
        }

        [TestMethod()]
        public void SaveSettingsTest()
        {
            //Settingsへ保存できているかどうか
            helper.Settings.Reset();
            helper.Settings.HasSaved = true;
            helper.SaveSettings();
            helper.Settings.Reload();
            Assert.IsTrue(helper.Settings.HasSaved);
        }

        [TestMethod()]
        public void SaveSettingsTest1()
        {
            //Settingsへ保存できているかどうか
            helper.Settings.Reset();
            helper.SaveSettings("HasSaved", true);
            helper.Settings.Reload();
            Assert.IsTrue(helper.Settings.HasSaved);
        }

        [TestMethod()]
        public void SaveSettingsTest2()
        {
            //Settingsへ保存できているかどうか
            helper.Settings.Reset();
            helper.SaveSettings(new KeyValuePair<string, object>("HasSaved", true));
            helper.Settings.Reload();
            Assert.IsTrue(helper.Settings.HasSaved);
        }

        [TestMethod()]
        public void SaveSettingsTest3()
        {
            //Settingsへ保存できているかどうか
            helper.Settings.Reset();
            var keyValues = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("HasSaved",true),
                new KeyValuePair<string, object>("HasSaved2",true),
                new KeyValuePair<string, object>("HasNotSaved",false),
            };
            helper.SaveSettings(keyValues);
            helper.Settings.Reload();
            Assert.IsTrue(helper.Settings.HasSaved && helper.Settings.HasSaved2 && !helper.Settings.HasNotSaved);
        }

        [TestMethod()]
        public void SaveSettingsTest4()
        {
            //Settingsへ保存できているかどうか
            helper.Settings.Reset();
            helper.SaveSettings(settings =>
            {
                settings.HasSaved = true;
                settings.HasSaved2 = true;
                settings.HasNotSaved = false;
            });
            helper.Settings.Reload();
            Assert.IsTrue(helper.Settings.HasSaved && helper.Settings.HasSaved2 && !helper.Settings.HasNotSaved);
        }
    }
}