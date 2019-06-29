using System;

namespace AzureFunctions.MyMedicine
{
    public class User : IDomain
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}