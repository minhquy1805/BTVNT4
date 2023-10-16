using System.ComponentModel.DataAnnotations;

namespace BTVNT4.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Student Name is required")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Region is required")]
        public string Region { get; set;}

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        public double Telephone { get; set; }

    }
}
