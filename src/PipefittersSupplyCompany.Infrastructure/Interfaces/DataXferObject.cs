using System;


namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public abstract class DataXferObject
    {
        public RecordStatus Status { get; set; } = RecordStatus.Unmodified;
    }

    public enum RecordStatus : int
    {
        Unmodified = 1,
        New = 2,
        Modified = 3,
        Deleted = 4
    }
}