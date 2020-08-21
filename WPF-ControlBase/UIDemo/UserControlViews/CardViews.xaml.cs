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


        #region 依赖项属性

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
        #endregion


        #region 自定义路由事件
        /// <summary>
        /// The delete event
        /// </summary>
        public static readonly RoutedEvent deleteEvent =
             EventManager.RegisterRoutedEvent("DeleteInfo", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CardViews));

        /// <summary>
        /// 删除该控件的操作.
        /// </summary>
        public event RoutedEventHandler DeleteInfo
        {
            add
            {
                AddHandler(deleteEvent, value);
            }

            remove
            {
                RemoveHandler(deleteEvent, value);
            }
        }
        #endregion

        private void btnDelete(object sender, MouseButtonEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(deleteEvent, this);
            RaiseEvent(args);
        }
    }
}
