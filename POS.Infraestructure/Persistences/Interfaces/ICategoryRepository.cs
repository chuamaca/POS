using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

        Task<BaseEntityResponse<Category>> ListCategories(BaseFilterRequest filters);

    }
}
