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
        public ActionResult Create(Semester semester)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SemesterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
