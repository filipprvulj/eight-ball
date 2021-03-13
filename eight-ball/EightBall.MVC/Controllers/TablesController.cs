using AutoMapper;
using EightBall.MVC.Models;
using EightBall.Shared.Dtos;
using EightBall.Shared.ServiceInterfaces;
using EightBall.Shared.Strings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.MVC.Controllers
{
    [Authorize(Roles = RoleNames.EmployeeOrVisitor)]
    public class TablesController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public TablesController(ITableService tableService, IMapper mapper)
        {
            _tableService = tableService;
            _mapper = mapper;
        }

        // GET: TablesController
        public async Task<IActionResult> Index()
        {
            var tables = await _tableService.GetEntitiesAsync();
            var model = _mapper.Map<List<TableModel>>(tables);

            return View(model);
        }

        // GET: TablesController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var table = await _tableService.GetByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // GET: TablesController/Create
        [Authorize(Roles = RoleNames.Employee)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TablesController/Create
        [HttpPost]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] TableModel tableModel)
        {
            if (ModelState.IsValid)
            {
                var table = _mapper.Map<TableDto>(tableModel);
                await _tableService.InsertAsync(table);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: TablesController/Edit/5
        [Authorize(Roles = RoleNames.Employee)]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var table = await _tableService.GetByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            TableModel model = _mapper.Map<TableModel>(table);
            return View(model);
        }

        // POST: TablesController/Edit/5
        [HttpPost]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] TableModel model)
        {
            if (ModelState.IsValid)
            {
                bool entityExists = await _tableService.EntityExists(model.Id);
                if (!entityExists)
                {
                    return NotFound();
                }

                TableDto table = _mapper.Map<TableDto>(model);
                await _tableService.UpdateAsync(table);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: TablesController/Delete/5
        [Authorize(Roles = RoleNames.Employee)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            bool entityExists = await _tableService.EntityExists(id);
            if (!entityExists)
            {
                return NotFound();
            }

            var table = await _tableService.GetByIdAsync(id);
            return View(table);
        }

        // POST: TablesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _tableService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}