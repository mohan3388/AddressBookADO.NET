using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook
{
    public class AddressBookRepo
    {
        static string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = AddressBookAdo; Integrated Security = SSPI";
        SqlConnection connection = new SqlConnection(connectionString);

        public void EstablishConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    throw new CustomException(CustomException.ExceptionType.Connection_Failed, "Connection not found");
                }
            }
        }
        public void CloseConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new CustomException(CustomException.ExceptionType.Connection_Failed, "Connection not found");
                }
            }
        }
        public string AddContact(Model obj)
        {
            try
            {
                
                SqlCommand com = new SqlCommand("spAddNewPersons", connection);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@FirstName", obj.FirstName);
                com.Parameters.AddWithValue("@LastName", obj.LastName);
                com.Parameters.AddWithValue("@Address", obj.Address);
                com.Parameters.AddWithValue("@City", obj.City);
                com.Parameters.AddWithValue("@State", obj.State);

                com.Parameters.AddWithValue("@ZipCode", obj.ZipCode);
                com.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber);
                com.Parameters.AddWithValue("@Email", obj.Email);


                connection.Open();
                int i = com.ExecuteNonQuery();
                connection.Close();
                if (i != 0)
                {
                    return "data Added";
                }
                else
                {
                    return "data not added";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public List<Model> GetAllEmployees()
        {
            
            List<Model> EmpList = new List<Model>();
            SqlCommand com = new SqlCommand("spViewContacts", connection);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            connection.Open();
            da.Fill(dt);
            connection.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(
                    new Model
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Address = Convert.ToString(dr["Address"]),
                        City = Convert.ToString(dr["City"]),
                        State = Convert.ToString(dr["State"]),
                        ZipCode = Convert.ToInt32(dr["ZipCode"]),
                        PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                        Email = Convert.ToString(dr["Email"]),

                    }
                    );
            }
            return EmpList;
        }
        //To Update Emp data   
        public bool UpdateEmp(Model obj)
        {
           
            SqlCommand com = new SqlCommand("SPUpdateDetails", connection);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Id);

            com.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber);

            connection.Open();
            int i = com.ExecuteNonQuery();
            connection.Close();
            if (i != 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete details
        public bool DeleteEmployee(int Id)
        {
          
            SqlCommand com = new SqlCommand("spDeletePersonById", connection);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", Id);

            connection.Open();
            int i = com.ExecuteNonQuery();
            connection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Model> RetrieveDataUsingCityName(string City, string State)
        {
           
            List<Model> EmpList = new List<Model>();
            SqlCommand com = new SqlCommand("spViewContactsUsingCityName", connection);
            connection.Open();
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@City", City);
            com.Parameters.AddWithValue("@State", State);
            SqlDataReader da = com.ExecuteReader();
            DataTable dt = new DataTable();



            if (da.HasRows)
            {
                while (da.Read())
                {
                    Model emp = new Model();
                    emp.FirstName = da.GetString(1);
                    emp.LastName = da.GetString(2);
                    emp.Address = da.GetString(3);
                    emp.City = da.GetString(4);
                    emp.State = da.GetString(5);
                    emp.ZipCode = da.GetInt32(6);
                    emp.PhoneNumber = da.GetString(7);
                    emp.Email = da.GetString(8);
                    List<Model> list = new List<Model>();
                    list.Add(emp);
                    DisplayEmployeeDetails(list);
                }
            }
            connection.Close();
            //Bind EmpModel generic list using dataRow     

            return EmpList;
        }
        public void DisplayEmployeeDetails(List<Model> sqlDataReader)
        {
            foreach (Model addressBookModel in sqlDataReader)
            {
                Console.WriteLine("FirstName: " + addressBookModel.FirstName + " LastName: " + addressBookModel.LastName + " Address: " + addressBookModel.Address + " City: " + addressBookModel.City + " State: " + addressBookModel.State + " ZipCode " + addressBookModel.ZipCode + " Phone number " + addressBookModel.PhoneNumber + " Email " + addressBookModel.Email);
            }
          
        }
        public bool CountDataFromCityAndState(Model address)
        {
            

            List<Model> list = new List<Model>();
           
            using (connection)
            {
                SqlCommand command = new SqlCommand("CountByCityState", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(@"City", address.City);
                command.Parameters.AddWithValue(@"State", address.State);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        address.Id = reader.GetInt32(0);
                        address.FirstName = reader.GetString(1);
                        address.LastName = reader.GetString(2);
                        address.Address = reader.GetString(3);
                        address.City = reader.GetString(4);
                        address.State = reader.GetString(5);
                        address.ZipCode = reader.GetInt32(6);
                        address.PhoneNumber = reader.GetString(7);
                        address.Email = reader.GetString(8);
                        list.Add(address);

                    }
                    Console.WriteLine("Count the Address");
                    Console.WriteLine(list.Count());
                    return true;
                }
                else
                {
                    Console.WriteLine("No Data Found");
                    return false;
                }
                connection.Close();
            }
        }
    }
}
