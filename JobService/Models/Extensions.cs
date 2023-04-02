using YattCommon.Contracts;
using YattCommon.Dtos;

namespace JobService.Models
{
    public static class Extensions
    {
        public static JobDto AsDto(this Job dto)
        {
            return new JobDto
            {
                Id = dto.Id,
                SubscribeId = dto.SubscribeId,
                JobType = dto.JobType,
                Level = dto.Level,
                Employment = dto.Employment,
                Title = dto.Title,
                Salary = dto.Salary,
                Description = dto.Description,
                Location = dto.Location,
                DeadlineDate = dto.DeadlineDate,
                CreatedDate = dto.CreatedDate,
                ModifiedDate = dto.ModifiedDate,
                Status = dto.Status,
            };
        }

        public static DutyDto AsDto(this Duty dto)
        {
            return new DutyDto
            {
                Id = dto.Id,
                JobId = dto.JobId,
                Detail = dto.Detail,
            };
        }
        public static QualificationDto AsDto(this Qualification dto)
        {
            return new QualificationDto
            {
                Id = dto.Id,
                JobId = dto.JobId,
                Level = dto.Level,
                Title = dto.Title,
            };
        }
        public static DescriptionDto AsDto(this Description dto)
        {
            return new DescriptionDto
            {
                Id = dto.Id,
                JobId = dto.JobId,
                Detail = dto.Detail,
            };
        }

        public static JobDetailDto AsDto(this Job job, List<DutyDto> duties,

            List<QualificationDto> qualifications, List<DescriptionDto> descriptions)
        {
            return new JobDetailDto
            {
                Id = job.Id,
                SubscribeId = job.SubscribeId,
                JobType = job.JobType,
                Level = job.Level,
                Employment = job.Employment,
                Title = job.Title,
                Salary = job.Salary,
                Description = job.Description,
                Location = job.Location,
                DeadlineDate = job.DeadlineDate,
                CreatedDate = job.CreatedDate,
                ModifiedDate = job.ModifiedDate,
                Status = job.Status, 

                Duties = duties,
                Qualifications = qualifications,
                Descriptions = descriptions
            };
        }

        public static JobContract AsContract(this Job item)
        {
            return new JobContract
            (item.Id,item.Title, item.JobType, item.Level, item.Employment, item.Salary, item.Description, item.Location, item.DeadlineDate, item.ModifiedDate, item.Status);
              
        }

    }
}
