using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Auth.Service.Model;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Auth.Service
{
    public static class AudienceStore
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudienceStore()
        {
            AudiencesList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                new Audience
                {
                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                    Name = "PayU.Service"
                });
        }

        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RandomNumberGenerator.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            var newaudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            AudiencesList.TryAdd(clientId, newaudience);
            return newaudience;
        }

        public static Audience FindAudience(string clientId)
        {
            return AudiencesList.TryGetValue(clientId, out var audience) ? audience : null;
        }
    }
}