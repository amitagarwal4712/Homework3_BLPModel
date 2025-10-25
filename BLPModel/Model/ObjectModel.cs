using System.ComponentModel.DataAnnotations;

namespace BLPModel.Model
{
    public class ObjectModel
    {
        [Required]
        public string? Oid { get; set; }
        
        [Required]
        public SecurityLevelEnum Level { get; set; }
    }
}