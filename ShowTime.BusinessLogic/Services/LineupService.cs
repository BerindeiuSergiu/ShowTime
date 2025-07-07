using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.GenericInterface;
using ShowTime.DataAccess.Models;
using ShowTime.BussinessLogic.Dtos;

namespace ShowTime.BusinessLogic.Services
{
    public class LineupService : ILineupService
    {
        private readonly IGenericRepository<Lineup> _lineupRepository;

        public LineupService(IGenericRepository<Lineup> lineupRepository)
        {
            _lineupRepository = lineupRepository ?? throw new ArgumentNullException(nameof(lineupRepository));
        }

        public async Task<Lineup> AddLineupAsync(LineupCreateDto lineupCreateDto)
        {
            try
            {
                // Check if this combination already exists to avoid duplicates
                var allLineups = await _lineupRepository.GetAllAsync();
                var existingLineup = allLineups.FirstOrDefault(l =>
                    l.FestivalId == lineupCreateDto.FestivalId &&
                    l.ArtistId == lineupCreateDto.ArtistId);

                if (existingLineup != null)
                {
                    throw new InvalidOperationException($"Lineup with Festival ID {lineupCreateDto.FestivalId} and Artist ID {lineupCreateDto.ArtistId} already exists.");
                }

                var newLineup = new Lineup
                {
                    FestivalId = lineupCreateDto.FestivalId,
                    ArtistId = lineupCreateDto.ArtistId,
                    Stage = lineupCreateDto.Stage,
                    StartTime = lineupCreateDto.StartTime
                };

                return await _lineupRepository.AddAsync(newLineup);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a lineup.", ex);
            }
        }

        public async Task<IList<LineupGetDto>> GetLineupsByFestivalAsync(int festivalId)
        {
            try
            {
                var allLineups = await _lineupRepository.GetAllAsync();
                var lineups = allLineups.Where(l => l.FestivalId == festivalId);

                return lineups.Select(l => new LineupGetDto
                {
                    FestivalId = l.FestivalId,
                    ArtistId = l.ArtistId,
                    Stage = l.Stage,
                    StartTime = l.StartTime
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving lineups by festival.", ex);
            }
        }

        public async Task<IList<LineupGetDto>> GetLineupsByArtistAsync(int artistId)
        {
            try
            {
                var allLineups = await _lineupRepository.GetAllAsync();
                var lineups = allLineups.Where(l => l.ArtistId == artistId);

                return lineups.Select(l => new LineupGetDto
                {
                    FestivalId = l.FestivalId,
                    ArtistId = l.ArtistId,
                    Stage = l.Stage,
                    StartTime = l.StartTime
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving lineups by artist.", ex);
            }
        }

        public async Task<Lineup> RemoveLineupAsync(int festivalId, int artistId)
        {
            try
            {
                var allLineups = await _lineupRepository.GetAllAsync();
                var lineup = allLineups.FirstOrDefault(l =>
                    l.FestivalId == festivalId &&
                    l.ArtistId == artistId);

                if (lineup == null)
                {
                    throw new KeyNotFoundException($"Lineup with Festival ID {festivalId} and Artist ID {artistId} not found.");
                }

                _lineupRepository.Delete(lineup);
                return lineup;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing a lineup.", ex);
            }
        }

        public async Task<LineupGetDto> GetLineupByIdsAsync(int festivalId, int artistId)
        {
            try
            {
                var allLineups = await _lineupRepository.GetAllAsync();
                var lineup = allLineups.FirstOrDefault(l =>
                    l.FestivalId == festivalId &&
                    l.ArtistId == artistId);

                if (lineup == null)
                {
                    throw new KeyNotFoundException($"Lineup with Festival ID {festivalId} and Artist ID {artistId} not found.");
                }

                return new LineupGetDto
                {
                    FestivalId = lineup.FestivalId,
                    ArtistId = lineup.ArtistId,
                    Stage = lineup.Stage,
                    StartTime = lineup.StartTime
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting a lineup by IDs.", ex);
            }
        }

        public async Task<IList<LineupGetDto>> GetAllLineupsAsync()
        {
            try
            {
                var lineups = await _lineupRepository.GetAllAsync();

                return lineups.Select(lineup => new LineupGetDto
                {
                    FestivalId = lineup.FestivalId,
                    ArtistId = lineup.ArtistId,
                    Stage = lineup.Stage,
                    StartTime = lineup.StartTime
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all lineups.", ex);
            }
        }

        public async Task<Lineup> UpdateLineupAsync(LineupUpdateDto lineupUpdateDto)
        {
            try
            {
                var allLineups = await _lineupRepository.GetAllAsync();
                var existingLineup = allLineups.FirstOrDefault(l =>
                    l.FestivalId == lineupUpdateDto.FestivalId &&
                    l.ArtistId == lineupUpdateDto.ArtistId);

                if (existingLineup == null)
                {
                    throw new KeyNotFoundException($"Lineup with Festival ID {lineupUpdateDto.FestivalId} and Artist ID {lineupUpdateDto.ArtistId} not found.");
                }

                // Update properties
                existingLineup.Stage = lineupUpdateDto.Stage;
                existingLineup.StartTime = lineupUpdateDto.StartTime;

                return await _lineupRepository.UpdateAsync(existingLineup);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating a lineup.", ex);
            }
        }
    }
}