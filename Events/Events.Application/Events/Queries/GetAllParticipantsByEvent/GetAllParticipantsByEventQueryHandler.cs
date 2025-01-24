using System;
using Events.Application.Common.Dto;
using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Events.Dto;
using Events.Domain.Events;
using Events.Domain.Events.Entities;
using Events.Domain.Events.Exceptions;
using Events.Domain.Users;
using Events.Domain.Users.ValueObjects;
using MediatR;

namespace Events.Application.Events.Queries.GetAllParticipantsByEvent
{
	public class GetAllParticipantsByEventQueryHandler : IRequestHandler<GetAllParticipantsByEventQuery, PaginatedResult<ParticipantResult>>
	{
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public GetAllParticipantsByEventQueryHandler(IEventRepository eventRepository, IUserRepository userRepository)
		{
            _eventRepository = eventRepository;
            _userRepository = userRepository;
		}

        public async Task<PaginatedResult<ParticipantResult>> Handle(GetAllParticipantsByEventQuery query, CancellationToken cancellationToken)
        {
            PaginatedResult<Participant> participants = await _eventRepository.GetPagedParticipants(query.EventId, query.PageNumber, query.PageSize);

            List<Guid> participantsIds = participants.Items.Select(e => e.UserId.Value).ToList();

            List<User> participantsData = await _userRepository.GetUsersByIds(participantsIds);

            List<ParticipantResult> participantsResultList = participants.Items.Select(e => new ParticipantResult(
                query.EventId,
                e.UserId.Value,
                participantsData.FirstOrDefault(u => u.Id.Value == e.UserId.Value)?.FirstName ?? "",
                participantsData.FirstOrDefault(u => u.Id.Value == e.UserId.Value)?.LastName ?? "",
                participantsData.FirstOrDefault(u => u.Id.Value == e.UserId.Value)?.Email ?? "",
                e.Role.ToString(),
                e.AttendanceStatus.ToString()
            )).ToList();

            PaginatedResult<ParticipantResult> result = new(
                participantsResultList,
                participants.Empty,
                participants.CurrentPage,
                participants.ResultsPerPage,
                participants.TotalPages,
                participants.TotalResults
            );

            return result;
        }
    }
}

