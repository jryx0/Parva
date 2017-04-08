namespace Parva.Domain.Core
{
    public interface IRowVersion
    {
        byte[] RowVersion { get; set; }
    }
}