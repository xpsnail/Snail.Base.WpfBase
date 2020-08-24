using System;
using System.Collections.Generic;
using System.IO;
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

namespace Snail.Base.WpfBase
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             

        }

        private bool DrivesExit()
        {
            string[] drives = Directory.GetLogicalDrives();
            foreach (var item in drives)
            {
                if (item.ToUpper() == @"D:\")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
