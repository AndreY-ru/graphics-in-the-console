using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }
        
        public Vector2()
        {
            x = 0; y = 0;
        }
        public Vector2(float x, float y, Graphics graphics)
        {
            try {
                // центр координатной сетки
                float centerX = graphics.width / 2;
                float centerY = graphics.height / 2;
                // масштабируем координаты в промежуток [-1;1]
                this.x = ((2.0f * (x - centerX)) / graphics.width + 1.0f) * graphics.aspect * graphics.pixelAspect;
                this.y = (2.0f * (centerY - y)) / graphics.height - 1.0f; }
            catch
            {
                Console.WriteLine("Ошибка, не верно введины значени!");
            }

        }

        // запишем операции над векторами
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2 { x = a.x + b.x, y = a.y + b.y };
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2 { x = a.x - b.x, y = a.y - b.y };
        }
    }
}
