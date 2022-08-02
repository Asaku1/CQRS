using AutoMapper;
using HR.LeaveManagement.Application.DTOs;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests;
using HR.LeaveManagement.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    //IRequestHandler<> takes in the specific Request. See Requests folder.
    //"GetLeaveTypeListRequest.cs" has data type LeaveTypeDto and presents it in list format.
                                                  //IRequestHandler<Requests, dataType>
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        //the method Handle implements the "Requests" GetLeaveTypeListRequest.cs
                     //Task<dataType>
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeRepository.GetAll(); //gets all data from db

            return _mapper.Map<List<LeaveTypeDto>>(leaveTypes); //converts "leaveTypes" to "LeaveTypeDto"
        }
    }
}
