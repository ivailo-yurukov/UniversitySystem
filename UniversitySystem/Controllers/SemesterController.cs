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
    public class SemesterController : Controller
    {
        private readonly ApplicationDbContext context;

        public SemesterController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: SemesterController
        public ActionResult Index()
        {
            //    var semesterViewModel = semesters
            //        .Join(disciplines, x => x.Id, y => y.SemesterId, (sem, dis) =>
            //     new SemesterViewModel
            //     {
            //         SemesterName = sem.SemesterName,
            //         DisciplineName = dis.DisciplineName,
            //         ProfessorName = dis.ProfessorName,
            //         CreatedOn = sem.CreatedOn,
            //         StartDate = sem.StartDate,
            //         EndDate = sem.EndDate,
            //         Disciplines = sem.Disciplines,
            //     })
            var semesterViewModel = context.Semesters
               .Select(x =>
               new SemesterViewModel
               {
                   Id = x.Id,
                   SemesterName = x.SemesterName,
                   StartDate = x.StartDate,
                   EndDate = x.EndDate,
                   CreatedOn = x.CreatedOn,
                   Disciplines = x.Disciplines,
                   DisciplineName = x.Disciplines.Select(x => x.DisciplineName),
                   ProfessorName = x.Disciplines.Select(x => x.ProfessorName),

               })
               .ToList()
               .GroupBy(x => x.SemesterName)
               .Select(x => x.First())
               .ToList();

            return View(semesterViewModel);
        }

        // GET: SemesterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SemesterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Semester semester)
        {
            if (ModelState.IsValid)
            {
                await context.AddAsync(semester);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semester);
        }

        // GET: SemesterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SemesterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SemesterController/Delete/5
        public IActionResult Delete(int id)
        {
            var semester = context.Semesters.Find(id);
            var semesterViewModel = new SemesterViewModel
            {
                SemesterName = semester.SemesterName,
                StartDate = semester.StartDate,
                EndDate = semester.EndDate,
            };

            return View(semesterViewModel);
        }

        // POST: SemesterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SemesterViewModel semesterViewModel)
        {
            var isSemesterConnectToStudent = context.StudentSemesters
                .Any(x => x.SemesterId == semesterViewModel.Id);

            if (!isSemesterConnectToStudent)
            {
                var semester = context.Semesters.Find(semesterViewModel.Id);
                context.Remove(semester);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound("The semester is connected with student(s)");
        }
    }
}
