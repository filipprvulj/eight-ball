using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EightBall.Data;
using EightBall.Shared.ServiceInterfaces;
using EightBall.Shared.Strings;
using EightBall.MVC.Models;
using AutoMapper;
using EightBall.Shared.Dtos;
using EightBall.MVC.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace EightBall.MVC.Controllers
{
    [Authorize(Roles = RoleNames.EmployeeOrVisitor)]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _service;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var result = await _service.GetEntitiesAsync();
            return View(result.Value);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            var appointment = result.Value;
            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = RoleNames.Employee)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Start,End,Id")] AppointmentModel model)
        {
            var appointmentDto = _mapper.Map<AppointmentDto>(model);
            var result = await _service.InsertAsync(appointmentDto);
            if (!result.Succeeded)
            {
                ModelState.AddModelStateErrors(result.Errors);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = RoleNames.Employee)]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            var appointment = _mapper.Map<AppointmentModel>(result.Value);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Start,End,Id")] AppointmentModel model)
        {
            var appointmentDto = _mapper.Map<AppointmentDto>(model);
            var result = await _service.UpdateAsync(appointmentDto);
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

        // GET: Appointments/Delete/5
        [Authorize(Roles = RoleNames.Employee)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            return View(result.Value);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = RoleNames.Employee)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _service.RemoveAsync(id);
            if (result.Errors.ContainsKey(Errors.NotFound))
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}