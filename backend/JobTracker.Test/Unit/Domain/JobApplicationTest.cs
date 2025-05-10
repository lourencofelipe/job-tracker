using JobTracker.Domain.Entities;

namespace JobTracker.Test.Unit.Domain;
public class JobApplicationTest
{
    [Fact]
    [Description("It should fail when trying to set the same status")]
    public void ChangeStatus_ToSameStatus_ShouldFail()
    {
        // Arrange
        var job = new JobApplication
        {
            CompanyName = "Company T",
            Position = "Engineer",
            DateApplied = DateTime.UtcNow
        };

        // Act
        var result = job.ChangeStatus(ApplicationStatus.Applied, out var error);

        // Assert
        result.Should().BeFalse();
        error.Should().Be("New status is the same as the current status.");
    }

    [Fact]
    [Description("It Should fail when trying to change the status from Applied to Offer.")]
    public void ChangeStatus_FromAppliedToOffer_ShouldFail()
    {
        // Arrange
        var job = new JobApplication
        {
            CompanyName = "Company T",
            Position = "Engineer",
            DateApplied = DateTime.UtcNow
        };

        // Act
        var result = job.ChangeStatus(ApplicationStatus.Offer, out var error);

        // Assert
        result.Should().BeFalse();
        error.Should().Be("Cannot change status to Applied.");
    }

    [Fact]
    [Description("It Should fail when trying to change the status from Interview to Applied.")]
    public void ChangeStatus_FromInterviewToApplied_ShouldFail()
    {
        // Arrange
        var job = new JobApplication
        {
            CompanyName = "Company T",
            Position = "Engineer",
            DateApplied = DateTime.UtcNow
        };

        // Act
        job.ChangeStatus(ApplicationStatus.Interview, out _);

        var result = job.ChangeStatus(ApplicationStatus.Applied, out var error);

        // Assert
        result.Should().BeFalse();
        error.Should().Be("Cannot revert status from Interview back to Applied.");
    }

    [Fact]
    [Description("It Should fail when trying to change the status after Rejected.")]
    public void ChangeStatus_WhenRejected_ShouldFail()
    {
        // Arrange
        var job = new JobApplication
        {
            CompanyName = "Company T",
            Position = "Engineer",
            DateApplied = DateTime.UtcNow
        };

        // Act
        job.ChangeStatus(ApplicationStatus.Rejected, out _);

        var result = job.ChangeStatus(ApplicationStatus.Offer, out var error);

        // Assert
        result.Should().BeFalse();
        error.Should().Be("Cannot change application status after rejected.");
    }

    [Fact]
    [Description("It Should fail when trying to change the status from Offer to Interview.")]
    public void ChangeStatus_FromOfferToInterview_ShouldFail()
    {
        // Arrange
        var job = new JobApplication
        {
            CompanyName = "Company T",
            Position = "Engineer",
            DateApplied = DateTime.UtcNow
        };

        // Act
        job.ChangeStatus(ApplicationStatus.Interview, out _);
        job.ChangeStatus(ApplicationStatus.Offer, out _);

        var result = job.ChangeStatus(ApplicationStatus.Interview, out var error);

        // Assert
        result.Should().BeFalse();
        error.Should().Be("Cannot change status from Offer to another status.");
    }

    [Fact]
    [Description("Successful when changing to Interview.")]
    public void ChangeStatus_ValidTransition_ShouldSucceed()
    {
        // Arrange
        var job = new JobApplication
        {
            CompanyName = "Company T",
            Position = "Engineer",
            DateApplied = DateTime.UtcNow
        };

        // Act
        var result = job.ChangeStatus(ApplicationStatus.Interview, out var error);

        // Assert
        result.Should().BeTrue();
        error.Should().BeNull();
        job.Status.Should().Be(ApplicationStatus.Interview);
    }
}
