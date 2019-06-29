using System;

namespace AzureFunctions.MyMedicine
{
    public class Medicine : IDomain
    {
        public string name { get; set; }
        public string picture { get; set; }
        public string compositionCount { get; set; } = null;
        public int amount { get; set; }
        public string amountType { get; set; }
        public DateTime? dueDate { get; set; }
        public string drugType { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
