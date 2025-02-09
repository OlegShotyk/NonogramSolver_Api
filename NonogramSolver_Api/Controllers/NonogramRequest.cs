namespace NonogramSolver_Api.Controllers
{
    public class NonogramRequest
    {
		private List<List<int>> _rowClues;

		public List<List<int>> RowClues
		{
			get { return _rowClues; }
			set { _rowClues = value; }
		}

        private List<List<int>> _colClues;

        public List<List<int>> ColClues
        {
            get { return _colClues; }
            set { _colClues = value; }
        }
    }
}
