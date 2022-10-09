using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Models
{
    public class AuthResponseModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
    }
}
