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
                Console.WriteLine("1. To check connection \n2. To Insert the Data in Data Base \n3. Edit data");
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
                        List<Model> empList = repo.GetAllEmployees();
                        foreach (Model data in empList)
                        {
                            Console.WriteLine(data.Id + " " + data.FirstName + " " + data.LastName + " " + data.Address + " " + data.City + " " + data.State + " " + data.ZipCode + " " + data.PhoneNumber + " " + data.Email);
                        }
                        break;
                    case 3:
                        Model emp = new Model();
                        emp.Id = 1;

                        emp.PhoneNumber = "7847850147";
                        repo.UpdateEmp(emp);
                      
                        break;
                    default:
                        Console.WriteLine("Please Enter the Correct option");
                        break;

                }
            }
        }

    }
}