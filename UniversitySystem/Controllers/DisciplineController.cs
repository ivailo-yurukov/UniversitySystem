using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{
    public class DisciplineController : Controller
    {
        private readonly ApplicationDbContext context;

        public DisciplineController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: DisciplineController
        public IActionResult Index()
        {
            var disciplneViewModel = context.Disciplines
                .OrderBy(x => x.DisciplineName)
                .Select(x => new DisciplineViewModel
                {
                    Id = x.Id,
                    DisciplineName = x.DisciplineName,
                    ProfessorName = x.ProfessorName,
                    Semester = x.Semester,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn,
                })
                .ToList();

            return View(disciplneViewModel);
        }

        // GET: DisciplineController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisciplineController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DisciplineViewModel disciplineViewModel)
        {
            if (ModelState.IsValid)
            {
                var discipline = new Discipline
                {
                    DisciplineName = disciplineViewModel.DisciplineName,
                    ProfessorName = disciplineViewModel.ProfessorName,
                };

                await context.AddAsync(discipline);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disciplineViewModel);
        }

        // GET: DisciplineController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var discipline = context.Disciplines.Find(id);
                var disciplineViewModel = new DisciplineViewModel
                {
                    Id = discipline.Id,
                    DisciplineName = discipline.DisciplineName,
                    ProfessorName = discipline.ProfessorName,
                    CreatedOn = discipline.CreatedOn,
                };
                return View(disciplineViewModel);
            }
        }

        // POST: DisciplineController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DisciplineViewModel disciplineViewModel)
        {
            if (id != disciplineViewModel.Id)
            {
                return NotFound("There is no such a discipline!");
            }
            if (ModelState.IsValid)
            {
                var discipline = new Discipline
                {
                    Id = disciplineViewModel.Id,
                    DisciplineName = disciplineViewModel.DisciplineName,
                    ProfessorName = disciplineViewModel.ProfessorName,
                    CreatedOn = disciplineViewModel.CreatedOn,
                };
                context.Update(discipline);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disciplineViewModel);
        }

        // GET: DisciplineController/Delete/5
        public IActionResult Delete(int id)
        {
            var discipline = context.Disciplines.Find(id);
            var disciplineViewModel = new DisciplineViewModel
            {
                DisciplineName = discipline.DisciplineName,
                ProfessorName = discipline.ProfessorName,
                CreatedOn = discipline.CreatedOn,
            };
            return View(disciplineViewModel);
        }

        // POST: DisciplineController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DisciplineViewModel disciplineViewModel)
        {
            var discipline = new Discipline
            {
                Id = disciplineViewModel.Id,
            };

            context.Disciplines.Remove(discipline);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddSemester(int id)
        {
            var discipline = context.Disciplines.Find(id);
            var semesters = context.Semesters
                .Select(x => new SelectListItem(x.SemesterName, x.Id.ToString()))
                .ToList();

            var disciplineViewModel = new DisciplineViewModel
            {
                DisciplineName = discipline.DisciplineName,
                ProfessorName = discipline.ProfessorName,
                SelectListItems = new List<SelectListItem>(semesters),
            };

            return View(disciplineViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddSemester(string submit, DisciplineViewModel disciplineViewModel)
        {
            var discipline = new Discipline
            {
                SemesterId = disciplineViewModel.SemesterId,
            };
            if (submit == "Add")
            {
                context.Update(discipline);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
