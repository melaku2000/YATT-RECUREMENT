using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YattCommon.Enums;

namespace YattCommon.Dtos
{
    public class JobDto
    {
        public Guid Id { get; set; }
        public Guid SubscribeId { get; set; }
        public string? Title { get; set; }
        public JobType JobType { get; set; }
        public ExperianceLevel Level { get; set; }
        public Employment Employment { get; set; }
        public decimal Salary { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime DeadlineDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ServiceStatus Status { get; set; }
    }

    public class DutyDto
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string? Detail { get; set; }
    }
    public class QualificationDto
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string? Title { get; set; }
        public EducationLevel Level { get; set; }
    }

    public class DescriptionDto
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public string? Detail { get; set; }
    }
    public class JobDetailDto
    {
        public Guid Id { get; set; }
        public Guid SubscribeId { get; set; }
        public string? Title { get; set; }
        public JobType JobType { get; set; }
        public ExperianceLevel Level { get; set; }
        public Employment Employment { get; set; }
        public decimal Salary { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime DeadlineDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ServiceStatus Status { get; set; }

        public ICollection<DutyDto> Duties { get; set; } = null!;
        public ICollection<QualificationDto> Qualifications { get; set; } = null!; 
        public ICollection<DescriptionDto> Descriptions { get; set; } = null!;  
    }
}
