namespace InventoryManagementSystem.Models.Interfaces
{
    public interface Imanager<T>
    {
        public Task Save(T ToBeSaved);
        public Task<T> Load();
    }
}
