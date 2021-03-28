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
    public class RentalService
    {
        private RentalService _rentalService;
        private CarService _carService;
        private ClientService _clientService;
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IList<RentalModel> GetRentals()
        {
            _rentalService = new RentalService();
            _carService = new CarService();
            _clientService = new ClientService();
            IList<RentalModel> list = new List<RentalModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetRentals", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        RentalModel obj = new RentalModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        obj.Client = _clientService.GetClient(Convert.ToInt32(_ds.Tables[0].Rows[i]["client"]));
                        obj.Car = _carService.GetCar(Convert.ToInt32(_ds.Tables[0].Rows[i]["car"]));
                        obj.DateOfIssue = Convert.ToDateTime(_ds.Tables[0].Rows[i]["dateOfIssue"]);
                        obj.CountOfDays = Convert.ToInt32(_ds.Tables[0].Rows[i]["countOfDays"]);
                        obj.Amount = Convert.ToDecimal(_ds.Tables[0].Rows[i]["amount"]);
                        list.Add(obj);
                    }
                }
            }
            return list;
        }

        public void AddRental(RentalModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddRental", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_client", model.Client.Id);
                cmd.Parameters.AddWithValue("@id_car", model.Car.Id);
                cmd.Parameters.AddWithValue("@date", model.DateOfIssue);
                cmd.Parameters.AddWithValue("@count_days", model.CountOfDays);
                cmd.ExecuteNonQuery();
            }
        }

        public RentalModel GetRental(int id)
        {
            _rentalService = new RentalService();
            _carService = new CarService();
            _clientService = new ClientService();
            var model = new RentalModel();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetRentalById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        model.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        model.Client = _clientService.GetClient(Convert.ToInt32(_ds.Tables[0].Rows[i]["client"]));
                        model.Car = _carService.GetCar(Convert.ToInt32(_ds.Tables[0].Rows[i]["car"]));
                        model.DateOfIssue = Convert.ToDateTime(_ds.Tables[0].Rows[i]["dateOfIssue"]);
                        model.CountOfDays = Convert.ToInt32(_ds.Tables[0].Rows[i]["countOfDays"]);
                        model.Amount = Convert.ToDecimal(_ds.Tables[0].Rows[i]["amount"]);
                    }
                }
            }
            return model;
        }

        public void UpdateRental(RentalModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateRental", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@id_client", model.Client.Id);
                cmd.Parameters.AddWithValue("@id_car", model.Car.Id);
                cmd.Parameters.AddWithValue("@date", model.DateOfIssue);
                cmd.Parameters.AddWithValue("@count_days", model.CountOfDays);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteRental(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteRental", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IList<RentalModel> GetRentalsOverPeriod(DateTime firstDay, DateTime lastDay)
        {
            _rentalService = new RentalService();
            _carService = new CarService();
            _clientService = new ClientService();
            IList<RentalModel> list = new List<RentalModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetRentalsOverPeriod", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstDay", firstDay);
                cmd.Parameters.AddWithValue("@lastDay", lastDay);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        RentalModel obj = new RentalModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        obj.Client = _clientService.GetClient(Convert.ToInt32(_ds.Tables[0].Rows[i]["client"]));
                        obj.Car = _carService.GetCar(Convert.ToInt32(_ds.Tables[0].Rows[i]["car"]));
                        obj.DateOfIssue = Convert.ToDateTime(_ds.Tables[0].Rows[i]["dateOfIssue"]);
                        obj.CountOfDays = Convert.ToInt32(_ds.Tables[0].Rows[i]["countOfDays"]);
                        obj.Amount = Convert.ToDecimal(_ds.Tables[0].Rows[i]["amount"]);
                        list.Add(obj);
                    }
                }
            }
            return list;
        }
    }
}