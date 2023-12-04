using POS.Infraestructure.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using POS.Infraestructure.Helpers;
using POS.Infraestructure.Commons.Bases.Request;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class GenericRepository <T> : IGenericRepository<T> where T : class
    {

        protected IQueryable<TDTO> Ordering<TDTO> (BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination=false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");
            if (pagination) queryDto = queryDto.Paginate(request);

            return queryDto;
        }
    }
}
