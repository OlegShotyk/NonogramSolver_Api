namespace NonogramSolver_Api.Models
{
    public class NonogramResponse
    {
		private int[,] _solution;

		public int[,] Solution
		{
			get { return _solution; }
			set { _solution = value; }
		}

	}
}
