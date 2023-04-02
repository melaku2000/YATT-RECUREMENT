using YattCommon.Dtos;

namespace ApplicantService.Models
{
    public static class Extensions
    {
        public static ApplicantDto AsDto(this Applicant dto,string candidateName)
        {
            return new ApplicantDto
            {
                Id = dto.Id,
                CandidateId = dto.CandidateId,
                JobId = dto.JobId,
                CandidateName = candidateName,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate
            };
        }
    }
}
