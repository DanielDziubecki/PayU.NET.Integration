using System.ComponentModel.DataAnnotations;

namespace AuthorizationService
{
    public class AudienceDto
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}