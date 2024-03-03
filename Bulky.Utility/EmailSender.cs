﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to send email.
            return Task.CompletedTask;
        }
    }
}