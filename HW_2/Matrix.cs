namespace HW_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* ----- ВАРИАНТ 2 ----- */
            char F = 'M', N = 'A';
            int variant = (F + N) % 7;

            Console.WriteLine($"* ----- Вариант {variant} ----- *\n" +
                $"double[]; IsSymmetric; m1 * m2; Trace, TryParse; MSUnit");
            /* ----- ВАРИАНТ 2 ----- */

        }
    }

    public class Matrix
    {
        /* ----- Данные: ----- */

        private int rows;
        private int cols;
        private double[]? data;

        /* ----- Свойства: ----- */
        public double[]? GetData
        {
            get { return data; }
        }

        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return cols; }
        }

        // Доступ к элементам матрицы через свойство-индексатор:
        public double this[int i, int j]
        {
            set { data[ i * cols + j] = value;}
            get
            {
                if (i < 0 || i > rows)
                    throw new ArgumentException("Неверный первый аргумент!");
                if (j < 0 || j > rows)
                    throw new ArgumentException("Неверный второй аргумент!");
                return data[i * cols + j];
            }
        }

        /// <summary>
        /// Получить размер квадратной матрицы / null (если матрица неквадратная).
        /// </summary>
        public int? Size
        {
            get
            {
                if (!IsSquared)
                    return null;
                return Rows * Columns;
            }
        }

        // Является ли матрица квадратной (количество строк == количству столбцов).
        public bool IsSquared
        {
            get
            {
                if (rows == cols) return true;
                else return false;
            }
        }

        // Является ли матрица нулевой (все значения матрицы == 0).
        public bool IsEmpty
        {
            get
            {
                foreach (var num in data)
                {
                    if (num != 0) return false;
                }

                return true;
            }
        }

        // Является ли матрица единичной (элементы главной диагонали == 1, все остальные == 0)
        public bool IsUnity
        {
            get
            {
                if (IsSquared) // Единичная матрица может быть только квадратной
                {
                    int diagonal = 0;

                    for (int i = 0; i < data.Length; i++)
                    {
                        if (i == diagonal)
                        {
                            if (data[i] != 1) return false; // На диагонали должны быть все единицы
                            diagonal += cols + 1;
                        }
                        else if(i != diagonal && data[i] != 0) // Вне диагонали должны быть нули
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else return false;
            }
        }

        // Является ли матрица симметричной (все значения матрицы a[i,j] == a[j,i])
        public bool IsSymmetric
        {
            get
            {
                if (IsSquared) // Симметричная матрица может быть только квадратной
                {
                    int currentRow = 0;
                    int currentCol = 1;

                    while (currentRow < rows &&
                        (currentRow * rows + currentCol != data.Length))
                    {
                        double num1 = data[currentRow * rows + currentCol];
                        double num2 = data[currentCol * rows + currentRow];

                        if (num1  != num2)
                            return false;
                        
                        currentCol++;

                        if (currentCol >= cols)
                            currentCol = ++currentRow + 1;
                    }
                    return true;
                }
                else return false;
            }
        }

        /* ----- Конструкторы: ----- */

        public Matrix(int nRows, int nCols)
        {
            if (nRows < 1)
            {
                throw new ArgumentException($"Невозможно создать матрицу с заданными параметрами!\n" +
                                    $"Количество строк матрицы должно быть > 0!");
            }

            if (nCols < 1)
            {
                throw new ArgumentException($"Невозможно создать матрицу с заданными параметрами!\n" +
                                    $"Количество столбцов матрицы должно быть > 0!");
            }

            rows = nRows;
            cols = nCols;
            data = new double[rows * cols];
        }

        public Matrix(double[,]? initData = null)
        {
            if (initData == null)
            {
                throw new ArgumentException($"Ошибка. Полученный массив пуст!");
            }
            if (initData.Length == 0)
            {
                throw new ArgumentException($"Невозможно создать матрицу с заданными параметрами!\n" +
                                    $"Количество столбцов и строк матрицы должны быть > 0!");
            }
            rows = initData.GetUpperBound(0) + 1;
            cols = initData.Length / rows;

            data = new double[rows * cols];

            int iterator = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[iterator++] = initData[i, j];
                }
            }
        }

        /* ----- Операторы: ----- */

        /// <summary>
        /// Умножение двух матриц.
        /// </summary>
        /// <param name="m1"> Первая матрица </param>
        /// <param name="m2"> Вторая матрица </param>
        /// <returns> Рузельтат умножения "m1" и "m2" </returns>
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            // Произведение двух матриц можно получить только в случае, если количество столбцов первой матрицы == количеству строк второй.
            if (m1.Columns != m2.Rows)
                throw new ArgumentException("Умножить матрицы нельзя! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");


            Matrix resMatrix = new(m1.Rows, m2.Columns);

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Columns; j++)
                {
                    resMatrix[i, j] = 0;

                    for (int p = 0; p < m2.Rows; p++)
                    {
                        resMatrix[i, j] += m1[i, p] * m2[p, j];
                    }
                }
            }

            return resMatrix;
        }

        /// <summary>
        /// Преобразование double[,] в объект класса Matrix
        /// </summary>
        /// <param name="arr"></param>
        public static explicit operator Matrix(double[,] arr)
        {
            if (arr == null)
                throw new ArgumentException("Массива не существует! Невозможно создать матрицу.");

            return new Matrix(arr);
        }

        /* ----- Методы: ----- */

        // Реализация экземплярного метода для вычисления следа матрицы
        public double Trace()
        {
            double result = 0;
            int diagonal = 0;
            do
            {
                result += data[diagonal];
                diagonal += cols + 1;
            }
            while (diagonal <= data.Length - 1);
            
            return result;
        }

        /// <summary>
        /// Реализация переопределение метода ToString для преобразования матрицы в строку
        /// </summary>
        /// <returns> Объект типа String вида "1 2 3 \n 4 5 6 \n 7 8 9 " для удобного вывода в консоль </returns>
        public override string ToString()
        {
            string strMatrix = "";

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    strMatrix += Convert.ToString(data[i * cols + j]) + " ";
                }
                strMatrix += "\n";
            }
            return strMatrix;
        }

        /* ----- Статические методы: ----- */

        /// <summary>
        /// Получить единичную матрицу.
        /// </summary>
        /// <param name="Size">Размер создаваемой матрицы</param>
        /// <returns>Объект класса Matrix</returns>
        public static Matrix GetUnity(int Size)
        {
            var matrix = new Matrix(Size, Size);

            int diagonal = 0;

            for (int i = 0; i < matrix.data.Length; i++)
            {
                if (i == diagonal)
                {
                    matrix.data[i] = 1; // На диагонали должны быть все единицы
                    diagonal += Size + 1;
                }
                else matrix.data[i] = 0; // Вне диагонали должны быть нули
            }

            return matrix;
        }

        /// <summary>
        /// Получить нулевую матрицу.
        /// </summary>
        /// <param name="Size">Размер создаваемой матрицы</param>
        /// <returns>Объект класса Matrix</returns>
        public static Matrix GetEmpty(int Size)
        {
            return new Matrix(Size, Size);
        }

        /// <summary>
        /// Преобразование строки в объект класса Matrix
        /// </summary>
        /// <param name="s"> Строка вида "1 2,8 3. 4,1 5,6 6. 7,8 8,345 9" </param>
        /// <param name="m"></param>
        /// <returns>объект класса Matrix</returns>
        public static bool TryParse(string s, out Matrix m)
        {
            m = null; // Изначально мы не знаем, будут ли данные корректны

            if (s == null || s.Length < 1)
                return false;

            string[] subRows = s.Split('.');
            int columnSize = subRows.GetLength(0);

            // Убираем все пробелы в начале и конце + разделяем Split()'ом строку на элементы 
            int rowSize = subRows[0].Trim().Split(" ").Length;

            m = new Matrix(columnSize, rowSize);


            /*
                В начале производим обход всех строк, чтобы убедиться в том,
                что все они одинаковы, иначе матрицу создать нельзя.
             
                После делаем ещё один обход для заполнения матрицы.
             */

            foreach (var subColumn in subRows)
            {
                string[] subColumnSplitted = subColumn.Trim().Split(" ");

                if (subColumnSplitted.Length != rowSize)
                    return false;
            }

            int iterator = 0;
            foreach (string subColumn in subRows)
            {
                string[] subColumnSplitted = subColumn.Trim().Split(" ");

                for (int i = 0; i < rowSize; i++)
                {
                    m.data[iterator++] = Convert.ToDouble(subColumnSplitted[i]);
                }
            }
            return true;
        }
    }
}