namespace Insurence.Models
{
    public class UserIndexData
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<InsuranceRecord> InsuranceRecords { get; set; }
    }
}
