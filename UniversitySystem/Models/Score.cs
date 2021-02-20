using AspNetCoreTemplate.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace UniversitySystem.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        public int ScoreNumber { get; set; }

        public int? DisciplineId { get; set; }

        public virtual Discipline Discipline { get; set; }
    }
}