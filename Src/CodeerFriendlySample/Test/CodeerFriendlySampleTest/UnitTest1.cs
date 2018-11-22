using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.KeyMouse;
using RM.Friendly.WPFStandardControls;

namespace CodeerFriendlySampleTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// テストパラメータを使用するためのプロパティ宣言
        /// http://neue.cc/2011/02/23_304.html
        /// </summary>
        public TestContext TestContext { get; set; }

        private WindowsAppFriend _app;
        private TestDriver _drv;

        [TestInitialize]
        public void TestInitialize()
        {
            // Execute target process and attach
            var path = System.IO.Path.GetFullPath("CodeerFriendlySample.exe");
            _app = new WindowsAppFriend(Process.Start(path));
            var w = _app.IdentifyFromTypeFullName("CodeerFriendlySample.MainWindow");
            _drv = new TestDriver(w);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // SoftwareKeyboardTestAppの終了
            Process process = Process.GetProcessById(_app.ProcessId);
            _app.Dispose();
            process.CloseMainWindow();
        }

        [TestMethod]
        public void TestHiragana()
        {
            // txtInputTextをクリア
            _drv.txtInputText.EmulateChangeText("");

            // ひらがなを選択
            _drv.cmbMode.EmulateChangeSelectedIndex(0);

            // ひらがなが表示されるのを待つ
            while(_drv.grdHiragana.Visibility != System.Windows.Visibility.Visible)
            {
                System.Threading.Thread.Sleep(10);
            }

            // ひらがなが表示、カタカナが非表示
            Assert.AreEqual<System.Windows.Visibility>(_drv.grdHiragana.Visibility, System.Windows.Visibility.Visible);
            Assert.AreEqual<System.Windows.Visibility>(_drv.grdKatakana.Visibility, System.Windows.Visibility.Collapsed);

            // 「あ」入力
            var btnA = _drv.GetButton("あ");
            btnA.EmulateClick();
            Assert.AreEqual<string>(_drv.txtInputText.Text, "あ");

            // 「い」入力
            var btnI = _drv.GetButton("い");
            btnI.EmulateClick();
            Assert.AreEqual<string>(_drv.txtInputText.Text, "あい");
        }

        [TestMethod]
        public void TestKatakana()
        {
            // txtInputTextをクリア
            _drv.txtInputText.EmulateChangeText("");

            // カタカナを選択
            _drv.cmbMode.EmulateChangeSelectedIndex(1);

            // カタカナが表示されるのを待つ
            while (_drv.grdKatakana.Visibility != System.Windows.Visibility.Visible)
            {
                System.Threading.Thread.Sleep(10);
            }

            // ひらがなが表示、カタカナが非表示
            Assert.AreEqual<System.Windows.Visibility>(_drv.grdHiragana.Visibility, System.Windows.Visibility.Collapsed);
            Assert.AreEqual<System.Windows.Visibility>(_drv.grdKatakana.Visibility, System.Windows.Visibility.Visible);

            // 「あ」入力
            var btnA = _drv.GetButton("ア");
            btnA.EmulateClick();
            Assert.AreEqual<string>(_drv.txtInputText.Text, "ア");

            // 「い」入力
            var btnI = _drv.GetButton("イ");
            btnI.EmulateClick();
            Assert.AreEqual<string>(_drv.txtInputText.Text, "アイ");
        }
    }
}
