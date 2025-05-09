using JobTracker.Domain.Enums;

namespace JobTracker.Domain.Entities;
public class JobApplication
{
    public Guid Id { get; set; }
    public required string CompanyName { get; set; }
    public required string Position { get; set; }
    public ApplicationStatus Status { get; private set; } = ApplicationStatus.Applied;
    public DateTime DateApplied { get; set; }

    public bool ChangeStatus(ApplicationStatus newStatus, out string? error) 
    {
        error = null;

        if (newStatus.Equals(Status))
        {
            error = "New status is the same as the current status.";
            return false;
        }

        if (Status.Equals(ApplicationStatus.Applied) && newStatus.Equals(ApplicationStatus.Offer))
        {
            error = "Cannot change status to Applied.";
            return false;
        }

        if (Status.Equals(ApplicationStatus.Interview) && newStatus.Equals(ApplicationStatus.Applied))
        {
            error = "Cannot revert status from Interview back to Applied.";
            return false;
        }

        if (Status.Equals(ApplicationStatus.Rejected)) 
        {
            error = "Cannot change application status after rejected.";
            return false;
        }

        if (Status.Equals(ApplicationStatus.Offer) && newStatus != ApplicationStatus.Offer)
        {
            error = "Cannot change status from Offer to another status.";
            return false;
        }

        Status = newStatus;
        return true;

    }
}
