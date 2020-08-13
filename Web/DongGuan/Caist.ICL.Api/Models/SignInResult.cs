using Caist.ICL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.ICL.Core;

namespace Caist.ICL.Api.Models
{
    public class SignInResult
    {
        public string Token { get; set; }
        public DateTime Exp { get; set; }
        public CurrentUser User { get; set; }
        public string Avatar { get; set; }
    }
    public class RePwd
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
