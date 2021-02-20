using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniversitySystem.Models;

namespace UniversitySystem.ViewModels
{
    public class DisciplineViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DisciplineName { get; set; }

        [Required]
        [StringLength(50)]
        public string ProfessorName { get; set; }

        public Semester Semester { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime CreatedOn { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? ModifiedOn { get; set; }

        public int SemesterId { get; set; }

        public IEnumerable<SelectListItem> SelectListItems { get; set; }
    }
}
