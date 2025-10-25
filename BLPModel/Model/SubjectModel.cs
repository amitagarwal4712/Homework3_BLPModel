using System.ComponentModel.DataAnnotations;

namespace BLPModel.Model
{
    public class SubjectModel
    {
        [Required]
        public string? Pid { get; set; }

        [Required]
        public SecurityLevelEnum Max_Level { get; set; }

        [Required]
        public SecurityLevelEnum Start_Level { get; set; }
    }
}
