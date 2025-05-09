namespace JobTracker.Api.Mapping;
public class ApiMappingConfig : Profile
{
    public ApiMappingConfig()
    {
        // Api/Request > Application/Record
        CreateMap<CreateJobApplicationRequest, CreateJobApplicationRecord>();
        CreateMap<UpdateJobApplicationRequest, UpdateJobApplicationRecord>();

        // Record/Application > Api/Response
        CreateMap<JobApplicationRecord, JobApplicationResponse>();

        CreateMap<UpdateJobApplicationRecord, JobApplication>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
