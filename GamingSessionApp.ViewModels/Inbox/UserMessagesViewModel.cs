﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingSessionApp.ViewModels.Inbox
{
    public class UserMessagesViewModel
    {
        public Guid Id { get; set; }

        public string SenderImageUrl { get; set; }

        [Display(Name = "From")]
        public string SenderName { get; set; }

        [Display(Name = "Sent")]
        public DateTime SentDate { get; set; }

        public string Subject { get; set; }
        
        public bool Read { get; set; }
    }
}