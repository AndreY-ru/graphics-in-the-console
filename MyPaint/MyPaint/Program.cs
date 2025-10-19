using MyPaint;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using static MyPaint.Line;

class Program
{
    static void Main(string[] args)
    {
        float k = 1.5f;
        int width = (int)(126f * k);
        int height = (int)(30f * k);
        Console.SetWindowSize(width, height);

        Graphics graphics = new Graphics(width, height);
        graphics.Begin();

        List<Shape> shapes = new List<Shape>();

        Console.WriteLine("Введите ваше имя: ");
        string name = Console.ReadLine();

        while (true)
        {
            Console.Clear();
            Console.WriteLine(name + ", выберите действие:");
            Console.WriteLine("1. Добавить фигуру");
            Console.WriteLine("2. Вывести фигуры");
            Console.WriteLine("3. Очистить холст");
            Console.WriteLine("4. Сменить пользователя");
            Console.WriteLine("5. Выход");
            Console.Write("Ввод: ");
            
            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    AddShape(ref graphics, shapes, name);
                    break;
                case "2":
                    DisplayShapes(shapes, name);
                    graphics.End();
                    Console.ReadLine();
                    break;
                case "3":
                    graphics.Begin(); // Очистка холста
                    shapes.Clear();
                    graphics.End();
                    Console.WriteLine("Холст очищен.");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine("Введите имя нового пользователя: ");
                    name = Console.ReadLine();
                    Console.Clear();
                    break;
                case "5":
                    return; // Выход из программы
                default:
                    Console.WriteLine("Неверный ввод." + name + ", пожалуйста, попробуйте еще раз.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AddShape(ref Graphics graphics, List<Shape> shapes, string name)
    {
        Console.Clear();
        Console.WriteLine(name + ", выберите тип фигуры:");
        Console.WriteLine("1. Круг");
        Console.WriteLine("2. Окружность");
        Console.WriteLine("3. Прямоугольник");
        Console.WriteLine("4. Линия");
        Console.WriteLine("5. Треугольник");
        Console.Write("Ввод: ");
        string shapeType = Console.ReadLine();
        switch (shapeType)
        {
            case "1":
                // Создаем круг
                while (true)
                {
                    try
                    {
                        Console.WriteLine(name + ", введите координаты центра (x, y):");
                        string[] center = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (center.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения для координат.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(center[0], out float x) || !float.TryParse(center[1], out float y))
                        {
                            throw new FormatException("Координаты должны быть числовыми значениями.");
                        }

                        Vector2 circleCenter = new Vector2(x, y, graphics);

                        Console.WriteLine(name + ", введите радиус:");
                        if (!float.TryParse(Console.ReadLine(), out float R))
                        {
                            throw new FormatException("Радиус должен быть числовым значением.");
                        }

                        if (R <= 0)
                        {
                            throw new ArgumentOutOfRangeException("Радиус должен быть положительным значением.");
                        }

                        Circle circle = new Circle(circleCenter, new char[] { '@' }, R);
                        shapes.Add(circle);
                        graphics.Draw(circle);

                        Console.WriteLine("Фигура Круг создана!");
                        break; // Выход из цикла, если всё прошло успешно
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Ошибка ввода: {ex.Message}");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
                    }
                }
                break;

            case "2":

                // Создаем кольцо
                while (true)
                {
                    try
                    {
                        Console.WriteLine(name + ", введите координаты центра (x, y):");
                        string[] center = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (center.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения для координат.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(center[0], out float x) || !float.TryParse(center[1], out float y))
                        {
                            throw new FormatException("Координаты должны быть числовыми значениями.");
                        }

                        Vector2 circleCenter = new Vector2(x, y, graphics);

                        Console.WriteLine(name + ", введите внешний радиус:");
                        if (!float.TryParse(Console.ReadLine(), out float R))
                        {
                            throw new FormatException("Радиус должен быть числовым значением.");
                        }

                        if (R <= 0)
                        {
                            throw new ArgumentOutOfRangeException("Радиус должен быть положительным значением.");
                        }

                        Console.WriteLine(name + ", введите внутрений радиус:");
                        if (!float.TryParse(Console.ReadLine(), out float r))
                        {
                            throw new FormatException("Радиус должен быть числовым значением.");
                        }

                        if (r <= 0)
                        {
                            throw new ArgumentOutOfRangeException("Радиус должен быть положительным значением.");
                        }

                        Circle circle = new Circle(circleCenter, new char[] { '@' },R, r);
                        shapes.Add(circle);
                        graphics.Draw(circle);

                        Console.WriteLine("Фигура Кольцо создана!");
                        break; // Выход из цикла, если всё прошло успешно
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Ошибка ввода: {ex.Message}");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
                    }
                }
                break;

            case "3":
                // Создаем прямоугольник
                while (true)
                {
                    try
                    {
                        Console.WriteLine(name + ", введите координаты левого верхнего угла (x, y):");
                        string[] center = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (center.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения для координат.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(center[0], out float x) || !float.TryParse(center[1], out float y))
                        {
                            throw new FormatException("Координаты должны быть числовыми значениями.");
                        }

                        Vector2 circleCenter = new Vector2(x, y, graphics);

                        Console.WriteLine(name + ", введите длину и ширину прямоугольника (a, b): ");
                        
                        string[] ab = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (ab.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(ab[0], out float a) || !float.TryParse(ab[1], out float b))
                        {
                            throw new FormatException("Длина и ширина должны быть числовыми значениями.");
                        }

                        if ((a <= 0) || (b <= 0))
                        {
                            throw new ArgumentOutOfRangeException("Длина и ширина должен быть положительным значением.");
                        }

                        SolidRectangle solidRectangle = new SolidRectangle(circleCenter, new char[] { '#' }, a, b);
                        shapes.Add(solidRectangle);
                        graphics.Draw(solidRectangle);

                        Console.WriteLine("Фигура Прямоугольник создана!");
                        break; // Выход из цикла, если всё прошло успешно
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Ошибка ввода: {ex.Message}");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
                    }
                }
                break;

            case "4":
                // Создаем линию
                while (true)
                {
                    try
                    {
                        Console.WriteLine(name + ", введите координаты точки а (x, y):");
                        string[] A = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (A.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения для координат.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(A[0], out float xA) || !float.TryParse(A[1], out float yA))
                        {
                            throw new FormatException("Координаты должны быть числовыми значениями.");
                        }

                        Vector2 a = new Vector2(xA, yA, graphics);

                        Console.WriteLine(name + ", введите координаты точки b (x, y):");
                        string[] B = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (B.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения для координат.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(B[0], out float xB) || !float.TryParse(B[1], out float yB))
                        {
                            throw new FormatException("Координаты должны быть числовыми значениями.");
                        }

                        Vector2 b = new Vector2(xB, yB, graphics);

                        Line line = new Line(a, b, new char[] { '.' });
                        shapes.Add(line);
                        graphics.Draw(line);

                        Console.WriteLine("Фигура Линия создана!");
                        break; // Выход из цикла, если всё прошло успешно
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Ошибка ввода: {ex.Message}");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
                    }
                }
                break;

            case "5":
                // Создаем треугольник
                while (true)
                {
                    try
                    {
                        Console.WriteLine(name + ", введите координаты точки а (x, y):");
                        string[] A = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (A.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения для координат.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(A[0], out float xA) || !float.TryParse(A[1], out float yA))
                        {
                            throw new FormatException("Координаты должны быть числовыми значениями.");
                        }

                        Vector2 a = new Vector2(xA, yA, graphics);

                        Console.WriteLine(name + ", введите координаты точки b (x, y):");
                        string[] B = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (B.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения для координат.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(B[0], out float xB) || !float.TryParse(B[1], out float yB))
                        {
                            throw new FormatException("Координаты должны быть числовыми значениями.");
                        }

                        Vector2 b = new Vector2(xB, yB, graphics);

                        Console.WriteLine(name + ", введите координаты точки c (x, y):");
                        string[] C = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (C.Length != 2)
                        {
                            throw new FormatException("Вы должны ввести ровно 2 значения для координат.");
                        }

                        // Проверка на числа
                        if (!float.TryParse(C[0], out float xC) || !float.TryParse(A[1], out float yC))
                        {
                            throw new FormatException("Координаты должны быть числовыми значениями.");
                        }

                        Vector2 c = new Vector2(xC, yC, graphics);

                        Triangle triangle = new Triangle(a, b, c, new char[] { '.' });
                        shapes.Add(triangle);
                        graphics.Draw(triangle);

                        Console.WriteLine("Фигура Треугольник создана!");
                        break; // Выход из цикла, если всё прошло успешно
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Ошибка ввода: {ex.Message}");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
                    }
                }
                break;
            default:
                Console.WriteLine("Неверный ввод.");
                break;
        }
        Console.ReadKey();
    }

    static void DisplayShapes(List<Shape> shapes, string name)
    {
        Console.Clear();
        if (shapes.Count == 0)
        {
            Console.WriteLine(name + ", нет фигур для отображения.");
        }
        else
        {
            Console.WriteLine("Фигуры:");
            foreach (var shape in shapes)
            {
                Console.WriteLine(shape.GetType().Name); // Печатает название типа фигуры
            }
        }
    }
}
