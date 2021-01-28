using AspNetCoreTemplate.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Models
{
    public class Discipline : BaseModel<int>
    {
        [StringLength(100)]
        public string DisciplineName { get; set; }

        [StringLength(50)]
        public string ProfessorName { get; set; }

        public int? SemesterId { get; set; }

        public virtual Semester Semester { get; set; }
    }
}