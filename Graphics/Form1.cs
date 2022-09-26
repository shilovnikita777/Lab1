using GraphicsTest.Shapes;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Rectangle = GraphicsTest.Shapes.Rectangle;

namespace GraphicsTest
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Bitmap bitmap;

        Circle circle;
        Rectangle rectangle;
        Triangle triangle;

        Color backColor;//���� ����

        Point direction;//����������� ���� �����


        public Form1()
        {
            InitializeComponent();
        }
        
        public void DrawLine(Point start, Point end, Color color)//�������� ���������� ��� ��������� ������
        {
            //�������� ������ � ������ ����� (������ �� �������, ����� ������������ ����� ������ Graphics ������ ����)
            Brush brush = new SolidBrush(color);
            Boolean swap;

            int x = start.X;
            int y = start.Y;
            int deltaX = Math.Abs(end.X - x);
            int deltaY = Math.Abs(end.Y - y);
            int sign1 = Math.Sign(end.X - start.X);
            int sign2 = Math.Sign(end.Y - start.Y);

            if (deltaY > deltaX)
            {
                (deltaX, deltaY) = (deltaY, deltaX);
                swap = true;
            } else
            {
                swap = false;
            }

            int e = 2 * deltaY - deltaX;

            for (int i = 0; i <= deltaX; ++i)
            {
                //bitmap.SetPixel(x, y, color);
                graphics.FillRectangle(brush, x, y, 1, 1);
                //Thread.Sleep(100);
                while(e >= 0)
                {
                    if (swap)
                    {
                        x = x + sign1;
                    } else
                    {
                        y = y + sign2;
                    }
                    e = e - 2 * deltaX;
                }
                if (swap)
                {
                    y = y + sign2;
                } else
                {
                    x = x + sign1;
                }
                e = e + 2 * deltaY;
            }
        }

        private void DrawCircle(Circle circle, Color color)//�������� ���������� ��� ��������� �����
        {
            //�������� ������ � ������ ����� (������ �� �������, ����� ������������ ����� ������ Graphics ������ ����)
            Brush brush = new SolidBrush(color);
            int y = 0;
            int x = circle.getRadius();

            int d = 3 - 2 * circle.getRadius();

            while (x > y)
            {
                graphics.FillRectangle(brush, circle.getCenter().X + x, circle.getCenter().Y + y, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X - x, circle.getCenter().Y + y, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X + x, circle.getCenter().Y - y, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X - x, circle.getCenter().Y - y, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X + y, circle.getCenter().Y + x, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X - y, circle.getCenter().Y + x, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X + y, circle.getCenter().Y - x, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X - y, circle.getCenter().Y - x, 1, 1);

                //Thread.Sleep(1);

                if (d < 0)
                {
                    d = d + 4 * y + 6;
                }
                else
                {
                    d = d + 4 * (y - x) + 10;
                    x--;
                }
                y++;
            }
            if (x == y)
            {
                graphics.FillRectangle(brush, circle.getCenter().X + x, circle.getCenter().Y + y, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X - x, circle.getCenter().Y + y, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X + x, circle.getCenter().Y - y, 1, 1);
                graphics.FillRectangle(brush, circle.getCenter().X - x, circle.getCenter().Y - y, 1, 1);
            }
        }

        private void FillCircle(Circle circle, Color color)//���������� ����� (������ ����� ������ ������ Graphics)
        {
            Brush brush = new SolidBrush(color);
            graphics.FillEllipse(brush,
                circle.getCenter().X - circle.getRadius(), circle.getCenter().Y - circle.getRadius(),
                circle.getRadius() * 2, circle.getRadius() * 2);
            DrawCircle(circle, circle.getOutlineColor());
        }

        private void EraseCircle(Circle circle)//�������� ����� (������� ���������� ����� � ������)
        {
            //�� ����� ������ ����� �� ���� ������ ����������, ������ ������ ����
            FillCircle(circle, backColor);
            DrawCircle(circle, backColor);
        }

        private Circle GenerateCircle()//������������� ��������� ���� (������� ���������� � ���������������� ����)
        {
            Random random = new Random();

            int radius = random.Next(10, pictureBox.Size.Height < pictureBox.Size.Width ? 
                (pictureBox.Size.Height/2 - 1) : (pictureBox.Size.Width/2 - 1));

            Point center = new Point(random.Next(pictureBox.Left + radius, pictureBox.Right - radius),
                                     random.Next(pictureBox.Top + radius, pictureBox.Bottom - radius));

            Color outlineColor = Color.FromArgb(random.Next(0,255), random.Next(0, 255), random.Next(0, 255));
            Color fillColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            return new Circle(center, radius, outlineColor, fillColor);
            
        }

        private void CircleMove(Circle circle/*, Point direction*/)//��� ����������� ����� (�������� �������, �������� ������, ��������� ������)
        {
            //�������� ������� �����
            EraseCircle(circle);

            //�������� �����
            circle.changeCenter(direction);

            //���������� ������ �����
            FillCircle(circle, circle.getFillColor());
            DrawCircle(circle, circle.getOutlineColor());

            //�������� �� ���� ����� � ������ ����������������� ����
            if ((circle.getCenter().X + circle.getRadius()) + Math.Abs(direction.X) >= pictureBox.Right && direction.X > 0 ||
                (circle.getCenter().X - circle.getRadius()) - Math.Abs(direction.X) <= pictureBox.Left && direction.X < 0)
            {
                direction.X = -direction.X;
            }

            if ((circle.getCenter().Y - circle.getRadius()) - Math.Abs(direction.Y) <= pictureBox.Top && direction.Y < 0 ||
                (circle.getCenter().Y + circle.getRadius()) + Math.Abs(direction.Y) >= pictureBox.Bottom && direction.Y > 0)
            {
                direction.Y = -direction.Y;
            }
        }

        private void DrawRectangle(Rectangle rectangle, Color color)//���������� ������������� (���������� 4 ����� ����� ������������� �������)
        {
            //������������� ������� 2 �������
            //����� ������� � ������ ������ ���������
            //� ����� ������ 4 ����� (� ����� ������� ������� � ������ �������)
            //(� ������ ������� � ������ ������)
            //(� ������ ������ � ����� ������)
            //(� ����� ������ � ����� �������)
            //������ �� �������, ����� ������������ ����� ������ Graphics
            Point temp = new Point(rectangle.getRightBottom().X, rectangle.getLeftTop().Y);

            DrawLine(rectangle.getLeftTop(), temp, color);
            DrawLine(temp, rectangle.getRightBottom(), color);

            temp.Offset(-(temp.X - rectangle.getLeftTop().X), -(temp.Y - rectangle.getRightBottom().Y));

            DrawLine(rectangle.getRightBottom(), temp, color);
            DrawLine(temp, rectangle.getLeftTop(), color);
        }

        private void FillRectangle(Rectangle rectangle, Color color)//���������� �������������� (������ ����� ������ ������ Graphics)
        {
            graphics.FillRectangle(new SolidBrush(color), 
                rectangle.getLeftTop().X + 1, rectangle.getLeftTop().Y + 1,
                rectangle.getRightBottom().X - rectangle.getLeftTop().X - 1,
                rectangle.getRightBottom().Y - rectangle.getLeftTop().Y - 1);
        }

        private void EraseRectangle(Rectangle rectangle)//������� ������������ (���������� ����� � ������)
        {
            //�� ����� ������ ����� �� ������������� ������ ����������, ������ ������ ����
            DrawRectangle(rectangle, backColor);
            FillRectangle(rectangle, backColor);
        }

        private Rectangle GenerateRectangle()//������������� ��������� ������������� (������� ���������� � ���������������� ����)
        {
            Random random = new Random();

            int leftTopX = random.Next(pictureBox.Left, pictureBox.Right - 10);
            int leftTopY = random.Next(pictureBox.Top, pictureBox.Bottom - 10);

            int rightBottomX = random.Next(leftTopX, pictureBox.Right);
            int rightBottomY = random.Next(leftTopY, pictureBox.Bottom);

            Point leftTop = new Point(leftTopX,leftTopY);
            Point rightBottom = new Point(rightBottomX,rightBottomY);

            Color outlineColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            Color fillColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            return new Rectangle(leftTop, rightBottom, outlineColor, fillColor);

        }

        private void DrawTriangle(Triangle triangle, Color color)//���������� ����������� (���������� 3 ����� ����� ������������� �������)
        {
            //�� ����� ������ ��� ����� (����������� ������� 3 �������)
            DrawLine(triangle.getFirstVertex(),triangle.getSecondVertex(), color);
            DrawLine(triangle.getSecondVertex(), triangle.getThirdVertex(), color);
            DrawLine(triangle.getThirdVertex(), triangle.getFirstVertex(),color);
        }
        
        private void FillTriangle(Triangle triangle, Color color)//���������� ��������������
        {
            //������ ����� ������ ������ Graphics
            Point[] points = new Point[] {triangle.getFirstVertex(), triangle.getSecondVertex(), triangle.getThirdVertex()};
            graphics.FillPolygon(new SolidBrush(color), points);
            DrawTriangle(triangle, triangle.getOutlineColor());
        }

        private void EraseTriangle(Triangle triangle)//������� ������������� (���������� ����� � ������)
        {
            //�� ����� ������ ����� �� ����������� ������ ����������, ������ ������ ����
            FillTriangle(triangle, backColor);
            DrawTriangle(triangle, backColor);
        }

        private Triangle GenerateTriangle()//������������� ��������� ������������� (������� ���������� � ����)
        {
            Random random = new Random();

            while (true)
            {
                int firstX = random.Next(pictureBox.Left, pictureBox.Right);
                int firstY = random.Next(pictureBox.Top, pictureBox.Bottom);

                int secondX = random.Next(pictureBox.Left, pictureBox.Right);
                int secondY = random.Next(pictureBox.Top, pictureBox.Bottom);

                int thirdX = random.Next(pictureBox.Left, pictureBox.Right);
                int thirdY = random.Next(pictureBox.Top, pictureBox.Bottom);

                Point first = new Point(firstX, firstY);
                Point second = new Point(secondX, secondY);
                Point third = new Point(thirdX, thirdY);

                Color outlineColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                Color fillColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

                if (first != second && second != third && third != first)
                {
                    return new Triangle(first, second, third, outlineColor, fillColor);
                }
            }

            return null;

        }

        private void DrawFrame()//���������� ����(������� ��������� ������, ����������� �����, ���������� �)
        {
            //��������� ������� ��������� ������, ���� ���� - �������
            if (circle != null)
            {
                EraseCircle(circle);
                circle = null;
            } else if (rectangle != null)
            {
                EraseRectangle(rectangle);
                rectangle = null;
            } else if (triangle != null)
            {
                EraseTriangle(triangle);
                triangle = null;
            }

            Random random = new Random();

            //��� ������
            int figureType = random.Next(0,3);

            //� ����������� �� ���� ���������� ������ � ������������
            switch (figureType)
            {
                case 0:
                    circle = GenerateCircle();
                    DrawCircle(circle, circle.getOutlineColor());
                    FillCircle(circle, circle.getFillColor());
                    break;
                case 1:
                    rectangle = GenerateRectangle();
                    DrawRectangle(rectangle, rectangle.getOutlineColor());
                    FillRectangle(rectangle, rectangle.getFillColor());
                    break;
                case 2:
                    triangle = GenerateTriangle();
                    DrawTriangle(triangle, triangle.getOutlineColor());
                    FillTriangle(triangle, triangle.getFillColor());
                    break;
            }
        }

        private Point GenerateDirection()//������������� ��������� ����������� ����������� �����
        {
            Random random = new Random();

            int x = random.Next(0, 10);
            int y = random.Next(0, 10);

            return new Point(x, y);
        }
        
        protected override void OnPaint(PaintEventArgs e)//����� ���������� ������ ��� ��� ��������� �����
        {
            //�������������� Graphics � Bitmap, ������ ������� ������ ����� ��� ������ ���� (����� ��� ��������� ������������� ������� ������)
            base.OnPaint(e);

            pictureBox.Width = this.ClientSize.Width;
            pictureBox.Height = this.Size.Height - 90;

            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);

            backColor = pictureBox.BackColor;
            //Draw();
        }

        private void buttonStart_Click(object sender, EventArgs e)//������� ������ �����
        {
            //���� ������� ������ ������ - ���������, � �������� ����
            if (timerStart.Enabled)
            {
                timerStart.Stop();
            } else
            {
                if (timerCircle.Enabled)
                {
                    timerCircle.Stop();
                }
                timerStart.Start();
            }
            //timerStart.Enabled = true;
        }

        private void timerStart_Tick(object sender, EventArgs e)//����� ������� (���������� ������ ��� �� ��������� �������)
        {
            //����������� ���� � ������� ������
            DrawFrame();
            pictureBox.Image = bitmap;
        }

        private void buttonCircleRun_Click(object sender, EventArgs e)//������� ������ ����������� �����
        {
            //���� ������� ������ ������ - ���������, � �������� ����
            if (timerCircle.Enabled)
            {
                timerCircle.Stop();
            }
            else
            {
                //���� ����� ���, ������ ���� � ���������� ����� ����������� ��� ������ ������� �������
                if (timerStart.Enabled)
                {
                    timerStart.Stop();
                }

                direction = GenerateDirection();

                if (circle == null)
                {
                    circle = GenerateCircle();
                }

                timerCircle.Start();
            }
        }

        private void timerCircle_Tick(object sender, EventArgs e)//����� ������� (���������� ������ ��� �� ��������� �������)
        {
            //������������ ���� � ������� ������
            CircleMove(circle);
            pictureBox.Image = bitmap;
        }
    }
}