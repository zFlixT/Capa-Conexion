﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaConexion.Modelos;

namespace CapaConexion
{
    public partial class Form1 : Form
    {
        List<customers> Customers = new List<customers>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection
                ("Data Source=DESKTOP-O6CMB76\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30");
            MessageBox.Show("Conexion creada");
            conexion.Open();

            //------------------------

            String selectFrom = "";
            selectFrom = selectFrom + "SELECT " + "\n";
            selectFrom = selectFrom + "      [CompanyName] " + "\n";
            selectFrom = selectFrom + "      ,[ContactName] " + "\n";
            selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
            selectFrom = selectFrom + "      ,[Address] " + "\n";
            selectFrom = selectFrom + "      ,[City] " + "\n";
            selectFrom = selectFrom + "      ,[Region] " + "\n";
            selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
            selectFrom = selectFrom + "      ,[Country] " + "\n";
            selectFrom = selectFrom + "      ,[Phone] " + "\n";
            selectFrom = selectFrom + "      ,[Fax] " + "\n";
            selectFrom = selectFrom + "  FROM [dbo].[Customers]";

            //-----------------------

            SqlCommand comando = new SqlCommand(selectFrom, conexion);
            SqlDataReader reader = comando.ExecuteReader();



            while (reader.Read())
            {

                customers customers = new customers();
                customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
                customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
                customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
                customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
                customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
                customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
                customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
                customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
                customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
                customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];

                Customers.Add(customers);
            }
            dataGrid.DataSource = Customers;


            MessageBox.Show("Conexion cerrada");
            conexion.Close();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var filtro = Customers.FindAll(X => X.CompanyName.StartsWith(tbFiltro.Text));
            dataGrid.DataSource = filtro;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            DatosLayer.DataBase.ConnetionTimeout = 30;

            string cadenaConexion = DatosLayer.DataBase.ConnectionString;
            var conxion = DatosLayer.DataBase.GetSql();
        }
    }
}
