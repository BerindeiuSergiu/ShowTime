using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Services
{
    public class FestivalService : IFestivalService
    {
        private readonly IGenericRepository<Festival> _festivalRepository;

        public FestivalService(IGenericRepository<Festival> festivalRepository)
        {
            _festivalRepository = festivalRepository ?? throw new ArgumentNullException(nameof(festivalRepository));
        }

        public async Task<FestivalGetDto> GetByIDAsync(int id)
        {
            try
            {
                var festival = await _festivalRepository.GetByIdAsync(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                return new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt,
                    Capacity = festival.Capacity

                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the festival by ID.", ex);
            }
        }

        public async Task<IList<FestivalGetDto>> GetAllFestivalsAsync()
        {
            try
            {
                var festivals = await _festivalRepository.GetAllAsync();
                return festivals.Select(festival => new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt,
                    Capacity = festival.Capacity
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving all festivals.", e);
            }
        }

        public async Task<Festival> AddFestivalAsync(FestivalCreateDto festivalDto)
        {
            try
            {
                var festival = new Festival
                {
                    Name = festivalDto.Name,
                    Location = festivalDto.Location,
                    StartDate = festivalDto.StartDate,
                    EndDate = festivalDto.EndDate,
                    SplashArt = festivalDto.SplashArt,
                    Capacity = festivalDto.Capacity
                };
                var addedFestival = await _festivalRepository.AddAsync(festival);
                return new Festival
                {
                    Id = addedFestival.Id,
                    Name = addedFestival.Name,
                    Location = addedFestival.Location,
                    StartDate = addedFestival.StartDate,
                    EndDate = addedFestival.EndDate,
                    SplashArt = addedFestival.SplashArt,
                    Capacity = addedFestival.Capacity
                };
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while adding the festival.", e);
            }
        }

        public async Task<Festival> DeleteFestivalAsync(int id)
        {
            try
            {
                var festival = await _festivalRepository.GetByIdAsync(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                _festivalRepository.Delete(festival);
                return new Festival
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt,
                    Capacity = festival.Capacity
                };
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while deleting the festival.", e);
            }
        }

        public async Task<Festival> UpdateFestivalAsync(FestivalUpdateDto festivalUpdateDto)
        {
            try
            {
                var festival = await _festivalRepository.GetByIdAsync(festivalUpdateDto.Id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {festivalUpdateDto.Id} not found.");
                }
                
                festival.Name = festivalUpdateDto.Name;
                festival.Location = festivalUpdateDto.Location;
                festival.StartDate = festivalUpdateDto.StartDate;
                festival.EndDate = festivalUpdateDto.EndDate;
                festival.SplashArt = festivalUpdateDto.SplashArt;
                festival.Capacity = festivalUpdateDto.Capacity;
                var updatedFestival = await _festivalRepository.UpdateAsync(festival);


                return new Festival
                {
                    Id = updatedFestival.Id,
                    Name = updatedFestival.Name,
                    Location = updatedFestival.Location,
                    StartDate = updatedFestival.StartDate,
                    EndDate = updatedFestival.EndDate,
                    SplashArt = updatedFestival.SplashArt,
                    Capacity = updatedFestival.Capacity
                };
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while updating the festival.", e);
            }
        }


        public async Task<IList<FestivalGetDto>> GetFestivalsByArtistIdAsync(int artistID)
        {
            try
            {
                var allFestivals = await _festivalRepository.GetAllAsync();
                var filteredFestivals = allFestivals.Where(f => f.Artists.Any(a => a.Id == artistID)).ToList();

                return filteredFestivals.Select(festival => new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.StartDate,
                    SplashArt = festival.SplashArt,
                    Capacity = festival.Capacity
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving festivals by artist ID.", e);
            }
        }


        public async Task<IList<FestivalGetDto>> GetFestivalsByLocationAsync(string location)
        {
            try
            {
                // did a preliminary check to see if the input is correct
                if (string.IsNullOrWhiteSpace(location))
                {
                    throw new ArgumentException("Location cannot be null or empty.", nameof(location));
                }

                // not the most efficient way, but it works for now
                var allFestivals = await _festivalRepository.GetAllAsync();
                var festivals = allFestivals.Where(f => f.Location.ToLower().Contains(location.ToLower())).ToList();

                return festivals.Select(festival => new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt,
                    Capacity = festival.Capacity
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving festivals by location.", e);
            }
        }



    }
}
