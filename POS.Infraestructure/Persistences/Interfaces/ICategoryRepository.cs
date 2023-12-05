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
    public interface ICategoryRepository 
    {

        Task<BaseEntityResponse<Category>> ListCategories(BaseFilterRequest filters);

        Task<IEnumerable<Category>> ListSelectCategories();
        Task <Category> CategoryById(int categoryId);

        Task<bool> RegisterCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> RemoveCategory(int categoryId);
       
    }
}
