using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsTest.Shapes
{
    internal class Rectangle : Shape
    {
        private Point leftTop;
        private Point rightBottom;

        public Rectangle(Point leftTop, Point rightBottom)
        {
            this.leftTop = leftTop;
            this.rightBottom = rightBottom;
        }

        public Rectangle(Point leftTop, Point rightBottom, Color outlineColor, Color fillColor)
        {
            this.leftTop = leftTop;
            this.rightBottom = rightBottom;
            this.outlineColor = outlineColor;
            this.fillColor = fillColor;
        }

        public Point getLeftTop()
        {
            return leftTop;
        }

        public void setLeftTop(Point leftTop)
        {
            this.leftTop = leftTop;
        }

        public Point getRightBottom()
        {
            return rightBottom;
        }

        public void setRightBottom(Point rightBottom)
        {
            this.rightBottom = rightBottom;
        }
    }
}
