
using YattCommon.Enums.Extensions;

namespace YattCommon.Enums 
{
    public enum TokenType
    {
        [StringValue("Email confermation")] EmailConfirmation=111,
        [StringValue("Phone confermation")] PhoneConfirmation=112,
        [StringValue("Password reset")] PasswordReset=113
    }
}
