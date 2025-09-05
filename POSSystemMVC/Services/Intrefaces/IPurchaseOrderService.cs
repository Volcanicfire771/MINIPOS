using POSSystemMVC.Models;
using System.Collections.Generic;

namespace POSSystemMVC.Services.Intrefaces
{
    public interface IPurchaseOrderService
    {
        IEnumerable<PurchaseOrder> GetAll();
        PurchaseOrder? GetById(int id);
        void Add(PurchaseOrder order);
        void Delete(int id);
    }
}
