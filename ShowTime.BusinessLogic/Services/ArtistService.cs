using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.BussinessLogic.Dtos;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess.Models;
namespace ShowTime.BusinessLogic.Services
{
    public class ArtistService : IArtistsService
    {
        private readonly IGenericRepository<Artists> _artistRepository; // dependency injection
        public ArtistService(IGenericRepository<Artists> artistRepository)
        {
            _artistRepository = artistRepository ?? throw new ArgumentNullException(nameof(artistRepository));
        }


        public async Task<ArtistGetDto> GetByIDAsync(int id)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);

                if ( artist == null )
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }

                return new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Genre = artist.Genre,
                    Image = artist.Image // asta e url
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while getting an artist bt ID");
            }
        }
        public async Task<IList<ArtistGetDto>> GetAllArtistsAsync()
        {
            try
            {
                var artists = await _artistRepository.GetAllAsync();

                // Select pe un obiect Ienumerable -> prin Dto cream toate obiectele separate
                // folosim pentru a nu aduce 100 de prop degeaba si putem sa extragem
                // doar ce dorim
                // pentru fiecare element din lista creeam un nou obiect ArtistsGetDto
                // artists => new ArtistGetDto

                return artists.Select(artists => new ArtistGetDto
                {
                    Id = artists.Id,
                    Name = artists.Name,
                    Genre = artists.Genre,
                    Image = artists.Image // asta e url
                }).ToList();
            }
            catch(Exception e)
            {
                throw new Exception("An error occurred while retrieving all artists.", e);
            }
        }

        public async Task<Artists> AddArtistAsync(ArtistCreateDto artistCreateDto)
        {
            try
            {
                var artist = new Artists
                {
                    Name = artistCreateDto.Name,
                    Genre = artistCreateDto.Genre,
                    Image = artistCreateDto.Image // asta e url
                };

                return await _artistRepository.AddAsync(artist);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding an artist.", ex);
            }
        }
        public async Task<Artists> DeleteArtistAsync(int id)
        {
            try
            {
                var artist = await _artistRepository.GetByIdAsync(id);
                if (artist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }
                _artistRepository.Delete(artist);
                return artist;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting an artist.", ex);
            }
        }
        public async Task<Artists> UpdateArtistAsync(ArtistUpdateDto artistUpdater)
        {
            try
            {
                var existingArtist = await _artistRepository.GetByIdAsync(artistUpdater.Id);
                if (existingArtist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {artistUpdater.Id} not found.");
                }
                // update code
                existingArtist.Name = artistUpdater.Name;
                existingArtist.Genre = artistUpdater.Genre;
                existingArtist.Image = artistUpdater.Image; // asta e url
                return await _artistRepository.UpdateAsync(existingArtist);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update artist.", ex);
            }

        }
    }
}
