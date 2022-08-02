using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    //"UpdateLeaveTypeCommandHandler" inherits "IRequestHandler" and takes as inputs "UpdateLeaveTypeCommand & Unit"
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        //Constructor
        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        //returns "Unit", look at Task<Unit>
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            //"UpdateLeaveTypeCommand" is an object, UpdateLeaveTypeCommand = {LeaveTypeDto}
            //request: {LeaveTypeDto}; and LeaveTypeDto = {Id: {Name, DefaultDays}}
            //Therefore, request.LeaveTypeDto.Id returns {Name, DefaultDays}
            var leaveType = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id); // leaveType = {Name, DefaultDays}

            _mapper.Map(request.LeaveTypeDto, leaveType); //update "leaveType" with "leaveTypeDto"

            await _leaveTypeRepository.Update(leaveType); //use Update method from "IGenericRepository" to update "leaveType"

            return Unit.Value;
        }
    }
}
