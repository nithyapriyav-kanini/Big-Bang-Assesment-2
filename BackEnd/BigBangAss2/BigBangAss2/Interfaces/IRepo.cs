namespace BigBangAss2.Interfaces
{
    public interface IRepo <T,I>
    {
        public Task<T?> Add(T item);
        public Task<T?> Update(T item);
        public Task<T?> Delete(I id);
        public Task<T?> Get(I id);
        public Task<ICollection<T>> GetAll();
    }
}
