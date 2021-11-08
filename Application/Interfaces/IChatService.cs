using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChatService
    {
        Task CreateAsync(Chat chat, int userId);
        Task<Chat> GetChatByIdAsync(int id);
        Task<IEnumerable<Chat>> GetChatsAsync(int userId);
        Task Update(Chat chat);
        Task Delete(Chat chat);
        Task JoinChat(int chatId, int userId);
        Task CreateMessage(Message message);
    }
}
