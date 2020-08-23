using Snail.Base.WpfBase.Decorators;
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
    /// VisualLayer.xaml 的交互逻辑
    /// </summary>
    public partial class VisualLayer : Window
    {
        public VisualLayer()
        {
            InitializeComponent();
        }

        #region 变量
        //拖动相关变量
        private bool isDragging = false;
        private Vector clickOffset;
        private DrawingVisual selectedVisual;

        //绘制选择区域矩形变量
        private bool isMultiSelecting = false;
        private Point selectionSquareTopLeft;

        //绘制常量
        private Brush drawingBrush = Brushes.AliceBlue;//绘制背景
        private Brush selectedDrawingBrush = Brushes.LightGoldenrodYellow;//选中背景
        private Pen drawingPen = new Pen(Brushes.SteelBlue, 3);//画笔
        private Pen selectedDrawingPen = new Pen(Brushes.Gray, 3);//画笔
        private Size squareSize = new Size(100, 100);//绘制矩形尺寸
        private DrawingVisual selectionSquare;//选中



        #endregion

        #region panel事件
        private void drawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointClicked = e.GetPosition(drawingSurface);//获取鼠标点击位置
            string strtype = ((ListBoxItem)drawType.SelectedItem).Content.ToString();

            if (strtype== "添加矩形")
            {
                DrawingVisual visual = new DrawingVisual();
                DrawSquare(visual, pointClicked, false);
                drawingSurface.AddVisual(visual);

                DrawingVisual visualImage = new DrawingVisual();
                DrawImage(visualImage, pointClicked, false);
                drawingSurface.AddVisual(visualImage);
            }
            else if (strtype == "添加文本")
            {
                DrawingVisual visual = new DrawingVisual();
                DrawText(visual, pointClicked, false, "添加一段文本");
                drawingSurface.AddVisual(visual);
            }
            else if (strtype == "选择")
            {
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if (visual != null)
                { 
                    Point topLeftCorner = new Point(
                        visual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        visual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
                    DrawSquare(visual, topLeftCorner, true);

                    clickOffset = topLeftCorner - pointClicked;
                    isDragging = true;

                    if (selectedVisual != null && selectedVisual != visual)
                    {
                        // The selection has changed. Clear the previous selection.
                        ClearSelection();
                    }
                    selectedVisual = visual;
                }
            }
            else if (strtype == "删除")
            {
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if (visual != null) drawingSurface.DeleteVisual(visual);
            }
            else if (strtype == "装饰")
            { 
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(txtBlock);
                adornerLayer.Add(new FocusAdorner(txtBlock));
            }



        }

        private void drawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point pointDragged = e.GetPosition(drawingSurface) + clickOffset;
                DrawSquare(selectedVisual, pointDragged, true);
            }
            else if (isMultiSelecting)
            {
                //Point pointDragged = e.GetPosition(drawingSurface);
                //DrawSelectionSquare(selectionSquareTopLeft, pointDragged);
            }
        }

        private void drawingSurface_MouseLeave(object sender, MouseEventArgs e)
        {

        }
        #endregion

        #region 绘制方法

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="topLeftCorner"></param>
        /// <param name="isSelected"></param>
        private void DrawSquare(DrawingVisual visual, Point topLeftCorner, bool isSelected)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                Brush brush = drawingBrush;
                if (isSelected)
                {
                    brush = selectedDrawingBrush;
                }
                dc.DrawRectangle(brush, drawingPen, new Rect(topLeftCorner, squareSize));
             }
        }

        private void DrawImage(DrawingVisual visual, Point topLeftCorner, bool isSelected)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                Brush brush = drawingBrush;
                if (isSelected)
                {
                    brush = selectedDrawingBrush;
                } 
                dc.DrawImage(new BitmapImage(new Uri("pack://application:,,,/ResourcesLib;component/Images/BtnIcons/雪山.png")), new Rect(topLeftCorner, new Size(30, 30)));
            }
        }

        /// <summary>
        /// 绘制文本
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="topLeftCorner"></param>
        /// <param name="isSelected"></param>
        /// <param name="text"></param>
        private void DrawText(DrawingVisual visual, Point topLeftCorner, bool isSelected,string text)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                Brush brush = drawingBrush;
                if (isSelected) brush = selectedDrawingBrush;

                FormattedText formattedText = new FormattedText(text, new System.Globalization.CultureInfo("en-US"), FlowDirection.LeftToRight,
                    new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal), 20, Brushes.Black)
                {

                };
                dc.DrawText(formattedText, topLeftCorner);
            }
        }

        /// <summary>
        /// 清除选中效果
        /// </summary>
        private void ClearSelection()
        {
            Point topLeftCorner = new Point(
                        selectedVisual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        selectedVisual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
            DrawSquare(selectedVisual, topLeftCorner, false);
            selectedVisual = null;
        }
        #endregion
    }
}
