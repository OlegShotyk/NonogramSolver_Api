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
        public async Task<ActionResult<string>> SolveNonogram([FromBody] string input)
        {
            NonogramSolver solver = new NonogramSolver();
            string response = solver.ConvertBoard();
            return response;
        }
    }
}
