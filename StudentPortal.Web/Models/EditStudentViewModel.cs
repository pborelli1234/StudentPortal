using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models
{
    public class EditStudentViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required!")]
        public string Phone { get; set; }
        public bool Subscribed { get; set; }
    }
}
