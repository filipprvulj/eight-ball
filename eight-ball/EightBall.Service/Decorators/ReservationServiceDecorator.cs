using EightBall.Service.Hubs;
using EightBall.Shared;
using EightBall.Shared.Dtos;
using EightBall.Shared.RepositoryInterfaces;
using EightBall.Shared.ServiceInterfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBall.Service.Decorators
{
    public class ReservationServiceDecorator : AbstractReservationDecorator
    {
        private readonly IReservationService _reservationService;
        private readonly IHubContext<ReservationHub> _hub;
        private readonly IUserRepository _userRepository;

        public ReservationServiceDecorator(IReservationService reservationService, IHubContext<ReservationHub> hub, IUserRepository userRepository) : base(reservationService)
        {
            _reservationService = reservationService;
            _hub = hub;
            _userRepository = userRepository;
        }

        public override async Task<Result<Guid>> InsertAsync(ReservationDto dto)
        {
            Result<Guid> result = await base.InsertAsync(dto);
            if (result.Succeeded)
            {
                var insertedReservationResult = await _reservationService.GetByIdAsync(result.Value);
                if (!insertedReservationResult.Succeeded)
                {
                    result.Errors = insertedReservationResult.Errors;
                    return result;
                }

                var employees = await _userRepository.GetEmployeeIdsAsync();
                await _hub.Clients.Users(employees).SendAsync("InsertedReservation", insertedReservationResult.Value);
            }

            return result;
        }

        public override async Task<Result> RemoveAsync(Guid id, Guid currentUserId)
        {
            Result result = await base.RemoveAsync(id, currentUserId);
            if (result.Succeeded)
            {
                var emloyees = await _userRepository.GetEmployeeIdsAsync();
                await _hub.Clients.Users(emloyees).SendAsync("RemoveReservation", id);
            }

            return result;
        }
    }
}