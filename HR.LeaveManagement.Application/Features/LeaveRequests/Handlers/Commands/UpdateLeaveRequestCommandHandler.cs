using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    //"UpdateLeaveRequestCommandHandler" takes as input "UpdateLeaveRequestCommand & Unit"
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        //Constructor
        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.Get(request.Id);

            if(request.LeaveRequestDto != null)
            {
                //"UpdateLeaveRequestCommand" is an object, UpdateLeaveRequestCommand = {LeaveRequestDto}
                //request: {LeaveRequestDto}; and LeaveRequestDto = {Id: {StartDate, ..., Cancelled}}
                //Therefore, request.LeaveRequestDto.Id returns {StartDate, ..., Cancelled}
                ///var leaveRequest = await _leaveRequestRepository.Get(request.LeaveRequestDto.Id); // leaveRequest = {NumberOfDays, LeaveTypeDto, LeaveTypeId, Period}

                _mapper.Map(request.LeaveRequestDto, leaveRequest); //update "leaveRequest" with "leaveRequestDto"

                await _leaveRequestRepository.Update(leaveRequest); //use Update method from "IGenericRepository" to update "leaveRequest"
            }
            else if(request.ChangeLeaveRequestApprovalDto != null)
            {
                ///var leaveRequest = await _leaveRequestRepository.Get(request.ChangeLeaveRequestApprovalDto.Id);

                await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
            }

            return Unit.Value;
        }
    }
}
