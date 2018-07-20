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
    public class SettingsHelperTests
    {
        Test settings = Test.Default;

        [TestMethod()]
        public void ReloadSettingsTest()
        {
            //Settingsがリロードできているかどうか
            settings.Reset();
            settings.IsReloaded = false;
            SettingsHelper.ReloadSettings<Test>();
            Assert.IsTrue(settings.IsReloaded);
        }

        [TestMethod()]
        public void ResetSettingsTest()
        {
            //Settingsが規定値にリセットできているかどうか
            settings.Reset();
            settings.IsReset = true;
            settings.Save();
            SettingsHelper.ResetSettings<Test>();
            Assert.IsTrue(settings.IsReset);
        }

        [TestMethod()]
        public void SaveSettingsTest()
        {
            //Settingsへ保存できているかどうか
            settings.Reset();
            settings.HasSaved = true;
            SettingsHelper.SaveSettings<Test>();
            settings.Reload();
            Assert.IsTrue(settings.HasSaved);
        }

        [TestMethod()]
        public void SaveSettingsTest1()
        {
            //Settingsへ保存できているかどうか
            settings.Reset();
            SettingsHelper.SaveSettings<Test>("HasSaved", true);
            settings.Reload();
            Assert.IsTrue(settings.HasSaved);
        }

        [TestMethod()]
        public void SaveSettingsTest2()
        {
            //Settingsへ保存できているかどうか
            settings.Reset();
            SettingsHelper.SaveSettings<Test>(new KeyValuePair<string, object>("HasSaved", true));
            settings.Reload();
            Assert.IsTrue(settings.HasSaved);
        }

        [TestMethod()]
        public void SaveSettingsTest3()
        {
            //Settingsへ保存できているかどうか
            settings.Reset();
            var keyValues = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("HasSaved",true),
                new KeyValuePair<string, object>("HasSaved2",true),
                new KeyValuePair<string, object>("HasNotSaved",false),
            };
            SettingsHelper.SaveSettings<Test>(keyValues);
            settings.Reload();
            Assert.IsTrue(settings.HasSaved && settings.HasSaved2 && !settings.HasNotSaved);
        }

        [TestMethod()]
        public void SaveSettingsTest4()
        {
            //Settingsへ保存できているかどうか
            settings.Reset();
            SettingsHelper.SaveSettings<Test>(settings =>
            {
                settings.HasSaved = true;
                settings.HasSaved2 = true;
                settings.HasNotSaved = false;
            });
            settings.Reload();
            Assert.IsTrue(settings.HasSaved && settings.HasSaved2 && !settings.HasNotSaved);
        }
    }
}