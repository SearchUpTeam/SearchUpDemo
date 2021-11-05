using System;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class EventMembership
    {
        [NotMapped]
        bool _isKicked = false;
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public MemberType MemberType { get; set; }
        public bool IsKicked {
            get { return _isKicked; }
            set { 
                if(value==true && _isKicked==true)
                    throw new ArgumentException("Member is already kicked");
                else if(value==false && _isKicked==false)
                    throw new ArgumentException("Member is already unkicked");
                else
                    _isKicked = value;
            }
        }
    }
}