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
    }
}
