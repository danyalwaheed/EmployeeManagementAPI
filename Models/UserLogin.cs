using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CRUDApi.Models
{
    [Keyless]
    public partial class UserLogin
    {

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
