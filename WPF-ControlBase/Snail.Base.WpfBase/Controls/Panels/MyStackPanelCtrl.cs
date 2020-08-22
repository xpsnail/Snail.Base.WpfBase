using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Snail.Base.WpfBase.Controls.Panels
{
    /// <summary>
    /// 增加两个布局方向的StackPanel，"从右到左"与"从下到上"
    /// </summary>
    public class MyStackPanelCtrl : Panel
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(MyStackPanelCtrl), new FrameworkPropertyMetadata(Orientation.LeftToRight, FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// 获取或设置控件中子控件的布局方式
        /// </summary>
        [System.ComponentModel.Category("Layout")]
        public Orientation Orientation
        {
            get
            {
                return (Orientation)this.GetValue(OrientationProperty);
            }
            set
            {
                this.SetValue(OrientationProperty, value);
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size childrenSize = new Size(0, 0);

            foreach (UIElement child in this.Children)
            {
                child.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                childrenSize.Width += child.DesiredSize.Width;
                childrenSize.Height += child.DesiredSize.Height;
            }

            return childrenSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            switch (this.Orientation)
            {
                case Orientation.LeftToRight:
                    {
                        Point childPos = new Point(0, 0);
                        foreach (UIElement child in this.Children)
                        {
                            child.Arrange(new Rect(childPos, new Size(child.DesiredSize.Width, finalSize.Height)));
                            childPos.X += child.RenderSize.Width;
                        }
                    }
                    break;
                case Orientation.RightToLeft:
                    {

                        Point childPos = new Point(0, 0);

                        //此IF语句块的目的在于,对第一个被摆放的孩子调用Arrange方法,以便得到其RenderSize,
                        //这样就可以用控件的finalSize.Width - firstChild.RenderSize.Width得到第一个被摆放的
                        //孩子应该被放置的位置的X坐标
                        if (this.Children.Count > 0)
                        {
                            UIElement firstChild = this.Children[this.Children.Count - 1];
                            firstChild.Arrange(new Rect(childPos, new Size(firstChild.DesiredSize.Width, finalSize.Height)));
                            childPos.X = finalSize.Width - firstChild.RenderSize.Width;
                        }

                        foreach (UIElement child in this.Children)
                        {
                            child.Arrange(new Rect(childPos, new Size(child.DesiredSize.Width, finalSize.Height)));
                            childPos.X -= child.RenderSize.Width;
                        }
                    }
                    break;
                case Orientation.TopToBottom:
                    {
                        Point childPos = new Point(0, 0);
                        foreach (UIElement child in this.Children)
                        {
                            child.Arrange(new Rect(childPos, new Size(finalSize.Width, child.DesiredSize.Height)));
                            childPos.Y += child.RenderSize.Height;
                        }
                    }
                    break;
                case Orientation.BottomToTop:
                    {
                        Point childPos = new Point(0, 0);

                        //此IF语句块的目的在于,对第一个被摆放的孩子调用Arrange方法,以便得到其RenderSize,
                        //这样就可以用控件的finalSize.Height - firstChild.RenderSize.Height得到第一个被摆放的
                        //孩子应该被放置的位置的Y坐标
                        if (this.Children.Count > 0)
                        {
                            UIElement firstChild = this.Children[this.Children.Count - 1];
                            firstChild.Arrange(new Rect(childPos, new Size(finalSize.Width, firstChild.DesiredSize.Height)));
                            childPos.Y = finalSize.Height - firstChild.RenderSize.Height;
                        }

                        foreach (UIElement child in this.Children)
                        {
                            child.Arrange(new Rect(childPos, new Size(finalSize.Width, child.DesiredSize.Height)));
                            childPos.Y -= child.RenderSize.Height;
                        }

                    }
                    break;
                default:
                    break;
            }


            return finalSize;
        }

    }

    public enum Orientation
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }

}
