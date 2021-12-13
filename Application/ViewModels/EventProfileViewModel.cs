﻿using Domain;
using System.Collections.Generic;
using System;

namespace Application.ViewModels
{
    public class EventProfileViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<EventMembership> Memberships { get; set; }
        public ICollection<InterestTag> Topics { get; set; }
        public ICollection<EventAttachedFile> AttachedFiles { get; set; }
        public int ChatId { get; set; }
    }
}
