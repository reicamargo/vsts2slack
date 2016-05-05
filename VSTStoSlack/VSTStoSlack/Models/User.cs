using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VSTStoSlack.Models
{
    public class User
    {
        public string VstsName { get; set; }
        public string SlackName { get; set; }
    }
}