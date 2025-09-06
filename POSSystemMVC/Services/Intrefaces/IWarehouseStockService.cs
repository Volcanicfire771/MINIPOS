using POSSystemMVC.Models;

public interface IWarehouseStockService
{
    IEnumerable<WarehouseStock> GetAll();
    WarehouseStock GetById(int id);
    void Add(WarehouseStock stock);
    void Update(WarehouseStock stock);
    void Delete(int id);
    void Save();
}
