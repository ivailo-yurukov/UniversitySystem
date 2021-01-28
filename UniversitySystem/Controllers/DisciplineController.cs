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
    public class DisciplineController : Controller
    {
        private readonly ApplicationDbContext context;

        public DisciplineController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: DisciplineController
        public ActionResult Index()
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: DisciplineController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DisciplineViewModel disciplineViewModel)
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
        public ActionResult Edit(int? id)
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
        public async Task<ActionResult> Edit(int id, DisciplineViewModel disciplineViewModel)
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
        public ActionResult Delete(int id)
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
        public async Task<ActionResult> Delete(DisciplineViewModel disciplineViewModel)
        {
            var discipline = new Discipline
            {
                Id = disciplineViewModel.Id,               
            };

            context.Disciplines.Remove(discipline);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
