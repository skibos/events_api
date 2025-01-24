namespace Events.API.Dto.Common;

public record PaginatedRequest(int PageNumber = 1, int PageSize = 10);
