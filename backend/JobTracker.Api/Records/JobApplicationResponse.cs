namespace JobTracker.Api.Records;
public record JobApplicationResponse(
    Guid Id,
    string CompanyName,
    string Position,
    string Status,
    DateTime DateApplied
);


