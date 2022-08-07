using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labo.API.DTO
{
    public class ValidationErrorDTO
    {
        public int Status => 400;
        public string Title => "One or more validation errors occurred.";
        public string? TraceId { get; }
        public Dictionary<string, string[]>? Errors { get; }

        public ValidationErrorDTO(ValidationException ex)
        {
            TraceId = Activity.Current?.Id;
            if(ex.Source != null)
            {
                Errors = new Dictionary<string, string[]> { { ex.Source, new string[] { ex.Message } } };
            }
        }
    }
}
