using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using System.Diagnostics;
using System.IO;

namespace TestProject1
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestMethod1()
        {
            ChangeWebConfig();
        }

        public void ChangeWebConfig()
        {
            var fakeExeName = "my.exe";
            InitializeConfigFile(fakeExeName, "IsReadOnly", "");

            Mock.Arrange(() => WebConfigurationManager.OpenWebConfiguration(Arg.IsAny<string>())).Returns(ConfigurationManager.OpenExeConfiguration(fakeExeName));

            var config = WebConfigurationManager.OpenWebConfiguration("~");

            Debug.Print(config.AppSettings.Settings["IsReadOnly"].Value);
            config.AppSettings.Settings["IsReadOnly"].Value = "12345";

            Debug.Print(config.AppSettings.Settings["IsReadOnly"].Value);

            config.Save();

            AssertSettingChanged(fakeExeName, "IsReadOnly", "12345");
        }

        public void InitializeConfigFile(string fakeExeName, string settingName, string settingValue)
        {
            if (!File.Exists(fakeExeName))
            {
                File.WriteAllText(fakeExeName, "Fake Exe created so we can use the config file");
            }

            var config = ConfigurationManager.OpenExeConfiguration(fakeExeName);
            config.AppSettings.Settings.Add(settingName, settingValue);
            config.Save();
        }

        public void AssertSettingChanged(string fakeExeName, string settingName, string newSettingValue)
        {
            Assert.AreEqual(newSettingValue, ConfigurationManager.OpenExeConfiguration(fakeExeName).AppSettings.Settings[settingName].Value);
        }
    }
}
