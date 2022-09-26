using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsTest.Shapes
{
    internal class Triangle : Shape
    {
        private Point firstVertex;
        private Point secondVertex;
        private Point thirdVertex;

        public Triangle(Point firstVertex, Point secondVertex, Point thirdVertex)
        {
            this.firstVertex = firstVertex;
            this.secondVertex = secondVertex;
            this.thirdVertex = thirdVertex;
        }

        public Triangle(Point firstVertex, Point secondVertex, Point thirdVertex, Color outlineColor, Color fillColor)
        {
            this.firstVertex = firstVertex;
            this.secondVertex = secondVertex;
            this.thirdVertex = thirdVertex;
            this.outlineColor = outlineColor;
            this.fillColor = fillColor;
        }

        public Point getFirstVertex()
        {
            return firstVertex;
        }

        public Point getSecondVertex()
        {
            return secondVertex;
        }

        public Point getThirdVertex()
        {
            return thirdVertex;
        }
    }
}
