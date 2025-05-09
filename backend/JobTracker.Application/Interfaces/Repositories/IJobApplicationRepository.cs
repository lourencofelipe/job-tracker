namespace JobTracker.Application.Interfaces.Repositories;
public interface IJobApplicationRepository
{
    Task<IEnumerable<JobApplication>> GetAll();
    Task<JobApplication?> GetById(Guid id);
    Task<JobApplication> Create(JobApplication jobApplication);
    Task<JobApplication> Update(JobApplication jobApplication);
    Task<bool> Delete(Guid id);
}
