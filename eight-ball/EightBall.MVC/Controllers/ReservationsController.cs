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

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
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
                Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
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
    }
}