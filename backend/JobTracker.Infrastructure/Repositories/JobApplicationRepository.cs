namespace JobTracker.Infrastructure.Repositories;
public class JobApplicationRepository : IJobApplicationRepository
{
    private readonly EfContext _context;
 
    public JobApplicationRepository(EfContext context) => _context = context;

    public async Task<IEnumerable<JobApplication>> GetAll() => 
        await _context.JobApplications.OrderByDescending(x => x.DateApplied).ToListAsync();

    public async Task<JobApplication?> GetById(Guid id) 
    {
        var jobApplication = await _context.JobApplications
                                      .FirstOrDefaultAsync(x => x.Id == id);

        return jobApplication;
    }
    
    public async Task<JobApplication> Create(JobApplication jobApplication)
    {
        _context.JobApplications.Add(jobApplication);
        await _context.SaveChangesAsync();
      
        return jobApplication;
    }

    public async Task<JobApplication> Update(JobApplication jobApplication)
    {
        _context.JobApplications.Update(jobApplication);
        await _context.SaveChangesAsync();
       
        return jobApplication;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _context.JobApplications.FindAsync(id);
        if (entity is null) return false;

        _context.JobApplications.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
