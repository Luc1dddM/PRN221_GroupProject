using PRN221_GroupProject.DTO;

namespace PRN221_GroupProject.Repository.UserMessages
{
    public interface IUserMessagesRepository
    {
        public void CreateUserMessageAsync(string messageId);
        public void CreateAdminUserMessageAsync(string messageId, string groupName);
        public void UpdateUserMessage(string groupName, string receiverId);
        public int CountMessagesUnReadCustomer(string receiver, string groupName);
        public int CountMessagesUnReadAdmin(string receiver);
        public Task<List<MessageDTO>> CountMessagesUnReadSpecific(string receiver);

    }
}
