namespace JobTracker.Application.Common.Mapping;
public class ApplicationAutoMapperConfig : Profile
{
    public ApplicationAutoMapperConfig()
    {
        // Entity <> Record
        CreateMap<JobApplication, JobApplicationRecord>().ReverseMap();

        // Create Record > Entity
        CreateMap<CreateJobApplicationRecord, JobApplication>();

        // Update Record > Entity
        CreateMap<UpdateJobApplicationRecord, JobApplication>()
            .ForMember(i => i.Id, opt => opt.Ignore());
    }
}
