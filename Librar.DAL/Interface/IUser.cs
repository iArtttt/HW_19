namespace Librar.DAL.Interface
{
    public interface IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
    }
}
