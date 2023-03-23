namespace Sat.Recruitment.Api.Models
{
    /// <summary>
    /// Represents the user creation response
    /// </summary>
    public sealed class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}