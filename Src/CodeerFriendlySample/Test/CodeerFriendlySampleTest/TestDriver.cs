using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;

namespace CodeerFriendlySampleTest
{
    public class TestDriver
    {
        public WindowControl MainWindow { get; private set; }
        public IWPFDependencyObjectCollection<DependencyObject> LogicalTree { get; }
        public WPFTextBox txtInputText { get; private set; }

        public WPFComboBox cmbMode { get; private set; }
        public WPFGrid grdHiragana { get; private set; }
        public WPFGrid grdKatakana { get; private set; }

        public TestDriver(WindowControl w)
        {
            MainWindow = w;
            LogicalTree = w.LogicalTree();

            cmbMode = new WPFComboBox(LogicalTree.ByType<ComboBox>().Single());
            txtInputText = new WPFTextBox(LogicalTree.ByType<TextBox>().Single());
            grdHiragana = new WPFGrid(w.AppVar["FindName"].Invoke("grdHiragana"));
            grdKatakana = new WPFGrid(w.AppVar["FindName"].Invoke("grdKatakana"));
        }

        public WPFButtonBase GetButton(string buttonCaption)
        {
            var btn = LogicalTree.ByType<Button>().ByContentText<Button>(buttonCaption).Single();
            if (btn == null) return null;
            return new WPFButtonBase(btn);
        }

    }
}
