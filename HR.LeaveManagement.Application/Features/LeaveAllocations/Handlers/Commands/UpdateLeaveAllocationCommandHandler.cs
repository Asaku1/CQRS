using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    //
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        //Constructor
        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            //"UpdateLeaveAllocationCommand" is an object, UpdateLeaveAllocationCommand = {LeaveAllocationDto}
            //request: {LeaveAllocationDto}; and LeaveAllocationDto = {Id: {Name, DefaultDays}}
            //Therefore, request.LeaveAllocationDto.Id returns {NumberOfDays, LeaveTypeDto, LeaveTypeId, Period}
            var leaveAllocation = await _leaveAllocationRepository.Get(request.LeaveAllocationDto.Id); // leaveType = {NumberOfDays, LeaveTypeDto, LeaveTypeId, Period}

            _mapper.Map(request.LeaveAllocationDto, leaveAllocation); //update "leaveAllocation" with "leaveAllocationDto"

            await _leaveAllocationRepository.Update(leaveAllocation); //use Update method from "IGenericRepository" to update "leaveAllocation"

            return Unit.Value;
        }
    }
}
