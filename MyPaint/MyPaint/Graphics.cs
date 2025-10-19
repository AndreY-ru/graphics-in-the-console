using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaint
{
    public class Graphics
    {
        // ширина и высота консолм
        public int width;
        public int height;
        // соотношение ширины и высоты символов
        public float aspect;
        public float pixelAspect;
        // масштабирование
        public float multipliter;
        // "экран" вывода символов
        public char[] buffer;

        public Graphics(int width, int height)
        {
            this.width = width;
            this.height = height;

            aspect = (float)width / height;
            pixelAspect = 11.0f / 24.0f;

            buffer = new char[width * height + 1];
            buffer[width * height] = '\0';
        }

        // заполняем пробелами
        private void Reset()
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    buffer[i + j * width] = ' ';
        }
        // очищение экрана
        public void Begin()
        {
            Reset();
            Console.SetCursorPosition(0, 0);
        }

        // Будем передавать буфер
        public char[] GetBuffer() { return buffer; }

        // добавляем фигуру в буфер
        public void SetBuffer(char[] buffer)
        {
            this.buffer = buffer;
        }
        public void Draw(Shape shape) //Shape - какая нибудь фигура
        {
            char[] shapeBuffer = shape.Draw(this);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (shapeBuffer[i + j * height] != 0)
                        buffer[i + j * width] = shapeBuffer[i + j * width];
               }
        }

        // выводим фигуру
        public void End()
        {
            // добавим координатную сетку
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (i == width / 2)
                    {
                        buffer[i + j * width] = '|';
                    }
                    else if (j == height / 2)
                        buffer[i + j * width] = '-';
                }
            // выводим рисунок на экран
            Console.WriteLine(buffer);
        }
    }
}
