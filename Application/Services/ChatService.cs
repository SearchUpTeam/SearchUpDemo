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
        public async Task CreateAsync(
            Chat chat,
            int userId)
        {
            MemberType memberType;
            if (chat.ChatType == ChatType.Default)
                memberType = MemberType.Participant;
            else memberType = MemberType.Organizer;
            var membership = new ChatMembership()
            {
                UserId = userId,
                MemberType = memberType
            };
            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();
            membership.ChatId = chat.Id;
            await _context.ChatMemberships.AddAsync(membership);
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
            var chat = await _context.Chats.Where(c => c.Id == id)
                .Include(c => c.Messages)
                .ThenInclude(m=>m.Sender)
                .FirstOrDefaultAsync();
            return chat;
        }
        public async Task<IEnumerable<Chat>> GetChatsAsync(int userId)
        {
            var chatsId = _context.ChatMemberships
                .Where(m => m.UserId == userId)
                .Select(m => m.ChatId);

            return await _context.Chats
                .Where( c => chatsId.Contains(c.Id))
                .ToListAsync();

        }
        public async Task<bool> IsUserInChat(int userId, int chatId)
        {
            return await _context.ChatMemberships
                .AnyAsync(m => m.UserId == userId && m.ChatId == chatId);
        }
        public async Task JoinChat(
            int chatId,
            int userId)
        {
            var membership = new ChatMembership()
            {
                ChatId = chatId,
                UserId = userId,
                MemberType = MemberType.Participant
            };
            await _context.ChatMemberships.AddAsync(membership);
            await _context.SaveChangesAsync();
        }
        public async Task LeaveChat(
            int chatId,
            int userId)
        {
            var member = await _context.ChatMemberships
                .Where(m => m.ChatId == chatId && m.UserId == userId)
                .FirstOrDefaultAsync();
            _context.ChatMemberships.Remove(member);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Chat chat)
        {
            _context.Entry(chat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
