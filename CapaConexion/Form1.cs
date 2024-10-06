using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; 
using DatosLayer;
using System.Reflection;

namespace CapaConexion
{
    public partial class Form1 : Form
    {
        // Instancia del repositorio de clientes para interactuar con la base de datos
        CustomerRepository customerRepository = new CustomerRepository();

        public Form1()
        {
            InitializeComponent(); // Inicializa los componentes visuales del formulario
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            // Llama al método ObtenerTodos del repositorio de clientes
            var Customers = customerRepository.ObtenerTodos();

            // Asigna la lista de clientes al DataSource del DataGridView
            dataGrid.DataSource = Customers;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // filtro para buscar clientes por nombre de la empresa
            // var filtro = customers.FindAll(X => X.CompanyName.StartsWith(tbFiltro.Text));
            // dataGrid.DataSource = filtro;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'northwindDataSet.Customers' Puede moverla o quitarla según sea necesario.
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);
            // Configura el nombre de la aplicación y el tiempo de espera de la conexión a la base de datos
            DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            DatosLayer.DataBase.ConnetionTimeout = 30;

            // Obtiene la cadena de conexión desde la capa de datos
            string cadenaConexion = DatosLayer.DataBase.ConnectionString;

            // Realiza una conexión con la base de datos mediante la capa de datos
            var conxion = DatosLayer.DataBase.GetSql();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Llama al método ObtenerPorID pasando el valor ingresado en el TextBox
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text);

            tboxCustomerID.Text = cliente.CustomerID;
            tboxCompanyName.Text = cliente.CompanyName;
            tboxContactName.Text = cliente.ContactName;
            tboxContactTitle.Text = cliente.ContactTitle;
            tboxAddress.Text = cliente.Address;
            tboxCity.Text = cliente.City;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            var resultado = 0;
            ;

            var nuevoCliente = ObtenerNuevoCliente();


            // hayNull= validarCampoNull(nuevoCliente) ? true : false ;

            /*  if (tboxCustomerID.Text != "" || 
                  tboxCompanyName.Text !="" ||
                  tboxContacName.Text != "" ||
                  tboxContacName.Text != "" ||
                  tboxAddress.Text != ""    ||
                  tboxCity.Text != "")
              {
                  resultado = customerRepository.InsertarCliente(nuevoCliente);
                  MessageBox.Show("Guardado" + "Filas modificadas = " + resultado);
              }
              else {
                  MessageBox.Show("Debe completar los campos por favor");
              }

              */

            /*
            if (nuevoCliente.CustomerID == "") {
                MessageBox.Show("El Id en el usuario debe de completarse");
               return;    
            }

            if (nuevoCliente.ContactName == "")
            {
                MessageBox.Show("El nombre de usuario debe de completarse");
                return;
            }
            
            if (nuevoCliente.ContactTitle == "")
            {
                MessageBox.Show("El contacto de usuario debe de completarse");
                return;
            }
            if (nuevoCliente.Address == "")
            {
                MessageBox.Show("la direccion de usuario debe de completarse");
                return;
            }
            if (nuevoCliente.City == "")
            {
                MessageBox.Show("La ciudad de usuario debe de completarse");
                return;
            }

            */

            if (validarCampoNull(nuevoCliente) == false)
            {
                resultado = customerRepository.InsertarCliente(nuevoCliente);
                MessageBox.Show("Guardado" + "Filas modificadas = " + resultado);
            }
            else
            {
                MessageBox.Show("Debe completar los campos por favor");
            }
        }
        public Boolean validarCampoNull(Object objeto)
        {
            foreach (PropertyInfo property in objeto.GetType().GetProperties())
            {
                object value = property.GetValue(objeto, null);
                if ((string)value == "")
                {
                    return true;
                }
            }
            return false;
        }


        private customers ObtenerNuevoCliente()
        {

            var nuevoCliente = new customers
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContactName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };

            return nuevoCliente;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var actualizarCliente = ObtenerNuevoCliente();
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente);
            MessageBox.Show($"Filas actualizadas = {actualizadas}");
        }
    }
}
