namespace Insurance.Models
{
    public class UserInsurance
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int InsuranceId { get; set; }    
        public virtual InsuranceRecord InsuranceRecord { get; set; }      
    }
}
