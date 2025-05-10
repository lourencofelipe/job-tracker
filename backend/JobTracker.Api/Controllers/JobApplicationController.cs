namespace JobTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class JobApplicationController : ControllerBase
{
    private readonly IJobApplicationService _service;
    public JobApplicationController(IJobApplicationService service) => _service = service;

    /// <summary>
    /// Retrieves all job applications.
    /// </summary>
    /// <returns>List of job applications.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<JobApplicationRecord>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<JobApplicationRecord>>> GetAll()
    {
        var result = await _service.GetAll();

        if (!result.Success)
            return Ok(Enumerable.Empty<JobApplicationRecord>());

        return Ok(result.Value);
    }

    /// <summary>
    /// Retrieves a job application by ID.
    /// </summary>
    /// <param name="id">Job application ID</param>
    /// <returns>Job application record.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(JobApplicationRecord), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobApplicationRecord>> GetById(Guid id)
    {
        var result = await _service.GetById(id);

        if (!result.Success)
            return NotFound(new { errors = result.Error });

        return Ok(result.Value);
    }

    /// <summary>
    /// Creates a new job application.
    /// </summary>
    /// <param name="request">Data to create a job application.</param>
    /// <returns>A new job application record.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(JobApplicationRecord), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JobApplicationRecord>> Create([FromBody] CreateJobApplicationRecord request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid model state.");

        var result = await _service.Create(request);

        if (!result.Success)
            return BadRequest(new { errors = result.Error });

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
    }

    /// <summary>
    /// Updates a job application.
    /// </summary>
    /// <param name="id">Job application ID</param>
    /// <param name="request">Data to update the record</param>
    /// <returns>The upated job application.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(JobApplicationRecord), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JobApplicationRecord>> Update(Guid id, [FromBody] UpdateJobApplicationRecord request)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid model state.");

        var result = await _service.Update(id, request);

        if (!result.Success)
        {
            if (result.Error == "Job application not found.")
                return NotFound(new { errors = result.Error });

            return BadRequest(new { errors = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Deletes a job application.
    /// </summary>
    /// <param name="id">Job application ID</param>
    /// <returns>No content if successfully removed.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _service.Delete(id);

        if (!result.Success)
            return NotFound(new { errors = result.Error });

        return NoContent();
    }
}
