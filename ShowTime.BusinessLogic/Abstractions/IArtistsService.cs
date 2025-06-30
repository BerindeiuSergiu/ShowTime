using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess;
using ShowTime.DataAccess.Models;
using ShowTime.BussinessLogic.Dtos;
using ShowTime.BusinessLogic.Dtos;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IArtistsService
    {
        Task<ArtistGetDto> GetByIDAsync(int d);
        Task<IEnumerable<ArtistGetDto>> GetAllArtistsAsync();
        Task<Artists> AddArtistAsync(ArtistCreateDto artistCreateDto);
        Task<Artists> DeleteArtistAsync(int id);
        Task<Artists> UpdateArtistAsync(Artists artist);

    }
}
