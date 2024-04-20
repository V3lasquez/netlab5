using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace netlab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS; Initial Catalog=NeptunoDB;User Id=userTecsup;Password=userTecsup03;TrustServerCertificate=true";

            try
            {
                // Obtener los valores de los TextBox
                string idCliente = textBoxIdCliente.Text;
                string nombreCompania = textBoxNombreCompania.Text;
                string nombreContacto = textBoxNombreContacto.Text;
                string cargoContacto = textBoxCargoContacto.Text;
                string direccion = textBoxDireccion.Text;
                string ciudad = textBoxCiudad.Text;
                string region = textBoxRegion.Text;
                string codPostal = textBoxCodPostal.Text;
                string pais = textBoxPais.Text;
                string telefono = textBoxTelefono.Text;
                string fax = textBoxFax.Text;

                // Cadena de conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Comando de TRANSACT SQL para insertar un nuevo cliente
                    string query = "INSERT INTO Clientes (idCliente, NombreCompañia, NombreContacto, CargoContacto, Direccion, Ciudad, Region, CodPostal, Pais, Telefono, Fax) VALUES (@IdCliente, @NombreCompañia, @NombreContacto, @CargoContacto, @Direccion, @Ciudad, @Region, @CodPostal, @Pais, @Telefono, @Fax)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Asignar parámetros
                        command.Parameters.AddWithValue("@IdCliente", idCliente);
                        command.Parameters.AddWithValue("@NombreCompañia", nombreCompania);
                        command.Parameters.AddWithValue("@NombreContacto", nombreContacto);
                        command.Parameters.AddWithValue("@CargoContacto", cargoContacto);
                        command.Parameters.AddWithValue("@Direccion", direccion);
                        command.Parameters.AddWithValue("@Ciudad", ciudad);
                        command.Parameters.AddWithValue("@Region", region);
                        command.Parameters.AddWithValue("@CodPostal", codPostal);
                        command.Parameters.AddWithValue("@Pais", pais);
                        command.Parameters.AddWithValue("@Telefono", telefono);
                        command.Parameters.AddWithValue("@Fax", fax);

                        // Ejecutar el comando
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cliente guardado exitosamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se pudo guardar el cliente.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void Listar_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS ;Initial Catalog=NeptunoDB;User Id=userTecsup;Password=userTecsup03;TrustServerCertificate=true";
            ;

            try
            {
                List<Clientes> clientes = new List<Clientes>();

                // Cadena de conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Comando de TRANSACT SQL
                    using (SqlCommand command = new SqlCommand("ListarClientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar la consulta
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string idCliente = reader.IsDBNull(reader.GetOrdinal("idCliente")) ? null : reader.GetString(reader.GetOrdinal("idCliente"));
                                    string nombreCompania = reader.IsDBNull(reader.GetOrdinal("NombreCompañia")) ? null : reader.GetString(reader.GetOrdinal("NombreCompañia"));
                                    string nombreContacto = reader.IsDBNull(reader.GetOrdinal("NombreContacto")) ? null : reader.GetString(reader.GetOrdinal("NombreContacto"));
                                    string cargoContacto = reader.IsDBNull(reader.GetOrdinal("CargoContacto")) ? null : reader.GetString(reader.GetOrdinal("CargoContacto"));
                                    string direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion"));
                                    string ciudad = reader.IsDBNull(reader.GetOrdinal("Ciudad")) ? null : reader.GetString(reader.GetOrdinal("Ciudad"));
                                    string region = reader.IsDBNull(reader.GetOrdinal("Region")) ? null : reader.GetString(reader.GetOrdinal("Region"));
                                    string codPostal = reader.IsDBNull(reader.GetOrdinal("CodPostal")) ? null : reader.GetString(reader.GetOrdinal("CodPostal"));
                                    string pais = reader.IsDBNull(reader.GetOrdinal("Pais")) ? null : reader.GetString(reader.GetOrdinal("Pais"));
                                    string telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono"));
                                    string fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? null : reader.GetString(reader.GetOrdinal("Fax"));
                                    bool activoInt = reader.IsDBNull(reader.GetOrdinal("Activo")) ? false : reader.GetBoolean(reader.GetOrdinal("Activo"));
                                    int activo = activoInt ? 1 : 0;


                                    clientes.Add(new Clientes
                                    {
                                        IdCliente = idCliente,
                                        NombreCompañia = nombreCompania,
                                        NombreContacto = nombreContacto,
                                        CargoContacto = cargoContacto,
                                        Direccion = direccion,
                                        Ciudad = ciudad,
                                        Region = region,
                                        CodPostal = codPostal,
                                        Pais = pais,
                                        Telefono = telefono,
                                        Fax = fax,
                                        Activo= activoInt
                                    });
                                }

                            }
                            else
                            {
                                Console.WriteLine("No se encontraron datos.");
                            }
                        }
                    }

                    // Asignar la lista de clientes al origen de datos de tu control (por ejemplo, un DataGridView)
                    dgvDemo.ItemsSource = clientes;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la recuperación o llenado de datos
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS ;Initial Catalog=NeptunoDB;User Id=userTecsup;Password=userTecsup03;TrustServerCertificate=true";
            ;
            string idCliente = textBoxIdCliente.Text;
            string nombreCompania = textBoxNombreCompania.Text;
            string nombreContacto = textBoxNombreContacto.Text;
            string cargoContacto = textBoxCargoContacto.Text;
            string direccion = textBoxDireccion.Text;
            string ciudad = textBoxCiudad.Text;
            string region = textBoxRegion.Text;
            string codPostal = textBoxCodPostal.Text;
            string pais = textBoxPais.Text;
            string telefono = textBoxTelefono.Text;
            string fax = textBoxFax.Text;
            bool activo = textBoxActivo.Text == "Activo";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    SqlCommand command = new SqlCommand("ActualizarCliente", connection);
                    command.CommandType = CommandType.StoredProcedure; // Especificar que es un procedimiento almacenado

                    // Asignar los valores de los parámetros del procedimiento almacenado
                    command.Parameters.AddWithValue("@idCliente", idCliente);
                    command.Parameters.AddWithValue("@NombreCompañia", nombreCompania);
                    command.Parameters.AddWithValue("@CargoContacto", cargoContacto);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@Ciudad", ciudad);
                    command.Parameters.AddWithValue("@Region", region);
                    command.Parameters.AddWithValue("@CodPostal", codPostal);
                    command.Parameters.AddWithValue("@Pais", pais);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Fax", fax);
                    command.Parameters.AddWithValue("@Activo", activo);

                    // Ejecutar el procedimiento almacenado
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Datos actualizados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message);
            }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS ;Initial Catalog=NeptunoDB;User Id=userTecsup;Password=userTecsup03;TrustServerCertificate=true";
            ;

            // Verificar si se ha seleccionado una fila
            if (dgvDemo.SelectedItem != null)
            {
                Clientes clienteSeleccionado = dgvDemo.SelectedItem as Clientes;
                if (clienteSeleccionado != null)
                {
                    try
                    {
                        string idClienteAEliminar = clienteSeleccionado.IdCliente;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "EXEC EliminarCliente @idCliente";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@idCliente", idClienteAEliminar);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Cliente eliminado correctamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se pudo eliminar el cliente.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el cliente: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("El elemento seleccionado no es un cliente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente para eliminar.");
            }
        }
    }
}