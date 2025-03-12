using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission10Assignment_Christensen.Models;

namespace Mission10Assignment_Christensen.Controllers;

[Route("[controller]")]
[ApiController]
public class BowlerController : ControllerBase
{
    private BowlingLeagueContext _BowlerContext;
    
    public BowlerController(BowlingLeagueContext temp)
    {
        _BowlerContext = temp;
    }
    
    [HttpGet("GetBowlersWithTeams")]
    public async Task<ActionResult<IEnumerable<BowlerViewModel>>> GetBowlersWithTeams()
    {
        var result = await _BowlerContext.Bowlers
            .Include(b => b.Team) // Ensure Team data is loaded
            .Where(b => b.Team.TeamName == "Marlins" || b.Team.TeamName == "Sharks")
            .Select(b => new BowlerViewModel
            {
                BowlerId = b.BowlerId,
                BowlerFirstName = b.BowlerFirstName,
                BowlerMiddleInit = b.BowlerMiddleInit,
                BowlerLastName = b.BowlerLastName,
                BowlerAddress = b.BowlerAddress,
                BowlerCity = b.BowlerCity,
                BowlerState = b.BowlerState,
                BowlerZip = b.BowlerZip,
                BowlerPhoneNumber = b.BowlerPhoneNumber,
                TeamName = b.Team.TeamName,
            })
            .ToListAsync();

        return Ok(result);
    }
}
