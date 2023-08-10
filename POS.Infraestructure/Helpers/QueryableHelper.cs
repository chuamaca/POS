using POS.Infraestructure.Commons.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Helpers
{
    public static class QueryableHelper
    {

        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, BasePaginationRequest request)
        {
            return queryable.Skip((request.NumPage -1) * request.Records).Take(request.Records);
        }

    }
}
