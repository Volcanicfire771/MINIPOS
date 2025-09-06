using POSSystemMVC.Models;

public interface IWarehouseService
{
    IEnumerable<Warehouse> GetAll();
    Warehouse GetById(int id);
    void Add(Warehouse warehouse);
    void Update(Warehouse warehouse);
    void Delete(int id);
    void Save();
}
