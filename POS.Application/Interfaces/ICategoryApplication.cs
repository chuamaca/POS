using POS.Application.Commons.Bases;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<BaseEntityResponse<CategoryResponseDTO>>> ListCategories(BaseFilterRequest filters);

        Task<BaseResponse<IEnumerable<CategorySelectResponseDTO>>> ListSelectCategories();
        Task<BaseResponse<CategoryResponseDTO>> CategoriesById(int categoryId);
        Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDTO requestDto);

        Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDTO requestDTO);
    }
}
