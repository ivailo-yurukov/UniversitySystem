using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext context;

        public StudentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: StudentController
        public IActionResult Index()
        {
            var studentViewModel = context.Students
                .OrderBy(x => x.Id)
                .Select(s =>
            new StudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DateOfBirth = s.DateOfBirth,
                Semesters = s.StudentSemesters
                .Where(st => st.StudentId == st.Student.Id)
                .Select(sem => sem.Semester.SemesterName)
                .ToList()
            })
            .ToList();

            return View(studentViewModel);
        }

        // GET: StudentController/Details/5
        public IActionResult Details(int id, StudentDetailsViewModel detailsViewModel)
        {
            var student = context.Students.Find(id);
            var semesters = context.StudentSemesters
                .Where(x => x.StudentId == id)
                .Select(s =>
                new Semester
                {
                    SemesterName = s.Semester.SemesterName,
                    StartDate = s.Semester.StartDate,
                    EndDate = s.Semester.EndDate,
                })
                .ToList();

            detailsViewModel = new StudentDetailsViewModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth.ToString("dd-MM-yyyy"),
                Semesters = semesters,
            };

            return View(detailsViewModel);
        }

        // GET: StudentController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                await context.AddAsync(student);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: StudentController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var student = await context.Students.FindAsync(id);
                return View(student);
            }
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound("There is no such a student!");
            }
            if (ModelState.IsValid)
            {
                context.Update(student);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        public IActionResult AddStudentSemester(int id)
        {
            var student = context.Students.Find(id);
            var semesters = context.Semesters.ToList();
            var studentSemesterModel = new StudentSemesterViewModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Semesters = semesters,
            };

            return View(studentSemesterModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentSemester(string submit, StudentSemesterViewModel viewModel)
        {
            var studentSemester = new StudentSemester
            {
                StudentId = viewModel.Id,
                SemesterId = viewModel.SemesterId,
            };

            if (submit == "Add")
            {              
                try
                {
                    await context.AddAsync(studentSemester);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (viewModel.SemesterId == 0)
                    {
                        return NotFound("Please select a semester!");
                    }
                    var semester = context.Semesters.
                    Where(x => x.Id == viewModel.SemesterId)
                    .Select(s => s.SemesterName)
                    .FirstOrDefault();

                    return NotFound($"This student has {semester}");
                }
            }

            try
            {
                context.Remove(studentSemester);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound("The student doesn`t have such semester!");
            }  
        }

        // GET: StudentController/Delete/5
        public IActionResult Delete(int? id)
        {
            var student = context.Students.Find(id);
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Student student)
        {
            context.Remove(student);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
