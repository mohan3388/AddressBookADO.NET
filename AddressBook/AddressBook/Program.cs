namespace AddressBook
{
    class Program
    {
        public static void Main(string[] args)
        {
            AddressBookRepo repo = new AddressBookRepo();
            repo.EstablishConnection();
            repo.CloseConnection();
        }
    }
}