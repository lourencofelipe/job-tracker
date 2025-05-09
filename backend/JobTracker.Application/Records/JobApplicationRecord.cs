namespace JobTracker.Application.Records;
public record JobApplicationRecord(
    Guid Id,
    string CompanyName,
    string Position,
    string Status,
    DateTime DateApplied
);