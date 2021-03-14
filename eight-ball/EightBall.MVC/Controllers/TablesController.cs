using AutoMapper;
using EightBall.MVC.Extensions;
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
            var result = await _tableService.GetEntitiesAsync();
            var model = _mapper.Map<List<TableModel>>(result.Value);

            return View(model);
        }

        // GET: TablesController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _tableService.GetByIdAsync(id);

            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            var table = result.Value;
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
            var table = _mapper.Map<TableDto>(tableModel);
            var result = await _tableService.InsertAsync(table);

            if (!result.Succeeded)
            {
                ModelState.AddModelStateErrors(result.Errors);

                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: TablesController/Edit/5
        [Authorize(Roles = RoleNames.Employee)]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _tableService.GetByIdAsync(id);

            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            var table = result.Value;
            TableModel model = _mapper.Map<TableModel>(table);
            return View(model);
        }

        // POST: TablesController/Edit/5
        [HttpPost]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] TableModel model)
        {
            TableDto table = _mapper.Map<TableDto>(model);
            var result = await _tableService.UpdateAsync(table);
            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelStateErrors(result.Errors);
                return View(model);
            }
        }

        // GET: TablesController/Delete/5
        [Authorize(Roles = RoleNames.Employee)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tableService.GetByIdAsync(id);
            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            var table = result.Value;
            return View(table);
        }

        // POST: TablesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _tableService.RemoveAsync(id);
            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}