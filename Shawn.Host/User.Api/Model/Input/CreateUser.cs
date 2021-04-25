using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Model.Input
{
    public class CreateUserInput
    {
        [Required]
        public string Phone { get; set; }
    }
}
