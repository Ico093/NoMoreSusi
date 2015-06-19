using System.Data.Entity;

namespace NoMoreSusi.Data.Interfaces
{
    public interface INoMoreSusiData
    {
        DbContext Context { get; }

        int SaveChanges();

        void Dispose();
    }
}
