
using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Infraestructure.Persistences.Context;
using POS.Infraestructure.Persistences.Interfaces;
using POS.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly PosContext _context;

        public CategoryRepository(PosContext context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<Category>> ListCategories(BaseFilterRequest filters)
        {
            var response = new BaseEntityResponse<Category>();
            var listCategories = (from c in _context.Categories
                                  where c.AuditDeleteUser == null && c.AuditDeleteUser == null
                                  select c).AsNoTracking().AsQueryable();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        listCategories = listCategories.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        listCategories = listCategories.Where(x => x.Description!.Contains(filters.TextFilter));
                        break;
                        //Si hay mas casos de Fitros se añade mas
                }
            }

            if (filters.StateFilter is not null)
            {
                listCategories = listCategories.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                listCategories = listCategories
                                .Where(x => x.AuditCreateDate >= Convert.ToDateTime(filters.StartDate)
                                && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            if (filters.Sort is null) filters.Sort = "CategoryId";

            response.TotalRecords = await listCategories.CountAsync();

            response.Items = await Ordering(filters, listCategories,!(bool)filters.Download!).ToListAsync();
            return response;

        }
        public async Task<IEnumerable<Category>> ListSelectCategories()
        {
            var listCategories = await _context.Categories.Where(
                                            c=> c.State.Equals((int)StateTypes.Active) && c.AuditDeleteDate==null && 
                                            c.AuditDeleteUser==null).AsNoTracking().ToListAsync();
            return listCategories;
        }


        public async Task<Category> CategoryById(int categoryId)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x=> x.CategoryId == categoryId);
            return category!;
        }
        
        public async Task<bool> RegisterCategory(Category category)
        {
            category.AuditCreateUser = 1;
            category.AuditCreateDate= DateTime.Now;
            await _context.AddAsync(category);
            var recordsAffected= await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            category.AuditUpdateUser = 1;
            category.AuditUpdateDate = DateTime.Now;
            _context.Update(category);
            _context.Entry(category).Property(e => e.AuditCreateUser).IsModified=false;
            _context.Entry(category).Property(e => e.AuditCreateDate).IsModified=false;

            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public async Task<bool> RemoveCategory(int categoryId)
        {
             var category =  await _context.Categories.AsNoTracking().SingleOrDefaultAsync(x=> x.CategoryId.Equals(categoryId));
            category!.AuditDeleteUser = 1;
            category.AuditDeleteDate= DateTime.Now;

            _context.Update(category);

            var recordsAfected= await _context.SaveChangesAsync();

            return recordsAfected > 0;
        }

        public Task<bool> EditCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
