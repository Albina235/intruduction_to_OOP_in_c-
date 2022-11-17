using System;

namespace H_w_P_C
{
	class Point3D
	{
		public int	x { get; set; }
		public int	y { get; set; }
		public int	z { get; set; }
		//конструкторы
		public Point3D(string str)
		{
			string[]	s = str.Split(new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);
			x = int.Parse(s[0]);
			y = int.Parse(s[1]);
			z = int.Parse(s[2]);
		}
		public Point3D(int x = 0, int y = 0, int z = 0)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		//создание копии
		public	Point3D(Point3D to_copy_dot)
		{
			x = to_copy_dot.x;
			y = to_copy_dot.y;
			z = to_copy_dot.z;
		}
		//обновить данные
		public void Update(int x = 0, int y = 0, int z = 0)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		//переопределенный вывод
		public override string ToString()
		{
			return ("(" + x + "," + y + "," + z + ")");
		}
		//сложение векторов
		public Point3D Plus(params Point3D []others)
		{
			Point3D current = new Point3D(this);
			foreach (var items in others)
			{
				current.x += items.x;
				current.y += items.y;
				current.z += items.z;
			}
			return current;
		}
		//норма
		public double Norm() => Math.Sqrt(x * x + y * y + z * z);
		//уранение плоскости по трем точкам
		public static int[] Plane_equation(Point3D d1, Point3D d2, Point3D d3)
		{
			int[] index = new int[4];
			index[0] = (d2.y - d1.y) * (d3.z - d1.z) - (d2.z - d1.z) * (d3.y - d1.y);
			index[1] = (d2.x - d1.x) * (d3.z - d1.z) - (d2.z - d1.z) * (d3.x - d1.x);
			index[2] = (d2.x - d1.x) * (d3.y - d1.y) - (d2.y - d1.y) * (d3.x - d1.x);
			index[3] = -d1.x * ((d2.y - d1.y) * (d3.z - d1.z) - (d2.z - d1.z) * (d3.y - d1.y)) -
				d1.y * ((d2.x - d1.x) * (d3.z - d1.z) - (d2.z - d1.z) * (d3.x - d1.x)) -
				d1.z * ((d2.x - d1.x) * (d3.y - d1.y) - (d2.y - d1.y) * (d3.x - d1.x));
			return index;
		}
		//Уравнение прямой по двум точкам
		/*public static string Straight_line_equation(Point3D d1, Point3D d2)
		{
		}*/
		//нормаль к плоскости
		public static Point3D Normal_plane(Point3D d1, Point3D d2, Point3D d3)
		{
			int[] index = Plane_equation(d1, d2, d3);
			return (new Point3D(index[0], index[1], index[2]));
		}
		//проверка: лежат ли три точки на одной прямой
		public bool On_one_line(Point3D d1, Point3D d2)
		{
			return ((this.x - d1.x) / (d2.x - d1.x) == (this.y - d1.y) / (d2.y - d1.y) &&  (this.x - d1.x) / (d2.x - d1.x) == (this.z - d1.z) / (d2.z - d1.z));
		}
		//проверка: лежит ли точка на заданной плоскоти + перегрузкой
		public bool On_one_plane(Point3D d1, Point3D d2, Point3D d3)
		{
			int[] index = Plane_equation(d1, d2, d3);
			return ((index[0] * this.x + index[1] * this.y + index[2] * this.z + index[3]) == 0);
		}
		public bool On_one_plane(int[] index) => ((index[0] * this.x + index[1] * this.y + index[2] * this.z + index[3]) == 0);

		//векторное произведение и векторное произведение
		public static Point3D	Vector_product(Point3D v1, Point3D v2) =>
			new Point3D(v1.y * v2.z - v1.z * v2.y, v1.z * v2.x - v1.x * v2.z, v1.x * v2.y - v1.y * v2.x);
		public static int		Scalar_product(Point3D v1, Point3D v2) => v1.x * v2.x + v1.y * v2.y + v1.z + v2.z;

		//перегрузка операторов
		public static Point3D operator+(Point3D d1, Point3D d2) =>
			new (d1.x + d2.x, d1.y + d2.y, d1.z + d2.z);
		public static Point3D operator-(Point3D d1, Point3D d2) =>
			new (d1.x - d2.x, d1.y - d2.y, d1.z - d2.z);

		public static Point3D operator++(Point3D obj)
		{
			obj.x +=1;
			obj.y +=1;
			obj.z +=1;
			return obj;
		}
		public static Point3D operator--(Point3D obj)
		{
			obj.x -=1;
			obj.y -=1;
			obj.z -=1;
			return obj;
		}

		//обращение к полям по индексу
		public int this[int i]
		{
			get{
				if (i == 0)
					return x;
				else if (i == 1)
					return y;
				else if (i == 2)
					return x;
				throw new Exception("Error, index out of range");
			}
		}
	}
	class Program
	{
		static void Main()
		{
			Point3D dot1 = new Point3D(1, 2, 3);
			dot1.z = 10;
			Console.WriteLine(dot1); //переопределение ToString
			Point3D dot2 = dot1; //ссылка
			dot2.x = 3;
			//Console.WriteLine(dot2.Plus(dot1, dot1));
			Console.WriteLine(dot1 + dot2); // перегрузка оператора
			Console.WriteLine(dot1);
			Point3D dot3 = new Point3D(4, 10, 3);
			Console.WriteLine(dot3);
			Console.WriteLine(Point3D.Vector_product(dot1, dot3));
			Console.WriteLine(dot1[1]);
			Point3D dot7 = new Point3D("1, 3, 5");
			Console.WriteLine(dot7);
		}
	}
}
