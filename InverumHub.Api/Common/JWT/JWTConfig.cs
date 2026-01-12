using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Api.Common.JWT
{
    public class JWTConfig
    {
        public string KeyId { get; set; } = default!;
        public string SecretKeyBase64 { get; set; } = default!;
        public int ExpirationMinutes { get; set; } = 60;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
    }
}
