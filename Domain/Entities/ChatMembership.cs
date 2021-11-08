using Domain.Enums;
using System;

namespace Domain
{
    public class ChatMembership
    {
        bool _isKicked;
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
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
