using AutoMapper;
using EightBall.MVC.Extensions;
using EightBall.MVC.Models;
using EightBall.Shared.Dtos;
using EightBall.Shared.ServiceInterfaces;
using EightBall.Shared.Strings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IAppointmentService _appointmentService;

        public TablesController(ITableService tableService, IMapper mapper, IAppointmentService appointmentService)
        {
            _tableService = tableService;
            _appointmentService = appointmentService;
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

        [Authorize(Roles = RoleNames.Employee)]
        public async Task<IActionResult> AddTableAppointment(Guid id)
        {
            bool tableExists = await _tableService.EntityExists(id);
            if (!tableExists)
            {
                return NotFound();
            }
            else
            {
                var appointmentResult = await _appointmentService.GetEntitiesAsync();
                if (appointmentResult.Succeeded)
                {
                    List<SelectListItem> selectList = appointmentResult.Value.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = string.Join(" - ", a.Start, a.End)
                    }).ToList();
                    TableAppointmentViewModel viewModel = new TableAppointmentViewModel();
                    viewModel.TableId = id;
                    viewModel.Appointments.AddRange(selectList);

                    return View(viewModel);
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTableAppointment([Bind("TableId, AppointmentId")] TableAppointmentViewModel viewModel)
        {
            var tableAppointmentResult = await _tableService.AddTableAppointmentAsync(viewModel.TableId, viewModel.AppointmentId);
            if (tableAppointmentResult.Succeeded)
            {
                return RedirectToAction(nameof(Details), new { id = viewModel.TableId });
            }
            else
            {
                var appointmentResult = await _appointmentService.GetEntitiesAsync();
                if (appointmentResult.Succeeded)
                {
                    List<SelectListItem> selectList = appointmentResult.Value.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = string.Join(" - ", a.Start, a.End)
                    }).ToList();

                    viewModel.Appointments.AddRange(selectList);
                }

                ModelState.AddModelStateErrors(tableAppointmentResult.Errors);
                return View(viewModel);
            }
        }

        [Authorize(Roles = RoleNames.Employee)]
        public async Task<IActionResult> RemoveTableAppointment(Guid id, Guid appointmentId)
        {
            var tableResutlt = await _tableService.GetByIdAsync(id);
            if (!tableResutlt.Succeeded)
            {
                if (tableResutlt.Errors.ContainsKey(Errors.NotFound))
                {
                    return NotFound();
                }

                return BadRequest(tableResutlt.Errors);
            }

            var appointmentResult = await _appointmentService.GetByIdAsync(appointmentId);
            if (!appointmentResult.Succeeded)
            {
                if (appointmentResult.Errors.ContainsKey(Errors.NotFound))
                {
                    return NotFound();
                }

                return BadRequest(appointmentResult.Errors);
            }

            RemoveTableAppointmentViewModel viewModel = new RemoveTableAppointmentViewModel
            {
                Table = tableResutlt.Value,
                Appointment = appointmentResult.Value
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveTableAppointment(RemoveTableAppointmentViewModel viewModel)
        {
            var tableAppointmentResult = await _tableService.RemoveTableAppointmentAsync(viewModel.Table.Id, viewModel.Appointment.Id);
            if (tableAppointmentResult.Succeeded)
            {
                return RedirectToAction(nameof(Details), new { id = viewModel.Table.Id });
            }
            else
            {
                if (tableAppointmentResult.Errors.ContainsKey(Errors.NotFound))
                {
                    return NotFound(tableAppointmentResult.Errors);
                }
                return BadRequest(tableAppointmentResult.Errors);
            }
        }
    }
}