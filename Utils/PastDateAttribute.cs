using System.ComponentModel.DataAnnotations;

namespace iPortal.Utils
{
    public class PastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Cho phép null nếu Dob là DateTime?
            }

            if (value is DateTime date)
            {
                return date < DateTime.Now;
            }

            return false;
        }
    }
}