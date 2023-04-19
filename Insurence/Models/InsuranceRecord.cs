using Insurance.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance.Models
{
    public class InsuranceRecord
    {
        [Key]
        public int InsuranceId { get; set; }

        [Required(ErrorMessage = "Zadejte druh pojištění")]
        [Display(Name = "Druh pojištění")]
        //public InsuranceCategory InsuranceCategory { get; set; }
        public string InsuranceRecordName { get; set;}

        [Required(ErrorMessage = "Zadejte celé číslo výše pojištění")]
        [Display(Name = "Částka výše pojištění")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Zadejte předmět pojištění")]
        [Display(Name = "Předmět pojištění")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Zadejte od kdy má pojištění platit")]
        [DataType(DataType.Date)]
        [Display(Name = "Od kdy")]
        public DateTime InsuredSince { get; set; }

        [Required(ErrorMessage = "Zadejte do kdy má pojištění platit")]
        [DataType(DataType.Date)]
        [Display(Name = "Do kdy")]
        public DateTime InsuredUntil { get; set; }

        public virtual ICollection<UserInsurance>? UserInsuranceRecords { get; set; }
    }
}
