using POSSystemMVC.Models;
using System.Collections.Generic;

namespace POSSystemMVC.Services.Intrefaces
{
    public interface ISalesExecutiveService
    {
        IEnumerable<SalesExecutive> GetAll();

        void Update(SalesExecutive ex);
        void Add(SalesExecutive ex);
        void Delete(int id);
    }
}
