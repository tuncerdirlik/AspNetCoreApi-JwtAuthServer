using JwtAuthServer.Core.Repositories;
using JwtAuthServer.Core.Services;
using JwtAuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthServer.Service.Services
{
    public class ServiceGeneric<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public ServiceGeneric(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(dto);
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var lstEntity = await _genericRepository.GetAllAsync();
            var lstDto = ObjectMapper.Mapper.Map<List<TDto>>(lstEntity);

            return Response<IEnumerable<TDto>>.Success(lstDto, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity != null)
            {
                var dto = ObjectMapper.Mapper.Map<TDto>(entity);
                return Response<TDto>.Success(dto, 200);
            }
            else
            {
                return Response<TDto>.Failed("Id not found", 404, true);
            }
            
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var dbEntity = await _genericRepository.GetByIdAsync(id);
            if (dbEntity != null)
            {
                _genericRepository.Remove(dbEntity);
                return Response<NoDataDto>.Success(204);
            }
            else
            {
                return Response<NoDataDto>.Failed("Id not found", 404, true);
            }

        }

        public async Task<Response<NoDataDto>> Update(int id, TDto dto)
        {
            var dbEntity = await _genericRepository.GetByIdAsync(id);
            if (dbEntity != null)
            {
                var entity = ObjectMapper.Mapper.Map<TEntity>(dto);
                _genericRepository.Update(entity);
                await _unitOfWork.CommitAsync();

                return Response<NoDataDto>.Success(204);
            }
            else
            {
                return Response<NoDataDto>.Failed("Id not found", 404, true);
            }
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);
            var lstDto = ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync());

            return Response<IEnumerable<TDto>>.Success(lstDto, 200);
        }
    }
}
