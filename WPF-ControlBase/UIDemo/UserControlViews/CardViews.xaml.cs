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

namespace UIDemo.UserControlViews
{
    /// <summary>
    /// CardViews.xaml 的交互逻辑
    /// </summary>
    public partial class CardViews : UserControl
    {
        public CardViews()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        //依赖项属性


        public string strCondition
        {
            get { return (string)GetValue(strConditionProperty); }
            set { SetValue(strConditionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for strCondition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty strConditionProperty =
            DependencyProperty.Register("strCondition", typeof(string), typeof(CardViews), new PropertyMetadata(datachange));

        private static void datachange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        //属性元数据 PropertyMetadata

        private static bool callback(object value)
        {
            return true;
        }
    }
}
