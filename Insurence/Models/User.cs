using System.ComponentModel.DataAnnotations;

namespace Insurance.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Zadejte jméno")]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Zadejte příjmení")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Zadejte telefonní číslo")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Zadejte email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email zadán v nesprávném formátu")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zadejte ulici a číslo popisné/orientační")]
        [Display(Name = "Ulice a číslo popisné/orientační")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Zadejte město")]
        [Display(Name = "Město")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zadejte PSČ")]
        [Display(Name = "PSČ")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Zadejte stát")]
        [Display(Name = "Stát")]
        public string Country { get; set; }

        public virtual ICollection<UserInsurance>? UserInsuranceRecords { get; set; }
    }
}
