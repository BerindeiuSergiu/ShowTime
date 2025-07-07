using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface ILineupService
    {
        Task<Lineup> AddLineupAsync(LineupCreateDto lineupCreateDto);
        Task<IList<LineupGetDto>> GetLineupsByFestivalAsync(int festivalId);
        Task<IList<LineupGetDto>> GetLineupsByArtistAsync(int artistId);
        Task<Lineup> RemoveLineupAsync(int festivalId, int artistId);
        Task<LineupGetDto> GetLineupByIdsAsync(int festivalId, int artistId);
        Task<IList<LineupGetDto>> GetAllLineupsAsync();
        Task<Lineup> UpdateLineupAsync(LineupUpdateDto lineupUpdateDto);
    }
} 