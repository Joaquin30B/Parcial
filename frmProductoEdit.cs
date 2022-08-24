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
    public partial class frmProductoEdit : Form
    {
        string cadenaConexion = "server=localhost\\SQLEXPRESS; database=Parcial; Integrated security=true";

        public frmProductoEdit()
        {
            InitializeComponent();
        }

        private void frmProductoEdit_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                // CARGAR DATOS 
                var sql = "SELECT * FROM Producto";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            Dictionary<string, string> ProductoSource = new Dictionary<string, string>();
                            while (lector.Read())
                            {
                                ProductoSource.Add(lector[0].ToString(), lector[1].ToString());
                            }
                        }
                    }
                }
                // CARGAR DATOS 
                sql = "SELECT * FROM Categoria";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            Dictionary<string, string> CategoriaSource = new Dictionary<string, string>();
                            while (lector.Read())
                            {
                                CategoriaSource.Add(lector[0].ToString(), lector[1].ToString());
                            }
                            cboCategoria.DataSource = new BindingSource(CategoriaSource, null);
                            cboCategoria.DisplayMember = "Value";
                            cboCategoria.ValueMember = "Key";
                        }
                    }
                }
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            decimal precio;
            int stock;
            precio = decimal.Parse(txtPrecio.Text);
            stock = int.Parse(txtStock.Text);

            if (precio > 2500)
            {
                MessageBox.Show("El precio es mayor a S/ 2500.", "Sistemas",
               MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }

            if (stock < 5)
            {
                MessageBox.Show("El stock debe ser mayor a 5", "Sistemas",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
