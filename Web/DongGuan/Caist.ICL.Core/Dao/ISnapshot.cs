namespace Caist.ICL.Core.Dao
{
    public interface ISnapshot<T>
    {
        void Update(T newEntity);
    }
}
