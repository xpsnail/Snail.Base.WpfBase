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
using System.Windows.Shapes;

namespace UIDemo
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cardView.strCondition = ((Button)sender).Content.ToString();
        }

         

        private void cardView_DeleteInfo(object sender, RoutedEventArgs e)
        {
            btn2.Width = btn2.Width + 20;
            btn2.Content = "click btn2: " + btn2.Width;
            //MessageBox.Show("click");
        }
    }
}
