namespace JobTracker.Application.Records;
public record UpdateJobApplicationRecord(
    string CompanyName,
    string Position,
    string Status,
    DateTime DateApplied
);