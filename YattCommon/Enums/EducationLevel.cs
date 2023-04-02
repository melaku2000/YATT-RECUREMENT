using YattCommon.Enums.Extensions;

namespace YattCommon.Enums
{
    public enum EducationLevel
    {
        [StringValue("None")] None = 221,
        [StringValue("Elementary")] Elementary = 222,
        [StringValue("Juniour Secondary")] JuniourSecondary = 223,
        [StringValue("Seniour Secondary")] SeniourSecondary = 224,
        [StringValue("Diploma")] Diploma = 225,
        [StringValue("Advance Diploma")] AdvanceDiploma = 226,
        [StringValue("Bachelor of Education")] BachelorOfEducation = 227,
        [StringValue("Bachelor of Art")] BachelorOfArt = 228,
        [StringValue("Bachelor of Science")] BachelorOfScience = 229,
        [StringValue("Masters")] Masters = 230,
        [StringValue("Doctorate")] Doctorate = 231,
        [StringValue("Certificate")] Certificate = 232,
        [StringValue("Others")] Others = 233
    }
}
