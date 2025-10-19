using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    // класс для создания фигур
    public class Shape
    {
        // protected открывает доступ родительским классам
        protected Vector2 position;
        protected char[] texture;
        public virtual float S { get; }
        public virtual float P { get; }
        public Shape(Vector2 position, char[] texture)
        {
            this.position = position;
            this.texture = texture;
        }

        // метод будет наследоваться и переобределятся в других кдассах, поэтому делаем его вирутальным
        public virtual char[] Draw(Graphics graphics)
        {
            return new char[graphics.width * graphics.height];
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public Vector2 GetPosition()
        {
            return position;
        }
    }

    // делаем круг - кольцо - окрудность
    public class Circle : Shape
    {
        protected float R { get; set; }
        protected float r { get; set; }
        

        public Circle(Vector2 position, char[] texture, float R, float r = -1) : base(position, texture)
        {
            this.R = R;
            this.r = r;
        }

        public override float S
        {
            get { 
                if (r <= 0) return R * R * 3.14f;
                else return R * R * 3.14f - r * r * 3.14f; }
        }

        public override float P {
                get {
                if (r  <= 0) return 3.14f * R * 2; 
                else return 3.14f * R * 2 + 3.14f * r * 2; ;
            }
        }

        // override позволяет переопределить виртуальный метод базового класса в производном классе.
        public override char[] Draw(Graphics graphics)
        {
            // выравниеваем R относительно x
            float aspectR = R / (graphics.width / 2);
            float aspect_r = r / (graphics.width / 2);
            char[] buffer = graphics.GetBuffer();
            for (int i = 0; i < graphics.width; i++)
                for (int j = 0; j < graphics.height; j++)
                {
                    // выравниваем соотношение коррдинат + делаем так, чтобы все было
                    // в промежутке [-1;1], именно поэтому мы выравнивали R у окружности в строчке 56
                    float x = (float)i / graphics.width * 2.0f - 1.0f;
                    float y = (float)j / graphics.height * 2.0f - 1.0f;
                    x *= graphics.aspect * graphics.pixelAspect;

                    // запише формулу круга
                    float f = (x - position.x) * (x - position.x) + (y - position.y) * (y - position.y);
                    // заполняем буффер "кругом"
                    if (f <= aspectR * aspectR)
                    {
                        buffer[i + j * graphics.width] = texture[0];
                    }
                    // если указать внутренний радиус, то пользоваель хочет построить кольцо - вырезаем дырку
                    if (r > 0 && f <= aspect_r * aspect_r)
                    {
                        buffer[i + j * graphics.width] = ' ';
                    }
                }
            return buffer;
        }
    }

    // класс прямоугольника = квадрат если одинакоавые дины указать
    public class SolidRectangle : Shape {
        // две переменные принимающая длинны сторон
        protected float x_Len { get; set; }
        protected float y_Len { get; set; }

        public SolidRectangle(Vector2 position, char[] texture, float x_Len, float y_Len) : base(position, texture)
        {
            
            this.x_Len = x_Len;
            this.y_Len = y_Len;
        }

        public override float S
        {
            get
            {
                return x_Len * y_Len;
            }
        }

        public override float P
        {
            get
            {
                return (x_Len + y_Len) * 2 ;
            }
        }
        public override char[] Draw(Graphics graphics)
        {
            // Нормализовать размеры прямоугольника к диапазону [-1, 1]
            float normalizedXLen = x_Len / graphics.width * graphics.aspect * graphics.pixelAspect; ;
            float normalizedYLen = y_Len / graphics.height;

            // Масштабируйте нормализованные размеры в зависимости от соотношения сторон и пикселей
            float scaledXLen = normalizedXLen * 2.0f * graphics.aspect * graphics.pixelAspect;
            float scaledYLen = normalizedYLen * 2.0f;

            char[] buffer = graphics.GetBuffer();
            for (int i = 0; i < graphics.width; i++)
            {
                for (int j = 0; j < graphics.height; j++)
                {
                    float normalizedX = (float)i / graphics.width * 2.0f - 1.0f;
                    float normalizedY = (float)j / graphics.height * 2.0f - 1.0f;

                    normalizedX *= graphics.aspect * graphics.pixelAspect;

                    // Проверьте, находится ли пиксель в пределах границ прямоугольника
                    if (normalizedX > position.x && normalizedX < position.x + scaledXLen &&
                        normalizedY >= position.y && normalizedY <= position.y + scaledYLen)
                    {
                        buffer[i + j * graphics.width] = texture[0];
                    } 
                }
            }
            return buffer;
        }
    }

    // класс линии
    public class Line : Shape
    {
        protected Vector2 endPosition;
        protected float k { get; set; }
        protected float a { get; set; }


        public Line(Vector2 position, Vector2 endPosition, char[] texture) : base(position, texture)
        {
            this.endPosition = endPosition;
            k = (endPosition.y - position.y) / (endPosition.x - position.x);
            a = position.y - position.x * k;
        }

        public override float S
        {
            get
            {
                return (float)Math.Sqrt((endPosition.x - position.x) * (endPosition.x - position.x) + (endPosition.y - position.y) * (endPosition.y - position.y));
            }
        }

        public override char[] Draw(Graphics graphics)
        {
            char[] buffer = graphics.GetBuffer();
            for(int i = 0; i < graphics.width; i++)
                for (int j = 0; j < graphics.height; j++)
                {
                    float x = (float)i / graphics.width * 2.0f - 1.0f;
                    float y = (float)j / graphics.height * 2.0f - 1.0f;
                    x *= graphics.aspect * graphics.pixelAspect;

                    float f = k * x + a;
                    if (y > f - 0.05f && y < f + 0.05f && x > position.x && x < endPosition.x)
                        buffer[i + j * graphics.width] = texture[0];
                }
            return buffer;
        }
        public class Triangle : Shape
        {
            // коорденаты вершин
            protected Vector2 vertex1;
            protected Vector2 vertex2;
            protected Vector2 vertex3;


            public Triangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, char[] texture) : base(vertex1, texture)
            {
                this.vertex1 = vertex1;
                this.vertex2 = vertex2;
                this.vertex3 = vertex3;
            }

            public override float S
            {
                get
                {
                    return (float)0.5 * (vertex1.x * (vertex2.y - vertex1.y) + vertex2.x * (vertex3.y - vertex1.y) + vertex3.x * (vertex1.y - vertex2.y));
                }
            }

            public override float P
            {
                get
                {
                    return (float)Math.Sqrt(
                        (vertex2.x - vertex1.x) * (vertex2.x - vertex1.x) + (vertex2.y - vertex1.y) * (vertex2.y - vertex1.y) +
                        (vertex3.x - vertex2.x) * (vertex3.x - vertex2.x) + (vertex3.y - vertex2.y) * (vertex3.y - vertex2.y) +
                        (vertex3.x - vertex1.x) * (vertex3.x - vertex1.x) + (vertex3.y - vertex1.y) * (vertex3.y - vertex1.y)
                        );
                }
            }

            public override char[] Draw(Graphics graphics)
            {
                char[] buffer = graphics.GetBuffer();

                // Рисуем линии между вершинами треугольника
                DrawLine(graphics, buffer, vertex1, vertex2);
                DrawLine(graphics, buffer, vertex2, vertex3);
                DrawLine(graphics, buffer, vertex1, vertex3);

                return buffer;
            }
            // функция рисования иний
            private void DrawLine(Graphics graphics, char[] buffer, Vector2 start, Vector2 end)
            {
                float k = (end.y - start.y) / (end.x - start.x);
                float a = start.y - start.x * k;

                for (int i = 0; i < graphics.width; i++)
                {
                    for (int j = 0; j < graphics.height; j++)
                    {
                        float x = (float)i / graphics.width * 2.0f - 1.0f;
                        float y = (float)j / graphics.height * 2.0f - 1.0f;
                        x *= graphics.aspect * graphics.pixelAspect;

                        float f = k * x + a;
                        if (y > f - 0.05f && y < f + 0.05f && x > start.x && x < end.x)
                        {
                            buffer[i + j * graphics.width] = texture[0];
                        }
                    }
                }
            }
        }

    }
}
