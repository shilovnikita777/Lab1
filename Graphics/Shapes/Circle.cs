using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsTest.Shapes
{
    internal class Circle : Shape
    {
        private Point center;
        private int radius;

        public Point getCenter()
        {
            return center;
        }

        public void setCenter(Point center)
        {
            this.center = center;
        }
        public int getRadius()
        {
            return radius;
        }

        public void setRadius(int radius)
        {
            this.radius = radius;
        }

        public Circle(Point center, int radius, Color outlineColor)
        {
            this.center = center;
            this.radius = radius;
            this.outlineColor = outlineColor;
        }

        public Circle(Point center, int radius, Color outlineColor, Color fillColor)
        {
            this.center = center;
            this.radius = radius;
            this.outlineColor = outlineColor;
            this.fillColor = fillColor;
        }

        public Circle(Circle circle)
        {
            center = circle.getCenter();
            radius = circle.getRadius();
        }

        public void changeCenter(Point direction)
        {
            center.Offset(direction.X, direction.Y);
        }

    }
}
