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
    public class CarService
    {
        private CarService _carService;
        private BrandService _brandService;
        private CarTypeService _carTypeService;
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IList<CarModel> GetCars()
        {
            _carService = new CarService();
            _brandService = new BrandService();
            _carTypeService = new CarTypeService();
            IList<CarModel> list = new List<CarModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCars", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        CarModel obj = new CarModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        obj.GovernmentPlate = Convert.ToString(_ds.Tables[0].Rows[i]["governmentPlate"]);
                        obj.Brand = _brandService.GetBrand(Convert.ToInt32(_ds.Tables[0].Rows[i]["brand"]));
                        obj.YearOfRelease = Convert.ToInt32(_ds.Tables[0].Rows[i]["yearOfRelease"]);
                        obj.CarType = _carTypeService.GetCarType(Convert.ToInt32(_ds.Tables[0].Rows[i]["typeCar"]));
                        obj.CostOf1Day = Convert.ToDecimal(_ds.Tables[0].Rows[i]["costOf1Day"]);
                        list.Add(obj);
                    }
                }
            }
            return list;
        }

        public void AddCar(CarModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddCar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@governmentPlate", model.GovernmentPlate);
                cmd.Parameters.AddWithValue("@brand", model.Brand.Id);
                cmd.Parameters.AddWithValue("@yearOfRelease", model.YearOfRelease);
                cmd.Parameters.AddWithValue("@typeCar", model.CarType.Id);
                cmd.Parameters.AddWithValue("@costOf1Day", model.CostOf1Day);
                cmd.ExecuteNonQuery();
            }
        }

        public CarModel GetCar(int id)
        {
            _carService = new CarService();
            var model = new CarModel();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                _brandService = new BrandService();
                _carTypeService = new CarTypeService();
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCarById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        model.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        model.GovernmentPlate = Convert.ToString(_ds.Tables[0].Rows[i]["governmentPlate"]);
                        model.Brand = _brandService.GetBrand(Convert.ToInt32(_ds.Tables[0].Rows[i]["brand"]));
                        model.YearOfRelease = Convert.ToInt32(_ds.Tables[0].Rows[i]["yearOfRelease"]);
                        model.CarType = _carTypeService.GetCarType(Convert.ToInt32(_ds.Tables[0].Rows[i]["typeCar"]));
                        model.CostOf1Day = Convert.ToDecimal(_ds.Tables[0].Rows[i]["costOf1Day"]);
                    }
                }
            }
            return model;
        }

        public void UpdateCar(CarModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@governmentPlate", model.GovernmentPlate);
                cmd.Parameters.AddWithValue("@brand", model.Brand.Id);
                cmd.Parameters.AddWithValue("@yearOfRelease", model.YearOfRelease);
                cmd.Parameters.AddWithValue("@typeCar", model.CarType.Id);
                cmd.Parameters.AddWithValue("@costOf1Day", model.CostOf1Day);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteCar(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteCar", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}