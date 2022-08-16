using System.ComponentModel.DataAnnotations;

namespace Labo.BLL.Validators
{
    public class BeforeTodayAttribute: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime? date = value as DateTime?;
            if (date is null)
            {
                return true;
            }
            return date < DateTime.Now;
            //return value is DateTime d ? d < DateTime.Now : true;
        }
    }
}
