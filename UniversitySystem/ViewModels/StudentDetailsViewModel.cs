using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversitySystem.Models;

namespace UniversitySystem.ViewModels
{
    public class StudentDetailsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public ICollection<Semester> Semesters { get; set; }

        public IEnumerable<Score> Scores { get; set; }
    }
}
