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
            HotkeyManager.Current.AddOrReplace("clickTarget", Key.D1 , ModifierKeys.Control | ModifierKeys.Alt, clickTarget);
        }

        private void clickButtonClick(object sender, RoutedEventArgs e)
        {
            var sim = new InputSimulator();
            sim.Mouse.MoveMouseTo(0, 0);
            sim.Mouse.RightButtonClick();
        }

        private void clickTarget(object sender, HotkeyEventArgs e)
        {
            var sim = new InputSimulator();
            sim.Mouse.MoveMouseTo(0, 0);
            sim.Mouse.RightButtonClick();
        }
    }
}
