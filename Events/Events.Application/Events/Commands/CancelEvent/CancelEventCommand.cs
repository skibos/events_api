using System;
using MediatR;

namespace Events.Application.Events.Commands.CancelEvent;

public record CancelEventCommand(Guid EventId) : IRequest;

