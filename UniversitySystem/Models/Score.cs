using AspNetCoreTemplate.Data.Common.Models;

namespace UniversitySystem.Models
{
    public class Score : BaseModel<int>
    {
        public string DisiplineName { get; set; }

        public string ProfessorName { get; set; }

        public int ScoreNumber { get; set; }

        public int? SemesterId { get; set; }

        public virtual Semester Semester { get; set; }
    }
}