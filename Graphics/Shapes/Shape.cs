using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsTest.Shapes
{
    internal class Shape
    {
        protected Color outlineColor;
        protected Color fillColor;
        public Color getOutlineColor()
        {
            return outlineColor;
        }

        public void setOutlineColor(Color outlineColor)
        {
            this.outlineColor = outlineColor;
        }

        public Color getFillColor()
        {
            return fillColor;
        }

        public void setFillColor(Color fillColor)
        {
            this.fillColor = fillColor;
        }
    }
}
