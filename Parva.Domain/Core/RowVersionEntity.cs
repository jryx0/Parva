namespace Parva.Domain.Core
{
    public abstract class RowVersionEntity : BaseEntity, IRowVersion
    {
        public byte[] RowVersion { get; set; }

        
    }
}