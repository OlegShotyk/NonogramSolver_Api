using System.Text.Json;

namespace NonogramSolver_Api.Models
{
    public class NonogramSolver
    {
        public int[,] SolveNonogram(List<List<int>> rowConstraints, List<List<int>> columnConstraints)
        {
            int rows = rowConstraints.Count;
            int cols = columnConstraints.Count;
            int[,] grid = new int[rows, cols]; // 0 = empty, 1 = filled, -1 = unknown
            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    grid[rowIndex, colIndex] = -1;
                }
            }

            if (Solve(0, 0, grid, rowConstraints, columnConstraints))
            {
                return grid;
            }
            return null;
            throw new Exception("No solution found.");
        }

        private bool Solve(int row, int col, int[,] grid, List<List<int>> rowConstraints, List<List<int>> columnConstraints)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // If we've completed all rows, validate the entire grid
            if (row == rows)
            {

                if (!IsValid(grid, rowConstraints, columnConstraints))
                {
                    return false;
                }

                if (IsValid(grid, rowConstraints, columnConstraints))
                {
                    return true;
                }


                return IsValid(grid, rowConstraints, columnConstraints);
            }


            int nextRow = (col == cols - 1) ? row + 1 : row;
            int nextCol = (col == cols - 1) ? 0 : col + 1;


            grid[row, col] = 1;
            if (IsValidPartial(grid, rowConstraints, columnConstraints, row, col) &&
                Solve(nextRow, nextCol, grid, rowConstraints, columnConstraints))
            {
                return true;
            }


            grid[row, col] = 0;
            if (IsValidPartial(grid, rowConstraints, columnConstraints, row, col) &&
                Solve(nextRow, nextCol, grid, rowConstraints, columnConstraints))
            {
                return true;
            }


            grid[row, col] = -1;
            return false;
        }

        private bool IsValid(int[,] grid, List<List<int>> rowConstraints, List<List<int>> columnConstraints)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);


            for (int row = 0; row < rows; row++)
            {
                List<int> rowSequence = ExtractSequence(grid, row, true);
                if (!IsFullyFilled(rowSequence) || !MatchSequence(rowSequence, rowConstraints[row]))
                {
                    return false;
                }
            }


            for (int col = 0; col < cols; col++)
            {
                List<int> colSequence = ExtractSequence(grid, col, false);
                if (!IsFullyFilled(colSequence) || !MatchSequence(colSequence, columnConstraints[col]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidPartial(int[,] grid, List<List<int>> rowConstraints, List<List<int>> columnConstraints, int row, int col)
        {

            List<int> rowSequence = ExtractSequence(grid, row, true);
            if (!MatchPartialSequence(rowSequence, rowConstraints[row]))
            {
                return false;
            }


            List<int> colSequence = ExtractSequence(grid, col, false);
            if (!MatchPartialSequence(colSequence, columnConstraints[col]))
            {
                return false;
            }

            return true;
        }

        private bool MatchSequence(List<int> actual, List<int> expected)
        {
            List<int> groups = new List<int>();
            int count = 0;

            foreach (var cell in actual)
            {
                if (cell == 1)
                {
                    count++;
                }
                else if (count > 0)
                {
                    groups.Add(count);
                    count = 0;
                }
            }

            if (count > 0) groups.Add(count);


            if (groups.Count != expected.Count) return false;


            int actualIndex = 0;
            foreach (var clue in expected)
            {
                while (actualIndex < actual.Count && actual[actualIndex] == 0 || actual[actualIndex] == -1)
                {
                    actualIndex++;
                }

                for (int i = 0; i < clue; i++)
                {
                    if (actualIndex >= actual.Count || actual[actualIndex] != 1)
                    {
                        return false;
                    }
                    actualIndex++;
                }


                if (actualIndex < actual.Count && actual[actualIndex] == 1)
                {
                    return false;
                }
            }

            return true;
        }

        private bool MatchPartialSequence(List<int> actual, List<int> expected)
        {
            List<int> groups = new List<int>();
            int count = 0;

            foreach (var cell in actual)
            {
                if (cell == 1)
                {
                    count++;
                }
                else if (count > 0)
                {
                    groups.Add(count);
                    count = 0;
                }
            }

            if (count > 0) groups.Add(count);


            for (int i = 0; i < groups.Count; i++)
            {
                if (i >= expected.Count || groups[i] > expected[i])
                {
                    return false;
                }
            }


            if (groups.Count < expected.Count && actual.LastOrDefault() == 1)
            {
                return false;
            }

            return true;
        }

        private List<int> ExtractSequence(int[,] grid, int index, bool isRow)
        {
            List<int> sequence = new List<int>();
            int length = isRow ? grid.GetLength(1) : grid.GetLength(0);

            for (int i = 0; i < length; i++)
            {
                sequence.Add(isRow ? grid[index, i] : grid[i, index]);
            }
            return sequence;
        }

        private bool IsFullyFilled(List<int> sequence)
        {
            return sequence.All(cell => cell != -1);
        }

        public string ConvertBoard()
        {
            var rowClues = new List<List<int>>
            {
                new List<int> { 3 },
                new List<int> { 1, 1, 1 },
                new List<int> { 5 },
                new List<int> { 5 },
                new List<int> { 3 },
                new List<int> { 1 },
                new List<int> { 1 },
                new List<int> { 2, 2 },
                new List<int> { 1, 2, 1 },
                new List<int> { 1, 1 },
                new List<int> { 1, 2 },
                new List<int> { 2, 1 },
                new List<int> { 3 }
            };

            var colClues = new List<List<int>>
            {
                new List<int> { 3, 3 },
                new List<int> { 1, 3, 2, 2 },
                new List<int> { 8, 1 },
                new List<int> { 1, 3, 2, 1 },
                new List<int> { 3, 1, 2 },
                new List<int> { 2 },
                new List<int> { 1 },
                new List<int> { 1 }
            };
            int[,] solution = SolveNonogram(rowClues, colClues);
            string json = JsonSerializer.Serialize(solution);
            if (solution == null)
            {
                return "failed to solve the board.";
            }
            return json;
        }
    }
}
