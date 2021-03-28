using CarRental.Models;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace CarRental.Services
{
    public class ClientService
    {
        private ClientService _clientService;
        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;

        public IList<ClientModel> GetClients()
        {
            _clientService = new ClientService();
            IList<ClientModel> list = new List<ClientModel>();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetClients", con);
                cmd.CommandType = CommandType.StoredProcedure;
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        ClientModel obj = new ClientModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        obj.Name = Convert.ToString(_ds.Tables[0].Rows[i]["name"]);
                        obj.Address = Convert.ToString(_ds.Tables[0].Rows[i]["address"]);
                        obj.PhoneNumber = Convert.ToString(_ds.Tables[0].Rows[i]["phoneNumber"]);
                        list.Add(obj);
                    }
                }
            }
            return list;
        }

        public void AddClient(ClientModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("AddClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", model.Name);
                cmd.Parameters.AddWithValue("@address", model.Address);
                cmd.Parameters.AddWithValue("@phoneNumber", model.PhoneNumber);
                cmd.ExecuteNonQuery();
            }
        }

        public ClientModel GetClient(int id)
        {
            _clientService = new ClientService();
            var model = new ClientModel();
            _ds = new DataSet();

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetClientById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        model.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        model.Name = Convert.ToString(_ds.Tables[0].Rows[i]["name"]);
                        model.Address = Convert.ToString(_ds.Tables[0].Rows[i]["address"]);
                        model.PhoneNumber = Convert.ToString(_ds.Tables[0].Rows[i]["phoneNumber"]);
                    }
                }
            }
            return model;
        }

        public void UpdateClient(ClientModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", model.Id);
                cmd.Parameters.AddWithValue("@name", model.Name);
                cmd.Parameters.AddWithValue("@address", model.Address);
                cmd.Parameters.AddWithValue("@phoneNumber", model.PhoneNumber);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteClient(int id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}