using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Configuration;
using System.Data;

namespace WebApplication1.Controllers
{
   
    public class ValuesController : ApiController
    {
       YogaEmployee emp = new YogaEmployee();

        // GET api/values
        public List<YogaEmployee> Get()
        {
            List<YogaEmployee> employees = new List<YogaEmployee>(); 
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["YogaEmp"].ConnectionString))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from YogaEmployee", con);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                for(int i=0;i<dt.Rows.Count;i++)
                {
                    YogaEmployee emp = new YogaEmployee();
                    emp.Id =Convert.ToInt32(dt.Rows[i]["Id"]);
                    emp.Name = dt.Rows[i]["Name"].ToString();
                    emp.Age = Convert.ToInt32(dt.Rows[i]["Age"]);
                    employees.Add(emp);

                }

            }
            return employees;
        }

        // GET api/values/5
        public YogaEmployee Get(int id)

        { 
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["YogaEmp"].ConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("select * from YogaEmployee where Id=@Id", con);

                command.Parameters.AddWithValue("@Id",id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                
           /*      SqlDataAdapter adapter= new SqlDataAdapter("select * from YogaEmployee where Id=@Id", con);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", id);*/

                 


                DataTable dt = new DataTable();
                 adapter.Fill(dt);
                 if (dt.Rows.Count > 0)
                {

                    emp.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    emp.Name = dt.Rows[0]["Name"].ToString();
                    emp.Age = Convert.ToInt32(dt.Rows[0]["Age"]);
                }
                

            }
            return emp;
           
        }

        // POST api/values
        public string Post(YogaEmployee emp)
        {
            string response;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["YogaEmp"].ConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand("Insert into YogaEmployee(Id,Name,Age) values (@Id,@Name,@Age)", con);

                command.Parameters.AddWithValue("@Id", emp.Id);
                command.Parameters.AddWithValue("@Name", emp.Name);
                command.Parameters.AddWithValue("@Age", emp.Age);

                //SqlDataAdapter adapter = new SqlDataAdapter(command);
                int i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    response = "success";
                }
                else
                {
                    response = "Fail";
                }
                return response;

            }
                
          }

        // PUT api/values/5
        public void Put(int id,YogaEmployee emp)
        {
           
            if (emp!=null)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["YogaEmp"].ConnectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("update YogaEmployee set Name=@Name, Age=@Age where Id=@Id", con);

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", emp.Name);
                    command.Parameters.AddWithValue("@Age", emp.Age);

                    //SqlDataAdapter adapter = new SqlDataAdapter(command);
                    int i = command.ExecuteNonQuery();
                   
                }
                
            }
           
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            if (emp != null)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["YogaEmp"].ConnectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("Delete from YogaEmployee where Id=@Id", con);

                    command.Parameters.AddWithValue("@Id", id);

                    //SqlDataAdapter adapter = new SqlDataAdapter(command);
                    int i = command.ExecuteNonQuery();


                }
            }
        }
    
    }
}
