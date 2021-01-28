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
            var students = context.Students
                .OrderBy(x => x.Id)
                .Select(s =>
            new Student
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DateOfBirth = s.DateOfBirth,
                Semesters = s.Semesters
                .Where(x => x.StudentId == s.Id)
                .ToList()
            })
            .ToList();
            return View(students);
        }

        // GET: StudentController/Details/5
        public IActionResult Details(int id, StudentDetailsViewModel detailsViewModel)
        {
            var student = context.Students.Find(id);
            var semesters = context.Semesters
                .Where(x => x.StudentId == id)
                .Select(x =>
                new Semester
                {
                    SemesterName = x.SemesterName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,

                }).ToList();

            var scores = context.Semesters
                .Where(x => x.StudentId == id)
                .Select(x => x.Scores
                               .Select(x => new Score
                               {
                                   DisiplineName = x.DisiplineName,
                                   ProfessorName = x.ProfessorName,
                                   ScoreNumber = x.ScoreNumber,
                               })).FirstOrDefault();
            
            detailsViewModel.FirstName = student.FirstName;
            detailsViewModel.LastName = student.LastName;
            detailsViewModel.DateOfBirth = student.DateOfBirth.ToString("dd-MM-yyyy");
            detailsViewModel.Semesters = semesters;
            detailsViewModel.Scores = scores ?? new List<Score>();

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
