using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelixService.MVC.Models
{
    [Table("Employee_Master")]
    public class Employee
    {
        [Key]
        [Column("employee_master_guid")]
        public Guid Guid { get; set; }

        [Display(Name = "First Name")]
        [Column("employee_first_name")]
        [DataType(DataType.Text)]
        [MaxLength(200, ErrorMessage = "First Name must be shorter than 200 characters")]
        [Required(ErrorMessage = "First Name is required.")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Column("employee_last_name")]
        [DataType(DataType.Text)]
        [MaxLength(200, ErrorMessage = "Last Name must be shorter than 200 characters")]
        [Required(ErrorMessage = "Last Name is required.")]
        public String LastName { get; set; }

        [Column("employee_email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid e-mail address.")]
        [MaxLength(500, ErrorMessage = "Email must be shorter than 500 characters")]
        [Required(ErrorMessage = "Email is required.")]
        public String Email { get; set; }

        [Column("employee_phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[(]{0,1}[0-9]{3}[)]{0,1}[-\s\.]{0,1}[0-9]{3}[-\s\.]{0,1}[0-9]{4}$", ErrorMessage = "Invalid phone number.")]
        [Required(ErrorMessage = "Phone is required.")]
        public String Phone { get; set; }

        [Column("employee_state")]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessage = "State must be shorter than 100 characters")]
        [Required(ErrorMessage = "State is required.")]
        public String State { get; set; }

        [Column("employee_city")]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessage = "City must be shorter than 100 characters")]
        [Required(ErrorMessage = "City is required.")]
        public String City { get; set; }

        [Column("employee_street")]
        [DataType(DataType.Text)]
        [MaxLength(100, ErrorMessage = "Street must be shorter than 100 characters")]
        [Required(ErrorMessage = "Street is required.")]
        public String Street { get; set; }

        [Column("employee_zip")]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Invalid Zip.")]
        [Required(ErrorMessage = "Zip is required.")]
        public String Zip { get; set; }
    }
}