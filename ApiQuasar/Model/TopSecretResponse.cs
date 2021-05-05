using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiQuasar.Model
{
    public class TopSecretResponse
    {
        public TopSecretResponse(Position position, string message)
        {
            this.Position = position;
            this.Message = message;
        }

        public Position Position { get; set; }
        public string Message { get; set; }
    }
}
