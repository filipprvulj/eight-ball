using EightBall.Shared;
using EightBall.Shared.Dtos;
using EightBall.Shared.ServiceInterfaces;
using EightBall.Shared.Strings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EightBall.MVC.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IAppointmentService _appointmentService;

        public ReservationsController(IReservationService reservationService, IAppointmentService appointmentService)
        {
            _reservationService = reservationService;
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            List<ReservationDto> reservations = new List<ReservationDto>();
            Result<List<ReservationDto>> reservationResult;
            if (User.IsInRole(RoleNames.Employee))
            {
                reservationResult = await _reservationService.GetEntitiesAsync();
                if (reservationResult.Succeeded)
                {
                    reservations.AddRange(reservationResult.Value);
                }
                else
                {
                    return BadRequest(reservationResult.Errors);
                }
            }
            else
            {
                Guid userId = GetCurrentUserId();
                reservationResult = await _reservationService.GetReservationsByUserIdAsync(userId);
                if (reservationResult.Succeeded)
                {
                    reservations.AddRange(reservationResult.Value);
                }
                else
                {
                    return BadRequest(reservationResult.Errors);
                }
            }

            return View(reservations);
        }

        public async Task<IActionResult> Create(Guid id, Guid tableId)
        {
            var appointmentResult = await _appointmentService.GetByIdAsync(id);
            if (!appointmentResult.Succeeded)
            {
                if (appointmentResult.Errors.ContainsKey(Errors.NotFound))
                {
                    return NotFound();
                }

                return BadRequest(appointmentResult.Errors);
            }

            Guid userId = GetCurrentUserId();
            ReservationDto reservationDto = new ReservationDto()
            {
                AppointmentId = id,
                Appointment = appointmentResult.Value,
                TableId = tableId,
                Table = appointmentResult.Value.Tables.FirstOrDefault(t => t.Id == tableId),
                UserId = userId
            };

            return View(reservationDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId, TableId, UserId")] ReservationDto reservation)
        {
            var reservationResult = await _reservationService.InsertAsync(reservation);
            if (!reservationResult.Succeeded)
            {
                return BadRequest(reservationResult.Errors);
            }

            return RedirectToAction(nameof(Index));
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}