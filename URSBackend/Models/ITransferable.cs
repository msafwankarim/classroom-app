namespace URSBackend.Models
{
    public interface ITransferable
    {
        string GetJson();
        void CopyFrom(object obj);
    }
}