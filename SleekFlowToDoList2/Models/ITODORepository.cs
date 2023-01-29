using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SleekFlowToDoList2.Models
{
    public interface ITODORepository
    {
        Task<IEnumerable<TODO>> Search(string FilterString, string sortOrder);
        Task<TODO> GetTODO(int ID);
        Task<IEnumerable<TODO>> GetTODOs();
        Task<TODO> GetTODODescription(string Description);
        Task<TODO> AddTODO(TODO todo);
        Task<TODO> UpdateTODO(TODO todo);
        Task DeleteTODO(int ID);
    }
}
