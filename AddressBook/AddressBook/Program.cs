namespace AddressBook
{
    class Program
    {
        public static void Main(string[] args)
        {
            AddressBookRepo repo = new AddressBookRepo();
            repo.EstablishConnection();
            repo.CloseConnection();
            bool check = true;
            while (check)
            {
                Console.WriteLine("1. To check connection \n2. To Insert the Data in Data Base \n");
                Console.WriteLine("Enter the Above Option");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        repo.EstablishConnection();
                        repo.CloseConnection();
                        break;
                     case 2:
                        Model empModel = new Model();
                        //empModel.Id = 1;
                        empModel.FirstName = "Mohan";
                        empModel.LastName = "Sahu";
                        empModel.Address = "Bera";
                        empModel.City = "Bemetara";
                        empModel.State = "CG";

                        empModel.ZipCode = 491335;
                        empModel.PhoneNumber = "7898625487";
                        empModel.Email = "Mohan@12gmail.com";

                        repo.AddContact(empModel);
                        break;
                    default:
                        Console.WriteLine("Please Enter the Correct option");
                        break;

                }
            }
        }
    }
}