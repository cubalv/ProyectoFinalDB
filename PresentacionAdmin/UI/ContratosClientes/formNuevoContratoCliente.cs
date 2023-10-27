using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negociacion;
using PresentacionAdmin.UI.Servicios;

namespace PresentacionAdmin.UI.ContratosClientes
{
    public partial class formNuevoContratoCliente : Form
    {
        zonasNegocios Zonas = new zonasNegocios();
        clientesNegocios Clientes = new clientesNegocios();
        planNegocios Planes = new planNegocios();
        contratoClienteNegocios Contratos = new contratoClienteNegocios();
        public formNuevoContratoCliente()
        {
            InitializeComponent();
        }
        private void formNuevoContratoCliente_Load(object sender, EventArgs e)
        {
            try
            {
                gridControl1.DataSource = Clientes.dtClientes();
                gridControl2.DataSource = Planes.listadoPlanes(true);

                ddlDepto.DataSource = Zonas.listadoDeptos();
                ddlDepto.DisplayMember= "nombre";
                ddlDepto.ValueMember = "id";

                cbConfirmaInstalacion.Visible = true;
            }
            catch { }
        }
        private bool verificarDatosLLenos()
        {
            if(tbDpi.Text!="" && lblCodigoPlan.Text.StartsWith("label") && lblCodigoZona.Text.StartsWith("label"))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Por favor selecciones los datos necesaros", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void ddlDepto_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int codigoDepto = Convert.ToInt32(ddlDepto.SelectedValue.ToString());
                ddlMuni.DataSource = Zonas.listadoMunis(codigoDepto);
                ddlMuni.DisplayMember = "nombre";
                ddlMuni.ValueMember = "id";
            }
            catch { }
        }

        private void ddlMuni_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int codigoMuni = Convert.ToInt32(ddlMuni.SelectedValue.ToString());
                gridControl3.DataSource = Zonas.listadoZonas(codigoMuni);
            }
            catch { }
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            try
            {
                tbDpi.Text = gridView1.GetFocusedRowCellValue("dpi").ToString();

                DataTable dt = Clientes.datosCliente(tbDpi.Text);
                tbNombre.Text = dt.Rows[0][0].ToString();
                tbApellido.Text = dt.Rows[0][1].ToString();
                tbTel1.Text = dt.Rows[0][3].ToString();
                tbCorreo.Text = dt.Rows[0][5].ToString();
            }
            catch { }
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoPlan = Convert.ToInt32(gridView2.GetFocusedRowCellValue("codigo").ToString());
                lblCodigoPlan.Text = codigoPlan.ToString();

                DataTable dt = Planes.datosPlanes(codigoPlan);

                tbNombrePlan.Text = dt.Rows[0][1].ToString();
                tbDescripcionPlan.Text = dt.Rows[0][2].ToString();
                tbCostoMensual.Text = dt.Rows[0][3].ToString();
                tbCostoInstalacion.Text = dt.Rows[0][4].ToString();
                tbDuracionContrato.Text = dt.Rows[0][5].ToString();
                verificarDisponibilidad();
            }
            catch { }
        }

        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
        {
            try
            {

                int codigoZona = Convert.ToInt32(gridView3.GetFocusedRowCellValue("codigo").ToString());
                lblCodigoZona.Text = codigoZona.ToString();

                DataTable dt = Zonas.datosZona(codigoZona);

                tbNombreZona.Text = dt.Rows[0][1].ToString();
                verificarDisponibilidad();
            }
            catch { }

        }
        private void verificarDisponibilidad()
        {
            try
            {
                if (!lblCodigoPlan.Text.StartsWith("label") && !lblCodigoZona.Text.StartsWith("label"))
                {
                    int codigoPlan = Convert.ToInt32(lblCodigoPlan.Text);
                    int codigoZona = Convert.ToInt32(lblCodigoZona.Text);
                    if (Contratos.verificarDisponibilidadZona(codigoZona, codigoPlan))
                    {
                        lblDisponiblidad.Text = "Plan disponible en la zona";
                        iconoDisponilibidad.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
                        iconoDisponilibidad.IconColor = Color.Green;
                        btnProgramarInstalacion.Visible = false;
                        cbConfirmaInstalacion.Checked = false;
                    }
                    else
                    {
                        lblDisponiblidad.Text = "Plan no disponible en la zona";
                        iconoDisponilibidad.IconChar = FontAwesome.Sharp.IconChar.XmarkCircle;
                        iconoDisponilibidad.IconColor = Color.Maroon;
                        btnProgramarInstalacion.Visible = true;
                        cbConfirmaInstalacion.Checked = false;
                    }
                    panelAviso.Visible = true;
                }
            }
            catch { }

        }
        private void btnVerificarDispon_Click(object sender, EventArgs e)
        {
            try
            {
                cbConfirmaInstalacion.Checked = true;
            }
            catch { }
        }

        private void btnCompletar_Click(object sender, EventArgs e)
        {

        }
    }
}
