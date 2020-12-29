using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventoRegistro.forms
{
    public partial class frmCliente_Insertar : Form
    {
        public frmCliente_Insertar()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            clsCliente nuevoCliente=null;
            try
            {
                nuevoCliente = new clsCliente(0, txtNombres.Text, txtApellidoPaterno.Text, txtApellidoMaterno.Text,
                                            'M', cmbEstadoCivil.SelectedItem.ToString(), 'S', dtpFechaNacimiento.Value,
                                            (short)nudPuntosBono.Value, (byte)nudCantidadHijos.Value, chbEsClienteRecomendado.Checked);
                if (rbFemenino.Checked) nuevoCliente.sexo = 'F';
                if (rbOro.Checked) nuevoCliente.tipo = 'O'; else if (rbPlata.Checked) nuevoCliente.tipo = 'P';
                nuevoCliente.correoElectronico = txtCorreoElectronico.Text;
                //nuevoCliente.Insertar();
            }
            catch (ArgumentNullException error)
            {
                MessageBox.Show(error.Message, "Datos no completados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombres.SelectAll(); txtNombres.Focus();
                return;
            }
            catch (ArgumentOutOfRangeException error)
            {
                MessageBox.Show(error.Message, "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombres.SelectAll(); txtNombres.Focus();
                return;
            }
            catch (DataException error)
            {
                MessageBox.Show(error.Message, "Error en la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombres.SelectAll(); txtNombres.Focus();
                return;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombres.SelectAll();txtNombres.Focus();
                return;
            }            
            MessageBox.Show("Cliente registrado satisfactoriamente.", "Confirmación de registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtNombres.SelectAll();txtNombres.Focus();

            
        }

        private void frmCliente_Insertar_Load(object sender, EventArgs e)
        {            
            dtpFechaNacimiento.MaxDate = new DateTime(System.DateTime.Now.AddYears(-25).Year,
                                                        System.DateTime.Now.Month,
                                                        System.DateTime.Now.Day);
            btnNuevo_Click(sender, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombres.Clear();txtApellidoPaterno.Clear();txtApellidoMaterno.Clear();
            rbMasculino.Checked = true; cmbEstadoCivil.SelectedIndex = 0;
            dtpFechaNacimiento.Value = new DateTime(System.DateTime.Now.AddYears(-25).Year, 1, 1);
            rbEstandar.Checked = true;txtCorreoElectronico.Clear();
            nudPuntosBono.Value = 0;nudCantidadHijos.Value = 0;
            chbEsClienteRecomendado.Checked = false;
            txtNombres.Focus();
        }
    }
}
