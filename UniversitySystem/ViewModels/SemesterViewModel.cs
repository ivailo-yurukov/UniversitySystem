using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniversitySystem.Models;

namespace UniversitySystem.ViewModels
{
    public class SemesterViewModel
    {
        public int Id { get; set; }

        [DisplayName("Semester")]
        public string SemesterName { get; set; }

        [DisplayName("Discipline")]
        public IEnumerable<string> DisciplineName { get; set; }

        [DisplayName("Professor")]
        public IEnumerable<string> ProfessorName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime EndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Discipline - Professor")]
        public ICollection<Discipline> Disciplines { get; set; }
    }
}
