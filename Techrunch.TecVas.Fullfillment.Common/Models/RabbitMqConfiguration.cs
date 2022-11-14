﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Fullfillment.Common.Models
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
    }
}
