using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebMVC.Models;

namespace WebMVC.Service
{
    public class EmployeeServices
    {
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;
        public List<EmployeeModel> GetEmployeeList()
        {
            IList<EmployeeModel> getEmpList = new List<EmployeeModel>();
            _ds = new DataSet();
            

            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "getEmpList");
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        EmployeeModel obj = new EmployeeModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["Id"]);
                        obj.EmpName = Convert.ToString(_ds.Tables[0].Rows[i]["EmpName"]);
                        obj.EmailId = Convert.ToString(_ds.Tables[0].Rows[i]["EmailId"]);
                        obj.MobileNo = Convert.ToString(_ds.Tables[0].Rows[i]["MobileNo"]);
                        getEmpList.Add(obj);
                    }
                }
            }

            return (List<EmployeeModel>)getEmpList;
        }
        public void InsertEmployee(EmployeeModel model)
        {
            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "AddEmployee");
                cmd.Parameters.AddWithValue("EmpName", model.EmpName);
                cmd.Parameters.AddWithValue("EmpEmailId", model.EmailId);
                cmd.Parameters.AddWithValue("EmpMobileNo", model.MobileNo);
                cmd.ExecuteNonQuery();
            }
        }
        public EmployeeModel GetEditById(int Id)
        {
            var model = new EmployeeModel();
            _ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetEmployeeById");
                cmd.Parameters.AddWithValue("@EmpId", Id);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["Id"]);
                    model.EmpName = Convert.ToString(_ds.Tables[0].Rows[0]["EmpName"]);
                    model.EmailId = Convert.ToString(_ds.Tables[0].Rows[0]["EmailId"]);
                    model.MobileNo = Convert.ToString(_ds.Tables[0].Rows[0]["MobileNo"]);
                }
            }

                    return model;
        }
        public void UpdateEmp(EmployeeModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "UpdateEmployee");
                cmd.Parameters.AddWithValue("@EmpName", model.EmpName);
                cmd.Parameters.AddWithValue("@EmpEmailId", model.EmailId);
                cmd.Parameters.AddWithValue("@EmpMobileNo", model.MobileNo);
                cmd.Parameters.AddWithValue("@EmpId", model.Id);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteEmp(int Id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteEmployee");
                cmd.Parameters.AddWithValue("@EmpId", Id);
                cmd.ExecuteNonQuery();

            }
        }
    }
}