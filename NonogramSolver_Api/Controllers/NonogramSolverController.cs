using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using NonogramSolver_Api.Models;
namespace NonogramSolver_Api.Controllers
{
    [Route("api/SolveNonogram")]
    [ApiController]
    public class NonogramSolverController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<string>> SolveNonogram([FromBody] NonogramRequest input)
        {
            NonogramSolver solver = new NonogramSolver();
            NonogramResponse response = new NonogramResponse();
            response.Solution = solver.SolveNonogram(input.RowClues, input.ColClues);
            if (response.Solution == null)
            {
                return BadRequest();
            }
            else
            {
                string solution = JsonSerializer.Serialize(response);
                return solution;
            }
            
        }
    }
}
