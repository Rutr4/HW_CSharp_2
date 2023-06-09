using HW_2;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.InteropServices;

namespace HW_2_test
{
    [TestClass]
    public class UnitTest1
    {
        // #1 ������� ������� � �����������
        [DataTestMethod]
        [DataRow(4, 4)]
        [DataRow(30, 15)]
        public void CreateMatrixWithArguments(int expectedRows, int expectedCols)
        {
            // arrange
            // act
            Matrix m = new(expectedRows, expectedCols);

            // assert
            Assert.AreEqual(expectedRows, m.Rows, $"{expectedRows} ������ ���� ����� {m.Rows}");
            Assert.AreEqual(expectedCols, m.Columns, $"{expectedCols} ������ ���� ����� {m.Columns}");
        }
        
        // #2 ������� ������� � ������������� �����������
        [DataTestMethod]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(null, 1)]
        [DataRow(1, null)]
        [DataRow(null, null)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMatrixWithArguments_BadArguments_Exception(int rows, int cols)
        {
            // Arrange
            // Act
            var matrix = new Matrix(rows, cols);
            // Assert
        }
        
        // #3 ������� �������, ����������� ������� double[,]
        [TestMethod]
        public void CreateMatrixWithArray()
        {
            // arrange
            double[,] arrayTwoDim = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            double[] expectedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // act
            Matrix actualMatrix = new Matrix(arrayTwoDim);

            // assert
            CollectionAssert.AreEqual(expectedArray, actualMatrix.GetData, $"{expectedArray} ������ ���� ����� {actualMatrix.GetData}");
        }

        // #4 ������� �������, ����������� ������������ ������
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateMatrixWithArray_BadArguments_Exception()
        {
            // Arrange
            double[,]? array = null;
            // Act
            var matrix = new Matrix(array);
            // Assert
        }

        // #5 �������� �� ������� ����������
        [DataTestMethod]
        [DataRow(4, 4, true)]
        [DataRow(40, 40, true)]
        [DataRow(15, 3, false)]
        [DataRow(25, 50, false)]
        public void Size_SquareMatrix_bool(int row, int col, bool expected)
        {
            // arrange
            Matrix matrix = new(row, col);

            // act
            bool actual = matrix.IsSquared;

            //assert
            Assert.AreEqual(expected, actual);
        }

        // #6 �������� ������� �� �����������, ������ ��������
        [DataTestMethod]
        [DataRow(0, 0, 1)]
        [DataRow(2, 2, 9.7)]
        [DataRow(1, 2, 6.1)]
        [DataRow(2, 0, 7.05)]
        public void ElementAccess_Arguments_double(int i, int j, double expected)
        {
            // arrange
            double[,] data = {{1, 2.1, 3 },
                              {4.004, 5.3, 6.1 },
                              {7.05, 8, 9.7 }};
            Matrix matrix = new(data);

            // act
            double actual = matrix[i, j];
        
            //assert
            Assert.AreEqual(expected, actual);
        }

        // #7 �������� ������� �� �����������, ������������ ��������
        [DataTestMethod]
        [DataRow(-1, 0)]
        [DataRow(0, -1)]
        [DataRow(1, 200)]
        [DataRow(200, 0)]
        [ExpectedException(typeof(ArgumentException))]
        public void ElementAccess_BadArguments_Exception(int i, int j)
        {
            // arrange
            double[,] data = {{1, 2.1, 3 },
                              {4.004, 5.3, 6.1 },
                              {7.05, 8, 9.7 }};
            Matrix matrix = new(data);

            // act
            double actual = matrix[i, j];
        
            //assert
        }

        // #8 �������� ���� �������
        [TestMethod]
        public void GetMatrixTrace_Double()
        {
            // arrange
            double[,] data = {{4.3, 5.5, 5, 4.3 },
                              {5.7, 9.1, 8,3.3 },
                              {4, 3.1, 3.2 ,3.3 },
                              {1, 2, 3, 4 } };
            
            double expected = 20.6;
            Matrix matrix = new(data);

            // act
            double actual = matrix.Trace();

            //assert
            // 0.000000005 - delta (�����������)
            Assert.AreEqual(expected, actual, 0.000000005);
        }

        // #9 �������� ��������� ������������� �������
        [TestMethod]
        public void MatrixToString_String()
        {
            // arrange
            double[,] data = {{4.3, 5, 4.3 },
                              {5.7, 9.1, 8},
                              {1, 2, 3 } };

            string expected = "4,3 5 4,3 \n5,7 9,1 8 \n1 2 3 \n";
            Matrix matrix = new(data);

            // act
            string actual = matrix.ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }

        // #10 �������� ������ ���������� �������
        [DataTestMethod]
        [DataRow(5, 5, 5)]
        [DataRow(5, 10, null)]
        [DataRow(10, 10, 10)]
        [DataRow(1, 5, null)]
        public void MatrixToString_String(int i, int j, bool expected)
        {
            // arrange
            Matrix matrix = new(i, j);

            // act
            int? actual = matrix.Size;

            //assert
            Assert.AreEqual(expected, actual);
        }

        private double[,] empty = { { 0, 0, 0, 0},
                                    { 0, 0, 0, 0},
                                    { 0, 0, 0, 0},
                                    { 0, 0, 0, 0}};

        private double[,] unity = { { 1, 0, 0, 0},
                                    { 0, 1, 0, 0},
                                    { 0, 0, 1, 0},
                                    { 0, 0, 0, 1}};

        // #11.1 �������� �� ������� �������
        [TestMethod]
        public void MatrixIsEmpty_BoolTrue()
        {
            // arrange
            Matrix matrix = new(empty);
            bool expected = true;

            // act
            bool actual = matrix.IsEmpty;

            //assert
            Assert.AreEqual(expected, actual);
        }
        // #11.2 �������� �� ������� �������
        [TestMethod]
        public void MatrixIsEmpty_BoolFalse()
        {
            // arrange
            Matrix matrix = new(unity);
            bool expected = false;

            // act
            bool actual = matrix.IsEmpty;

            //assert
            Assert.AreEqual(expected, actual);
        }

        // #12.1 �������� �� ������� ���������
        [TestMethod]
        public void MatrixIsUnity_BoolTrue()
        {
            // arrange
            Matrix matrix = new(unity);
            bool expected = true;

            // act
            bool actual = matrix.IsUnity;

            //assert
            Assert.AreEqual(expected, actual);
        }
        // #12.2 �������� �� ������� ���������
        [TestMethod]
        public void MatrixIsUnity_BoolFalse()
        {
            // arrange
            Matrix matrix = new(empty);
            bool expected = false;

            // act
            bool actual = matrix.IsUnity;

            //assert
            Assert.AreEqual(expected, actual);
        }

        // #13.1 �������� �� ������� ������������
        [TestMethod]
        public void MatrixIsSymmetric_BoolTrue()
        {
            // arrange
            double[,] data = { {1,2,3 },
                               {2,5,4 },
                               {3,4,6 } };
            Matrix matrix = new(data);
            bool expected = true;

            // act
            bool actual = matrix.IsSymmetric;

            //assert
            Assert.AreEqual(expected, actual);
        }
        // #13.2 �������� �� ������� ������������
        [TestMethod]
        public void MatrixIsSymmetric_BoolFalse()
        {
            // arrange
            double[,] data = { {1,8,9 },
                               {2,5,6 },
                               {3,4,6 } };
            Matrix matrix = new(data);
            bool expected = false;

            // act
            bool actual = matrix.IsSymmetric;

            //assert
            Assert.AreEqual(expected, actual);
        }

        // #14 �������� ������ �������
        [TestMethod]
        public void GetEmptyMatrix_Matrix()
        {
            // arrange
            Matrix expectedEmptyM = new(empty);

            // act
            Matrix actualEmptyM = Matrix.GetEmpty(4);

            //assert
            CollectionAssert.AreEqual(expectedEmptyM.GetData, actualEmptyM.GetData);
        }

        // #15 �������� ��������� �������
        [TestMethod]
        public void GetEmptyUnity_Matrix()
        {
            // arrange
            Matrix expectedEmptyM = new(unity);

            // act
            Matrix actualEmptyM = Matrix.GetUnity(4);

            //assert
            CollectionAssert.AreEqual(expectedEmptyM.GetData, actualEmptyM.GetData);
        }

        // #16.1 ���������� ���������� ������ � ������� �� �� ������ ������ Matrix
        [TestMethod]
        public void MatrixTryParse_MatrixFromString()
        {
            // arrange
            string str = "2,1 3,4 5. 6,9 7 3";
            double[] expected = { 2.1, 3.4, 5, 6.9, 7, 3 };
            // act
            Matrix? actual = null;
            bool result= Matrix.TryParse(str, out actual);

            //assert
            CollectionAssert.AreEqual(expected, actual.GetData);
        }
        // #16.2 ���������� ���������� ������ � ������� �� �� ������ ������ Matrix, �������� bool
        [TestMethod]
        public void MatrixTryParse_Bool()
        {
            // arrange
            string str = "2,1 3,4 5. 6,9 7 3";
            bool expected = true;
            // act
            bool actual = Matrix.TryParse(str, out Matrix array);

            //assert
            Assert.AreEqual(expected, actual);
        }

        // #17 ����������� double[,] � ������ ������ Matrix
        [TestMethod]
        public void ArrayOfDoubles_MatrixFromArray()
        {
            // arrange
            double[,] array = { { 2.1, 3.4, 5, 6.9, 7, 3 }, { 1, 2, 3, 4, 5, 6 } };
            double[] expected = { 2.1, 3.4, 5, 6.9, 7, 3, 1, 2, 3, 4, 5, 6 };

            // act
            Matrix actualM = (Matrix)array;
            double[]? actual = actualM.GetData;
            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        // #18.1 ��������� ������
        [TestMethod]
        public void MatrixMultiplication_Matrix()
        {
            // arrange
            double[,] first = { {1, 2 }, { 4, 0.5 }};
            double[,] second = { { 1, 500, 0 }, { 0.5, 0.5, 0 }};
            double[] expected = { 2, 501, 0, 4.25, 2000.25, 0 };
            Matrix firstM = new(first);
            Matrix secondM = new(second);

            // act
            Matrix actualM = firstM * secondM;
            double[]? actual = actualM.GetData;
            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        // #18.2 ��������� ������ � ��������� �����������
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MatrixMultiplication_BadArguments_Matrix()
        {
            // arrange
            double[,] first = { { 1.1, 2.2, 3.3 }, { 4.4, 5.5, 6.6 }, { 7.7, 8.8, 9.9 } };
            double[,] second = { { 1.1, 2.2 }, {8.8, 9.9 } };
            Matrix firstM = new(first);
            Matrix secondM = new(second);

            // act
            Matrix actualM = firstM * secondM;
        }
    }
}