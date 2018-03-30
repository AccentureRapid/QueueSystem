using Abp.AutoMapper;
using Pfizer.QueueSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pfizer.QueueSystem.Services.Dto
{
    public class FastTokenDto
    {
        public int Id { get; set; }
        public string NtId { get; set; }
    }
    public class FastTokenResult
    {
        public bool Success { get; set; }

        public FastToken FastToken { get; set; }

        public string Message { get; set; }
    }

    public class UserIdDto
    {
        public string NtId { get; set; }
    }
}
