namespace JobTracker.Application.Interfaces.Services;
public interface IJobApplicationService
{
    Task<ApplicationResult<IEnumerable<JobApplicationRecord>>> GetAll();
    Task<ApplicationResult<JobApplicationRecord>> GetById(Guid id);
    Task<ApplicationResult<JobApplicationRecord>> Create(CreateJobApplicationRecord jobApplication);
    Task<ApplicationResult<JobApplicationRecord>> Update(Guid id, UpdateJobApplicationRecord jobApplication);
    Task<ApplicationResult<bool>> Delete(Guid id);
}

