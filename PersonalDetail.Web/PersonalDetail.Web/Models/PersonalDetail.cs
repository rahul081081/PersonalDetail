using System.ComponentModel.DataAnnotations;

namespace PersonalDetail.Web.Models
{
    public class PersonalDetail
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

    }
}
