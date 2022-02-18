namespace SchoolAssistant.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using SchoolAssistant.Data;
    using SchoolAssistant.Data.Common.Repositories;
    using SchoolAssistant.Data.Models;
    using SchoolAssistant.Services.Data;

    [Area("Administration")]
    public class DepartmentsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Department> dataRepository;
        private readonly IDepartmentsService departmentsService;

        public DepartmentsController(IDeletableEntityRepository<Department> dataRepository, IDepartmentsService departmentsService)
        {
            this.dataRepository = dataRepository;
            this.departmentsService = departmentsService;
        }

        // GET: Administration/Departments
        public IActionResult Index()
        {
            return this.View(this.departmentsService.GetAll());
        }

        // GET: Administration/Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var department = await this.departmentsService.GetById(id);

            return this.View(department);
        }

        // GET: Administration/Departments/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Department department)
        {
            if (this.ModelState.IsValid)
            {
                await this.departmentsService.CreateAsync(department);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(department);
        }

        // GET: Administration/Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var department = await this.dataRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);

            if (department == null)
            {
                return this.NotFound();
            }

            return this.View(department);
        }

        // POST: Administration/Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Department department)
        {
            if (id != department.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(department);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.DepartmentExists(department.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(department);
        }

        // GET: Administration/Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var department = await this.dataRepository.AllWithDeleted()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return this.NotFound();
            }

            return this.View(department);
        }

        // POST: Administration/Departments/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await this.dataRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            this.dataRepository.Delete(department);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool DepartmentExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
