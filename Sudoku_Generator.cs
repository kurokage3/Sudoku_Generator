using System;
using System.Linq;

namespace Sudoku_Generator;

class Program
{
    private static Random random = new Random();

    static void Main()
    {
        int[,] sudokuGrid = GenerateSudokuGrid();
        PrintSudokuGrid(sudokuGrid);
    }

    static int[,] GenerateSudokuGrid()
    {
        int[,] grid = new int[9, 9];

        FillValues(grid, 0, 0);

        RemoveValues(grid, 20);

        return grid;
    }

    static bool FillValues(int[,] grid, int row, int col)
    {
        if (row == 9)
        {
            return true;
        }

        int nextRow = (col == 8) ? row + 1 : row;
        int nextCol = (col + 1) % 9;

        if (grid[row, col] != 0)
        {
            return FillValues(grid, nextRow, nextCol);
        }

        int[] possibleValues = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ShuffleArray(possibleValues);

        for (int i = 0; i < 9; i++)
        {
            if (IsValid(grid, row, col, possibleValues[i]))
            {
                grid[row, col] = possibleValues[i];

                if (FillValues(grid, nextRow, nextCol))
                {
                    return true;
                }
            }
        }

        grid[row, col] = 0;
        return false;
    }

    static int FindNextValidValue(int[,] grid, int row, int col)
    {
        int[] possibleValues = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ShuffleArray(possibleValues);

        for (int i = 0; i < 9; i++)
        {
            if (IsValid(grid, row, col, possibleValues[i]))
            {
                return possibleValues[i];
            }
        }

        throw new Exception("No valid value found");
    }

    static void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    static bool IsValid(int[,] grid, int row, int col, int value)
    {
        return IsRowValid(grid, row, value) &&
               IsColValid(grid, col, value) &&
               IsBoxValid(grid, row - row % 3, col - col % 3, value);
    }

    static bool IsRowValid(int[,] grid, int row, int value)
    {
        for (int col = 0; col < 9; col++)
        {
            if (grid[row, col] == value)
            {
                return false;
            }
        }
        return true;
    }

    static bool IsColValid(int[,] grid, int col, int value)
    {
        for (int row = 0; row < 9; row++)
        {
            if (grid[row, col] == value)
            {
                return false;
            }
        }
        return true;
    }

    static bool IsBoxValid(int[,] grid, int row, int col, int value)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[row + i, col + j] == value)
                {
                    return false;
                }
            }
        }
        return true;
    }

    static void RemoveValues(int[,] grid, int numberOfValuesToRemove)
    {
        while (numberOfValuesToRemove > 0)
        {
            int row = random.Next(0, 9);
            int col = random.Next(0, 9);

            if (grid[row, col] != 0)
            {
                               grid[row, col] = 0;
                numberOfValuesToRemove--;
            }
        }
    }

    static void PrintSudokuGrid(int[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            if (row % 3 == 0 && row != 0)
            {
                Console.WriteLine("- - - - - - - - - - -");
            }

            for (int col = 0; col < 9; col++)
            {
                if (col % 3 == 0 && col != 0)
                {
                    Console.Write("| ");
                }

                Console.Write(grid[row, col] == 0 ? ". " : grid[row, col] + " ");
            }

            Console.WriteLine();
        }
    }
}
