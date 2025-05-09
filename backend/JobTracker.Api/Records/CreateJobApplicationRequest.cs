namespace JobTracker.Api.Records;
public record CreateJobApplicationRequest(
    string CompanyName,
    string Position,
    string Status,
    DateTime DateApplied
);

