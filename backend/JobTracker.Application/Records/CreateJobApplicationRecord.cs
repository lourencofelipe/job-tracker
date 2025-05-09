namespace JobTracker.Application.Records;
public record CreateJobApplicationRecord(
    string CompanyName,
    string Position,
    string Status,
    DateTime DateApplied
);
