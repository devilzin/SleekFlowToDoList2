using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SleekFlowToDoList2.Models
{
    public class TODORepository : ITODORepository
    {
        private readonly AppDBContext appDBContext;
        public TODORepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        //Add new task
        public async Task<TODO> AddTODO(TODO todo)
        {
            var result = await appDBContext.TODOs.AddAsync(todo);
            await appDBContext.SaveChangesAsync();
            return result.Entity;
        }

        //Delete task
        public async Task DeleteTODO(int ID)
        {
            var result = await appDBContext.TODOs.FirstOrDefaultAsync(e => e.ID == ID);

            if (result != null)
            {
                appDBContext.TODOs.Remove(result);
                await appDBContext.SaveChangesAsync();
            }
        }
        //Get Task by ID
        public async Task<TODO> GetTODO(int ID)
        {
            return await appDBContext.TODOs.FirstOrDefaultAsync(e => e.ID == ID);
        }
        //Get Task by Description
        public async Task<TODO> GetTODODescription(String Description)
        {
            return await appDBContext.TODOs.FirstOrDefaultAsync(e => e.Description == Description);
        }
        //Get All Task List
        public async Task<IEnumerable<TODO>> GetTODOs()
        {
            return await appDBContext.TODOs.ToListAsync();
        }
        //Search Task by Filtering and Sorting
        public async Task<IEnumerable<TODO>> Search(string FilterString, string sortOrder)
        {
            IQueryable<TODO> query = appDBContext.TODOs;

            //Filtering
            if (!string.IsNullOrEmpty(FilterString))
            {
                query = query.Where(e => e.Name.Contains(FilterString) || e.Description.Contains(FilterString));
            }

            //Sorting
            switch (sortOrder)
            {
                case "Descending":
                    query = query.OrderByDescending(s => s.Name);
                    break;
                case "Ascending":
                    query = query.OrderBy(s => s.Name);
                    break;
            }
            return await query.ToListAsync();
        }
        // Update task
        public async Task<TODO> UpdateTODO(TODO todo)
        {
            var result = await appDBContext.TODOs.FirstOrDefaultAsync(e => e.ID == todo.ID);

            if (result != null)
            {
                result.Name = todo.Name;
                result.Description = todo.Description;
                result.DueDate = todo.DueDate;
                if (todo.Status != 0)
                {
                    result.Status = todo.Status;
                }
                await appDBContext.SaveChangesAsync();

                return null;
            }

            return null;
        }
    }
}
