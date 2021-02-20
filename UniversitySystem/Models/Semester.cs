using AspNetCoreTemplate.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversitySystem.Models
{
    public class Semester : BaseModel<int>
    {
        public Semester()
        {
            this.Disciplines = new HashSet<Discipline>();
            this.StudentSemesters = new HashSet<StudentSemester>();
        }

        [StringLength(50)]
        public string SemesterName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }       

        public virtual ICollection<Discipline> Disciplines { get; set; }

        public virtual ICollection<StudentSemester> StudentSemesters { get; set; }
    }
}
