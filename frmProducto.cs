using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial
{
    public partial class frmProducto : Form
    {
        string cadenaConexion = "server=localhost\\SQLEXPRESS; database=Parcial; Integrated security=true";
        public frmProducto()
        {
            InitializeComponent();
        }

        private void iniciarFormulario(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            
           dgvListado.Rows.Clear();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var sql = " SELECT p.Nombre,p.Marca,c.Nombre,p.Precio " +
                    " FROM Categoria c " +
                    " INNER JOIN  Producto p ON p.IdCategoria = c.IdCategoria ";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                dgvListado.Rows.Add(lector[0], lector[1], lector[2],lector[3]);
                            }
                        }
                    }
                }
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            var frm = new frmProductoEdit();
            if (frm.ShowDialog()== DialogResult.OK)
            {
                var nombre = ((TextBox)frm.Controls["txtNombre"]).Text;
                var marca = ((TextBox)frm.Controls["txtMarca"]).Text;
                var categoria = ((ComboBox)frm.Controls["cboCategoria"]).SelectedValue.ToString();
                var stock = ((TextBox)frm.Controls["txtStock"]).Text;
                var precio = ((TextBox)frm.Controls["txtPrecio"]).Text;

                using (var conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    var sql = "INSERT INTO Producto (Nombre ,Marca,IdCategoria, Stock,Precio)"+
                        "VALUES(@nombre, @marca, @categoria, @stock, @precio)";

                    using (var comando = new SqlCommand(sql, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@marca", marca);
                        comando.Parameters.AddWithValue("@categoria", categoria);
                        comando.Parameters.AddWithValue("@stock", stock);
                        comando.Parameters.AddWithValue("@precio", precio);
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            MessageBox.Show("El cliente ha sido registrado", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cargarDatos();
                        }
                        else
                        {
                            MessageBox.Show("El proceso de creación del cliente ha fallado.", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            
            
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            string eliminar = "DELETE FROM Producto WHERE Nombre = @nombre";

            if (dgvListado.executeCommand(eliminar))
            {

            }
        }
    }
}
