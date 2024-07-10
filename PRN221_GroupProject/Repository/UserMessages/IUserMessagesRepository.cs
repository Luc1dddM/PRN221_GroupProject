namespace PRN221_GroupProject.Repository.UserMessages
{
    public interface IUserMessagesRepository
    {
        public void CreateUserMessageAsync(string messageId);
        public void CreateAdminUserMessageAsync(string messageId, string groupName);
        public void UpdateUserMessage(string receiverId);

    }
}
