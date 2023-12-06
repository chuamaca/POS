using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Category;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CategoryValidator _validationRules;

        public CategoryApplication(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidator validationRules )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<CategoryResponseDTO>>> ListCategories(BaseFilterRequest filters)
        {
            //Obtenemos Base Response
            var response = new BaseResponse<BaseEntityResponse<CategoryResponseDTO>>();

            var categories = await _unitOfWork.Category.ListCategories(filters);
            if (categories is not null)
            {
                //Base Responsae
                response.IsSuccess = true;
                //Se mapea par aMapper
                response.Data=_mapper.Map<BaseEntityResponse<CategoryResponseDTO>>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess= false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPY;
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<CategorySelectResponseDTO>>> ListSelectCategories()
        {
            var response = new BaseResponse<IEnumerable<CategorySelectResponseDTO>>();
            var categories= await _unitOfWork.Category.ListSelectCategories();
            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<CategorySelectResponseDTO>>(categories);
                response.Message= ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDTO requestDto)
        {
            var response = new BaseResponse<bool>();
            var validacionResult = await _validationRules.ValidateAsync(requestDto);
            if (!validacionResult.IsValid)
            {
                response.Message = ReplyMessage.MESSAGE_QUERY;
                response.Errors = validacionResult.Errors;
                return response;
            }

            var category = _mapper.Map<Category>(requestDto);
            response.Data = await _unitOfWork.Category.RegisterCategory(category);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<CategoryResponseDTO>> CategoriesById(int categoryId)
        {

            var response = new BaseResponse<CategoryResponseDTO>();
            var categories = await _unitOfWork.Category.CategoryById(categoryId);
            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<CategoryResponseDTO>(categories);
                response.Message = ReplyMessage.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPY;
            }

            return response;
        }



        public async Task<BaseResponse<bool>> UpdateCategory(int categoryId, CategoryRequestDTO requestDTO)
        {

            var response = new BaseResponse<bool>();
            var categoryEdit = await CategoriesById(categoryId);
            if (categoryEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPY;
            }

            var category= _mapper.Map<Category>(requestDTO);
            category.CategoryId = categoryId;

            response.Data = await _unitOfWork.Category.UpdateCategory(category);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveCategory(int categoryID)
        {
            var response= new BaseResponse<bool>();

            var category = await CategoriesById(categoryID);

            if (category.Data is null)
            {
                response.IsSuccess=false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPY;
            }
            response.Data = await _unitOfWork.Category.RemoveCategory(categoryID);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess=false;
                response.Message=ReplyMessage.MESSAGE_FAILED;
            }
            return response;
        }
    }
}
