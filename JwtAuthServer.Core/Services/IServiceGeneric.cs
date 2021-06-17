using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using SharedLibrary.Dtos;

namespace JwtAuthServer.Core.Services
{
    public interface IServiceGeneric<TEntity, TDto> where TEntity: class where TDto: class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<TDto>> AddAsync(TDto dto);
        Task<Response<NoDataDto>> Remove(int id);
        Task<Response<NoDataDto>> Update(int id, TDto dto);
    }
}
