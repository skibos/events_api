namespace Events.Application.Common.Dto;

public record PaginatedResult<T>(
    List<T> Items,
    bool Empty,
    int CurrentPage,
    int ResultsPerPage,
    int TotalPages,
    int TotalResults
);
