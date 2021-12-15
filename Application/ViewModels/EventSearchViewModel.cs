using Domain;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class EventSearchViewModel
    {
        public int Page { get; set; } = 1;
        public string SearchStr { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
