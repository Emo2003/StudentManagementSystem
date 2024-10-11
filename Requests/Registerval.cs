using System.ComponentModel.DataAnnotations;
using System.Data;

namespace StudentProject.Requests
{
    public class Registerval
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        public string Address { get; set; }
        [Required]
        [MinLength(11)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password Must Be At Least 6 Letters")]
        public string Password { get; set; }
        [Required]
        public bool Is_Admin { get; set; }
    }
}