using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IFestivalService
    {
        Task<FestivalGetDto> GetByIDAsync(int id);
        Task<IList<FestivalGetDto>> GetAllFestivalsAsync();
        Task<Festival> AddFestivalAsync(FestivalCreateDto festivalCreateDto);
        Task<Festival> DeleteFestivalAsync(int id);
        Task<Festival> UpdateFestivalAsync(FestivalUpdateDto festivalUpdateDto);
        Task<IList<FestivalGetDto>> GetFestivalsByArtistIdAsync(int artistId);
        Task<IList<FestivalGetDto>> GetFestivalsByLocationAsync(string location);
    }
}
