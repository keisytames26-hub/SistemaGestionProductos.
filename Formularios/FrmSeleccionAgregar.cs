using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tarea_ProgramacionII_SistemaGestionProductos.Formularios
{
    public partial class FrmSeleccionAgregar : Form
    {
        public string OpcionSeleccionada { get; private set; }

        public FrmSeleccionAgregar()
        {
            InitializeComponent();
        }

        private void btnCatalogo_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada = "Catalogo";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSeleccionados_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada = "Seleccionados";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCanselar_Click(object sender, EventArgs e)
        {
            OpcionSeleccionada = "";
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
