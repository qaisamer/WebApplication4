namespace WebApplication4.viewmodel
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string FirstName;
        public string LastName;
        public string Email;
        public IEnumerable<string> Roles;
    }
}
