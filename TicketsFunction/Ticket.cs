using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsFunction
{
    internal class Ticket
    {
        public int concertId { get; set; } = 0;

        [MaxLength(10, ErrorMessage = "Name must be less than 10 characters.")]
        [Required(ErrorMessage = "Name is required")]
        [DefaultValue("Liam")]
        public string name { get; set; } = "Liam";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        [DefaultValue("1234567891")]
        public string phone { get; set; } = "3299123";

        [Required(ErrorMessage = "A Quantity is required")]
        [Range(1, 5, ErrorMessage = "Quantity must be between 1 and 5.")]
        public int quantity { get; set; } = 1;

        [Required(ErrorMessage = "Credit card is required")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Invalid Credit Card Format")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Credit card must contain only numeric digits.")]
        [DefaultValue("1234567890123456")]
        public string creditCard { get; set; } = "1234567890123456";

        [Required(ErrorMessage = "Expiration field is required")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Expiration date must be in MM/YY format.")]
        public string expiration { get; set; } = string.Empty;

        [Required(ErrorMessage = "Security Code is required")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "Invalid security code. It must be 3 or 4 digits.")]
        [MinLength(3, ErrorMessage = "Security code must be at least 3 digits.")]
        [MaxLength(4, ErrorMessage = "Security code must be at most 4 digits.")]
        public string securityCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(20, ErrorMessage = "Address must be less than 20 characters.")]
        [DefaultValue("123 Main St")]
        public string address { get; set; } = "123 Main St";

        [Required(ErrorMessage = "City is required")]
        [MaxLength(20, ErrorMessage = "City must be less than 20 characters.")]
        [DefaultValue("Halifax")]
        public string city { get; set; } = "Halifax";

        [Required(ErrorMessage = "Province is required")]
        [MaxLength(20, ErrorMessage = "Province must be less than 50 characters.")]
        [DefaultValue("Nova Scotia")]
        public string province { get; set; } = "Nova Scotia";

        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$", ErrorMessage = "Invalid postal code format. Use format A1A 1A1.")]
        public string postalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(20, ErrorMessage = "Country must be less than 20 characters.")]
        [DefaultValue("Canada")]
        public string country { get; set; } = "Canada";
    }
}
