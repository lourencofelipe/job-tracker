namespace JobTracker.Application.Services;
public class JobApplicationService : IJobApplicationService
{
    private readonly IJobApplicationRepository _repository;
    private readonly IMapper _mapper;

    public JobApplicationService(IJobApplicationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApplicationResult<IEnumerable<JobApplicationRecord>>> GetAll()
    {
        try
        {
            var applications = await _repository.GetAll();

            if (!applications.Any())
                return ApplicationResult<IEnumerable<JobApplicationRecord>>.Fail("No job applications found.");

            var result = _mapper.Map<IEnumerable<JobApplicationRecord>>(applications);
            return ApplicationResult<IEnumerable<JobApplicationRecord>>.Ok(result);
        }
        catch (Exception ex)
        {
            return ApplicationResult<IEnumerable<JobApplicationRecord>>
                .Fail($"An unexpected error occurred: {ex.Message}");
        }
    }

    public async Task<ApplicationResult<JobApplicationRecord>> GetById(Guid id)
    {
        try
        {
            var application = await _repository.GetById(id);

            if (application is null)
                return ApplicationResult<JobApplicationRecord>.Fail("Job application not found.");

            var result = _mapper.Map<JobApplicationRecord>(application);
            return ApplicationResult<JobApplicationRecord>.Ok(result);
        }
        catch (Exception ex)
        {
            return ApplicationResult<JobApplicationRecord>
                .Fail($"An unexpected error occurred: {ex.Message}");
        }
    }

    public async Task<ApplicationResult<JobApplicationRecord>> Create(CreateJobApplicationRecord request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.CompanyName) || string.IsNullOrWhiteSpace(request.Position))
                return ApplicationResult<JobApplicationRecord>.Fail("Company name and position are required.");

            var entity = _mapper.Map<JobApplication>(request);
            var created = await _repository.Create(entity);
            var result = _mapper.Map<JobApplicationRecord>(created);

            return ApplicationResult<JobApplicationRecord>.Ok(result);
        }
        catch (Exception ex)
        {
            return ApplicationResult<JobApplicationRecord>
                .Fail($"An unexpected error occurred: {ex.Message}");
        }
    }

    public async Task<ApplicationResult<JobApplicationRecord>> Update(Guid id, UpdateJobApplicationRecord request)
    {
        try
        {
            var existingApplication = await _repository.GetById(id);
            if (existingApplication is null)
                return ApplicationResult<JobApplicationRecord>.Fail("Job application not found.");

            existingApplication.CompanyName = request.CompanyName;
            existingApplication.Position = request.Position;
            existingApplication.DateApplied = request.DateApplied;

            if (!Enum.TryParse<ApplicationStatus>(request.Status, true, out var newStatus) ||
                !Enum.IsDefined(typeof(ApplicationStatus), newStatus))
                return ApplicationResult<JobApplicationRecord>.Fail("Invalid status value.");

            if (existingApplication.Status != newStatus)
            {
                var changed = existingApplication.ChangeStatus(newStatus, out var error);
                if (!changed)
                    return ApplicationResult<JobApplicationRecord>.Fail(error ?? "Invalid status change.");
            }

            var updated = await _repository.Update(existingApplication);
            var result = _mapper.Map<JobApplicationRecord>(updated);
             
            return ApplicationResult<JobApplicationRecord>.Ok(result);
        }
        catch (Exception ex)
        {
            return ApplicationResult<JobApplicationRecord>
                .Fail($"An unexpected error occurred: {ex.Message}");
        }
    }

    public async Task<ApplicationResult<bool>> Delete(Guid id)
    {
        try
        {
            var deleted = await _repository.Delete(id);

            if (!deleted)
                return ApplicationResult<bool>.Fail("Error deleting the application.");

            return ApplicationResult<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return ApplicationResult<bool>
                .Fail($"An unexpected error occurred: {ex.Message}");
        }
    }
}
