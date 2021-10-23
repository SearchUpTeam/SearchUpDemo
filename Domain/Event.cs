using System;
using System.Collections.Generic;

namespace Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        //public virtual ICollection<File> EventPictures { get; set; }
        public virtual ICollection<InterestTag> Topics { get; set; }



    }
}
