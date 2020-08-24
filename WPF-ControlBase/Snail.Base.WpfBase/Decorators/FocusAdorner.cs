using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Snail.Base.WpfBase.Decorators
{
    public class FocusAdorner : Adorner
    {

        public FocusAdorner(UIElement adornedElement) : base(adornedElement)
        {
            this.IsHitTestVisible = false;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Red), 1.0);

            drawingContext.DrawRectangle(new SolidColorBrush(Colors.Transparent), renderPen, adornedElementRect);
        }
    }
}
