namespace JobTracker.Api.Records;
public record UpdateJobApplicationRequest(
    string CompanyName,
    string Position,
    string Status,
    DateTime DateApplied
);