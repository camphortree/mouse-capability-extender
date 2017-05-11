using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

using WindowsInput;
using NHotkey;
using NHotkey.Wpf;


namespace TargetClicker
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<Target> list;

            list = new ObservableCollection<Target>();
            list.Add(new Target { StrName = "RightButtom", StrShortCutKeys = "CTRL+ALT+1", StrMouseEvent = "RIGHTCLICK", StrCordinate = "65535,65535" });
            list.Add(new Target { StrName = "LeftTop", StrShortCutKeys = "CTRL+ALT+2", StrMouseEvent = "RIGHTCLICK", StrCordinate = "0,0" });
            listView.DataContext = list;

            foreach (var t in list)
            {
                t.RegistHotKey();

            }
        }

        private void clickTarget(object sender, HotkeyEventArgs e)
        {
            var sim = new InputSimulator();
            sim.Mouse.MoveMouseTo(0, 0);
            sim.Mouse.RightButtonClick();
        }
    }
}
