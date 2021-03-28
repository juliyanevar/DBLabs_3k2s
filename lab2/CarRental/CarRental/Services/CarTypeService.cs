using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CarRental.Services
{
    public class CarTypeService
    {
        private CarTypeService _carTypeService;
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IList<CarTypeModel> GetCarTypes()
        {
            _carTypeService = new CarTypeService();
            IList<CarTypeModel> list = new List<CarTypeModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCarTypes", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        CarTypeModel obj = new CarTypeModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        obj.CarType = Convert.ToString(_ds.Tables[0].Rows[i]["carType"]);
                        list.Add(obj);
                    }
                }
            }
            return list;
        }

        public void AddCarType(CarTypeModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddCarType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@carType", model.CarType);
                cmd.ExecuteNonQuery();
            }
        }

        public CarTypeModel GetCarType(int id)
        {
            _carTypeService = new CarTypeService();
            var model = new CarTypeModel();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCarTypeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        model.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        model.CarType = Convert.ToString(_ds.Tables[0].Rows[i]["carType"]);
                    }
                }
            }
            return model;
        }

        public void UpdateCarType(CarTypeModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCarType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@carType", model.CarType);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteCarType(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteCarType", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}