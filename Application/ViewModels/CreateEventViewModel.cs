using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Domain;
using System.Linq;

namespace Application.ViewModels
{
    public class CreateEventViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public string SelectedTopicsInput { get; set; }
    
        public IEnumerable<int> SelectedTopicsId {
            get {return SelectedTopicsInput.Split(',')
                .Select(t=> Int32.Parse(t))
                .ToList();}
        }
        public ICollection<EventAttachedFile> Files { get; set; }
    }
}