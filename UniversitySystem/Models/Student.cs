﻿using AspNetCoreTemplate.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversitySystem.Models
{
    public class Student : BaseModel<int>
    {
        public Student()
        {
            this.StudentSemesters = new HashSet<StudentSemester>();
        }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<StudentSemester>  StudentSemesters { get; set; }
    }
}
