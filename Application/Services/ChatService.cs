using Application.Interfaces;
using Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly SearchUpContext _context;
        public ChatService(SearchUpContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Chat chat, int userId)
        {
            MemberType memberType;
            if (chat.ChatType == ChatType.Default)
                memberType = MemberType.Participant;
            else memberType = MemberType.Organizer;
            var member = new Member()
            {
                UserId = userId,
                MemberType = memberType
            };
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
            member.ChatId = chat.Id;
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }
        public async Task CreateMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Chat chat)
        {
            _context.Remove(chat);
            await _context.SaveChangesAsync();
        }
        public async Task<Chat> GetChatByIdAsync(int id)
        {
            var chat = await _context.Chats.FindAsync(id);
            return chat;
        }
        public async Task<IEnumerable<Chat>> GetChatsAsync(int userId)
        {
            var chats =  await _context.User
                .Where(u => u.Id == userId)
                .Select(c => c.Chats)
                .FirstOrDefaultAsync();
            return chats;
        }
        public async Task JoinChat(int chatId, int userId)
        {
            var member = new Member()
            {
                ChatId = chatId,
                UserId = userId,
                MemberType = MemberType.Participant
            };
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Chat chat)
        {
            _context.Entry(chat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
