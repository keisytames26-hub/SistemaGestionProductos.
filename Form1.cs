using System;
using System.Windows.Forms;
using SistemaGestionProductos.Datos;
using SistemaGestionProductos.Logica;
using SistemaGestionProductos.Modelos;
using Tarea_ProgramacionII_SistemaGestionProductos.Almacenamiento;
using System.Linq;
using System.Collections.Generic;
using Tarea_ProgramacionII_SistemaGestionProductos.Formularios;

namespace Tarea_ProgramacionII_SistemaGestionProductos

{
    public partial class Form1 : Form
    {

        private CategoriaLogica categoriaLogica = new CategoriaLogica();
        private ProductoLogica productoLogica = new ProductoLogica();
        private int idProductoSeleccionado = 0;
        private bool productoSeleccionadoDesdeCatalogo = false;
        private ProductoSeleccionadoArchivo almacenamientoSeleccionados =
    new ProductoSeleccionadoArchivo();

        private void CargarCategorias()
            {

            try
            {
                var categorias = categoriaLogica.Listar();

                
                cbxCategoria.DataSource = null;
                cbxCategoria.Items.Clear();

                cbxCategoria.DataSource = categorias;
                cbxCategoria.DisplayMember = "Nombre";
                cbxCategoria.ValueMember = "IdCategoria";
                cbxCategoria.SelectedIndex = -1;
               
                cbxFiltrarPorCategoria.DataSource = null;

                var categoriasFiltro = new List<Categoria>();
                categoriasFiltro.Add(new Categoria
                {
                    IdCategoria = 0,
                    Nombre = "Todas las categorías"
                });

                categoriasFiltro.AddRange(categorias);

                cbxFiltrarPorCategoria.DataSource = categoriasFiltro;
                cbxFiltrarPorCategoria.DisplayMember = "Nombre";
                cbxFiltrarPorCategoria.ValueMember = "IdCategoria";
                cbxFiltrarPorCategoria.SelectedIndex = 0;
                dgvCatalogoCategorias.AutoGenerateColumns = false;
                dgvCatalogoCategorias.Columns["ColIDCategorias"].DataPropertyName = "IdCategoria";
                dgvCatalogoCategorias.Columns["ColNombreCategorias"].DataPropertyName = "Nombre";
                dgvCatalogoCategorias.Columns["ColDescripcionCategorias"].DataPropertyName = "Descripcion";

                dgvCatalogoCategorias.DataSource = categorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las categorías: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
        private void CargarProductos()
        {
            try
            {
                tgvCatalogo.AutoGenerateColumns = false;

                tgvCatalogo.Columns["ColID"].DataPropertyName = "IdProducto";

                tgvCatalogo.Columns["ColNombre"].DataPropertyName = "Nombre";

                tgvCatalogo.Columns["ColCategoria"].DataPropertyName = "NombreCategoria";

                tgvCatalogo.Columns["ColPrecio"].DataPropertyName = "Precio";

                tgvCatalogo.Columns["ColStock"].DataPropertyName = "Stock";

                tgvCatalogo.Columns["ColFechaRegistro"].DataPropertyName = "FechaRegistro";

                tgvCatalogo.DataSource = productoLogica.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar los productos: " + ex.Message,
                    "Error al cargar productos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void ActualizarIndicadoresInventario()
        {
            try
            {
                var productos = productoLogica.Listar();

                int totalProductos = 0;
                decimal valorInventario = 0;

                foreach (Producto producto in productos)
                {
                    totalProductos += producto.Stock;

                    valorInventario += producto.Precio * producto.Stock;
                }

                txtTotalProductos.Text = totalProductos.ToString();

                txtValorInventario.Text = valorInventario.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al calcular los indicadores del inventario: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void CargarProductosSeleccionados()
        {
            try
            {
                dgvProductosSeleccionados.Rows.Clear();

                var productos = almacenamientoSeleccionados.Cargar();

                foreach (Producto producto in productos)
                {
                    dgvProductosSeleccionados.Rows.Add(
                        producto.IdProducto,
                        producto.Nombre,
                        producto.NombreCategoria,
                        producto.Precio,
                        producto.Stock,
                        producto.CantidadSeleccionada,
                        producto.SubtotalSeleccionado
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron recuperar los productos seleccionados: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void GuardarProductosSeleccionados()
        {
            List<Producto> productos = new List<Producto>();

            foreach (DataGridViewRow fila in dgvProductosSeleccionados.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                Producto producto = new Producto();

                producto.IdProducto = Convert.ToInt32(fila.Cells["ColIDSelec"].Value);
                producto.Nombre = fila.Cells["ColNombreSelec"].Value.ToString();
                producto.NombreCategoria = fila.Cells["ColCategoriaSelec"].Value.ToString();
                producto.Precio = Convert.ToDecimal(fila.Cells["ColPrecioSelec"].Value);
                producto.Stock = Convert.ToInt32(fila.Cells["ColStockSelec"].Value);
                producto.CantidadSeleccionada = Convert.ToInt32(fila.Cells["ColCantidadSelec"].Value);

                productos.Add(producto);
            }

            almacenamientoSeleccionados.Guardar(productos);
        }

        private void tgvCatalogo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow fila = tgvCatalogo.Rows[e.RowIndex];

                idProductoSeleccionado = Convert.ToInt32(
                    fila.Cells["ColID"].Value
                );
                productoSeleccionadoDesdeCatalogo = true;

                txtID.Text = fila.Cells["ColID"].Value.ToString();
                txtNombreProducto.Text = fila.Cells["ColNombre"].Value.ToString();
                string nombreCategoria = fila.Cells["ColCategoria"].Value.ToString();

                foreach (Categoria categoria in cbxCategoria.Items)
                {
                    if (categoria.Nombre == nombreCategoria)
                    {
                        cbxCategoria.SelectedValue = categoria.IdCategoria;
                        break;
                    }
                }
                txtPrecio.Text = fila.Cells["ColPrecio"].Value.ToString();
                txtStock.Text = fila.Cells["ColStock"].Value.ToString();

                txtCantidadSeleccionada.Text = "0";

                errorProvider1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los datos del producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        


        private bool ValidarCantidadSeleccionada(out int cantidad)
        {
            cantidad = 0;

            
            errorProvider1.SetError(txtCantidadSeleccionada, "");

            
            if (!int.TryParse(txtCantidadSeleccionada.Text.Trim(), out cantidad))
            {
                errorProvider1.SetError(
                    txtCantidadSeleccionada,
                    "Ingrese una cantidad válida."
                );

                return false;
            }

            
            int stock;

            if (!int.TryParse(txtStock.Text.Trim(), out stock))
            {
                errorProvider1.SetError(
                    txtStock,
                    "Ingrese un stock válido."
                );

                return false;
            }

            
            try
            {
                productoLogica.ValidarCantidadSeleccionada(cantidad, stock);
                return true;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(txtCantidadSeleccionada, ex.Message);
                return false;
            }
        }


        private bool ValidarProducto()
        {
            bool valido = true;

            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text))
            {
                errorProvider1.SetError(
                    txtNombreProducto,
                    "Debe ingresar el nombre del producto."
                );

                valido = false;
            }

            
            decimal precio;

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                errorProvider1.SetError(
                    txtPrecio,
                    "Debe ingresar el precio."
                );

                valido = false;
            }
            else if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                errorProvider1.SetError(
                    txtPrecio,
                    "El precio debe ser un número válido."
                );

                valido = false;
            }
            else if (precio <= 0)
            {
                errorProvider1.SetError(
                    txtPrecio,
                    "El precio debe ser mayor que cero."
                );

                valido = false;
            }

         
            int stock;

            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                errorProvider1.SetError(
                    txtStock,
                    "Debe ingresar la cantidad disponible."
                );

                valido = false;
            }
            else if (!int.TryParse(txtStock.Text, out stock))
            {
                errorProvider1.SetError(
                    txtStock,
                    "El stock debe ser un número entero."
                );

                valido = false;
            }
            else if (stock < 0)
            {
                errorProvider1.SetError(
                    txtStock,
                    "El stock no puede ser negativo."
                );

                valido = false;
            }

           
            if (cbxCategoria.SelectedIndex == -1)
            {
                errorProvider1.SetError(
                    cbxCategoria,
                    "Debe seleccionar una categoría."
                );

                valido = false;
            }

            return valido;
        }

        
        public Form1()
        {
            InitializeComponent();

            tgvCatalogo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tgvCatalogo.MultiSelect = false;
            tgvCatalogo.ReadOnly = true;

            dgvProductosSeleccionados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductosSeleccionados.MultiSelect = false;
            dgvProductosSeleccionados.ReadOnly = true;

            CargarCategorias();
            CargarProductos();
            ActualizarIndicadoresInventario();
            CargarProductosSeleccionados();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {

            using (FrmSeleccionAgregar ventana = new FrmSeleccionAgregar())
            {
                DialogResult resultado = ventana.ShowDialog();

                if (resultado != DialogResult.OK)
                    return;

                if (ventana.OpcionSeleccionada == "Catalogo")
                {
                    AgregarAlCatalogo();
                }
                else if (ventana.OpcionSeleccionada == "Seleccionados")
                {
                    AgregarASeleccionados();
                }
            }
            ActualizarIndicadoresInventario();
        }

        private void AgregarAlCatalogo()
        {
            try
            {
                if (!ValidarProducto())
                {
                    MessageBox.Show(
                        "Por favor, corrija los campos señalados.",
                        "Validación de datos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                Producto producto = new Producto();

                producto.Nombre = txtNombreProducto.Text.Trim();
                producto.Precio = decimal.Parse(txtPrecio.Text);
                producto.Stock = int.Parse(txtStock.Text);
                producto.IdCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);

                producto.IdProducto = productoLogica.Agregar(producto);

                CargarProductos();

                MessageBox.Show(
                    "El producto se agregó correctamente al catálogo.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LimpiarCamposProducto();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo agregar el producto al catálogo: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void AgregarASeleccionados()
        {
            try
            {
               

                if (tgvCatalogo.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Primero debe seleccionar un producto del catálogo.",
                        "Seleccionar producto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }


                int cantidad;

                if (!ValidarCantidadSeleccionada(out cantidad))
                {
                    MessageBox.Show(
                        "Revise la cantidad seleccionada.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }


                DataGridViewRow filaProducto = tgvCatalogo.SelectedRows[0];

                int idProductoSeleccionadoActual = Convert.ToInt32(
                    filaProducto.Cells["ColID"].Value
                );

                if (filaProducto == null)
                {
                    MessageBox.Show(
                        "No se encontró el producto en el catálogo.",
                        "Producto no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                int id = idProductoSeleccionadoActual;
                string nombre = filaProducto.Cells["ColNombre"].Value.ToString();
                string categoria = filaProducto.Cells["ColCategoria"].Value.ToString();
                decimal precio = Convert.ToDecimal(filaProducto.Cells["ColPrecio"].Value);
                int stock = Convert.ToInt32(filaProducto.Cells["ColStock"].Value);

                
                DataGridViewRow filaExistente = null;

                foreach (DataGridViewRow fila in dgvProductosSeleccionados.Rows)
                {
                    if (!fila.IsNewRow &&
                        Convert.ToInt32(fila.Cells["ColIDSelec"].Value) == id)
                    {
                        filaExistente = fila;
                        break;
                    }
                }

                
                if (filaExistente != null)
                {
                    MessageBox.Show(
                        "Este producto ya está en la tabla de seleccionados.",
                        "Producto existente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                
                dgvProductosSeleccionados.Rows.Add(
                    id,
                    nombre,
                    categoria,
                    precio,
                    stock,
                    cantidad,
                    precio * cantidad
                );

             

                GuardarProductosSeleccionados();

                MessageBox.Show(
                    "El producto se agregó a productos seleccionados correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
              

                LimpiarCamposProducto();
                idProductoSeleccionado = 0;
                productoSeleccionadoDesdeCatalogo = false;
                tgvCatalogo.ClearSelection();
            }

            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo agregar el producto a seleccionados: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                if (idProductoSeleccionado <= 0)
                {
                    MessageBox.Show(
                        "Seleccione un producto de alguna de las tablas.",
                        "Eliminar producto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este producto?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                
                if (respuesta != DialogResult.Yes)
                    return;

                productoLogica.Eliminar(idProductoSeleccionado);

                for (int i = dgvProductosSeleccionados.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow fila = dgvProductosSeleccionados.Rows[i];

                    if (!fila.IsNewRow &&
                        Convert.ToInt32(fila.Cells["ColIDSelec"].Value) == idProductoSeleccionado)
                    {
                        dgvProductosSeleccionados.Rows.RemoveAt(i);
                    }
                }
                GuardarProductosSeleccionados();

                MessageBox.Show(
                    "El producto se eliminó correctamente.",
                    "Eliminación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                CargarProductos();
                ActualizarIndicadoresInventario();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar el producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
           
            try
            {
                
                if (idProductoSeleccionado <= 0)
                {
                    MessageBox.Show(
                        "Seleccione un producto del catálogo.",
                        "Modificar producto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                
                if (!ValidarProducto())
                {
                    MessageBox.Show(
                        "Revise los datos señalados antes de modificar.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                Producto producto = new Producto();

                producto.IdProducto = idProductoSeleccionado;
                producto.Nombre = txtNombreProducto.Text.Trim();
                producto.Precio = decimal.Parse(txtPrecio.Text);
                producto.Stock = int.Parse(txtStock.Text);
                producto.IdCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
                producto.NombreCategoria = cbxCategoria.Text;

                DataGridViewRow filaSeleccionada = null;
                int cantidadSeleccionada = 0;


                foreach (DataGridViewRow fila in dgvProductosSeleccionados.Rows)
                {
                    if (fila.IsNewRow)
                        continue;

                    if (Convert.ToInt32(fila.Cells["ColIDSelec"].Value)
                        == producto.IdProducto)
                    {
                        filaSeleccionada = fila;

                        cantidadSeleccionada = Convert.ToInt32(
                            fila.Cells["ColCantidadSelec"].Value
                        );

                        break;
                    }
                }

                if (cantidadSeleccionada > producto.Stock)
                {
                    MessageBox.Show(
                        "El nuevo stock no puede ser menor que la cantidad seleccionada.",
                        "Stock insuficiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                productoLogica.Modificar(producto);

               
                if (filaSeleccionada != null)
                {
                    filaSeleccionada.Cells["ColNombreSelec"].Value = producto.Nombre;
                    filaSeleccionada.Cells["ColCategoriaSelec"].Value = producto.NombreCategoria;
                    filaSeleccionada.Cells["ColPrecioSelec"].Value = producto.Precio;
                    filaSeleccionada.Cells["ColStockSelec"].Value = producto.Stock;

                   
                    filaSeleccionada.Cells["ColSubtotalSelec"].Value =
                        producto.Precio * cantidadSeleccionada;
                }
                GuardarProductosSeleccionados();


                CargarProductos();
                ActualizarIndicadoresInventario();


                MessageBox.Show(
                    "El producto se modificó correctamente.",
                    "Modificación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo modificar el producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        
        private void dgvProductosSeleccionados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex >= 0)
            {
                idProductoSeleccionado = Convert.ToInt32(
                    dgvProductosSeleccionados.Rows[e.RowIndex].Cells["ColIDSelec"].Value
                );
                productoSeleccionadoDesdeCatalogo = false;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
          
            LimpiarCamposProducto();

            idProductoSeleccionado = 0;

            tgvCatalogo.ClearSelection();

            MessageBox.Show(
                "Los campos de datos del producto se limpiaron correctamente.",
                "Formulario limpio",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        

        private void LimpiarCamposProducto()
        {
            txtID.Clear();
            txtNombreProducto.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtCantidadSeleccionada.Clear();

            cbxCategoria.SelectedIndex = -1;

            errorProvider1.Clear();

            txtNombreProducto.Focus();
        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            
            try
            {
                Categoria categoria = new Categoria();

                categoria.Nombre = txtNombreCategoria.Text.Trim();
                categoria.Descripcion = txtDescripcion.Text.Trim();

                categoriaLogica.Agregar(categoria);

                CargarCategorias();

                LimpiarCamposCategoria();

                MessageBox.Show(
                    "La categoría se agregó correctamente.",
                    "Registro exitoso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo agregar la categoría: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private void LimpiarCamposCategoria()
        {
            txtIDcategoria.Clear();
            txtNombreCategoria.Clear();
            txtDescripcion.Clear();

            txtNombreCategoria.Focus();
        }

        private void dgvCatalogoCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex < 0)
                return;

            try
            {
                DataGridViewRow fila = dgvCatalogoCategorias.Rows[e.RowIndex];

                txtIDcategoria.Text = fila.Cells["ColIDCategorias"].Value.ToString();
                txtNombreCategoria.Text = fila.Cells["ColNombreCategorias"].Value.ToString();
                txtDescripcion.Text = fila.Cells["ColDescripcionCategorias"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los datos de la categoría: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnModificarCategoria_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(txtIDcategoria.Text))
                {
                    MessageBox.Show(
                        "Primero debe seleccionar una categoría de la tabla.",
                        "Modificar categoría",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                Categoria categoria = new Categoria();

                categoria.IdCategoria = Convert.ToInt32(txtIDcategoria.Text);
                categoria.Nombre = txtNombreCategoria.Text.Trim();
                categoria.Descripcion = txtDescripcion.Text.Trim();

                categoriaLogica.Modificar(categoria);

                CargarCategorias();

                LimpiarCamposCategoria();

                MessageBox.Show(
                    "La categoría se modificó correctamente.",
                    "Modificación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo modificar la categoría: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnEliminarCategoria_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (string.IsNullOrWhiteSpace(txtIDcategoria.Text))
                {
                    MessageBox.Show(
                        "Primero debe seleccionar una categoría de la tabla.",
                        "Eliminar categoría",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                int idCategoria = Convert.ToInt32(txtIDcategoria.Text);

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea eliminar esta categoría?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta != DialogResult.Yes)
                    return;

                categoriaLogica.Eliminar(idCategoria);

                CargarCategorias();

                LimpiarCamposCategoria();

                MessageBox.Show(
                    "La categoría se eliminó correctamente.",
                    "Eliminación exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar la categoría: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnLimpiarCategoria_Click(object sender, EventArgs e)
        {
           
            LimpiarCamposCategoria();

            MessageBox.Show(
                "Los campos se limpiaron correctamente.",
                "Limpiar categoría",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        private bool ValidarCamposCategoria()
        {
            bool valido = true;

            errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
            {
                errorProvider1.SetError(
                    txtNombreCategoria,
                    "Debe ingresar el nombre de la categoría."
                );
                valido = false;
            }
            else if (txtNombreCategoria.Text.Trim().Length > 100)
            {
                errorProvider1.SetError(
                    txtNombreCategoria,
                    "El nombre no puede superar los 100 caracteres."
                );
                valido = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                errorProvider1.SetError(
                    txtDescripcion,
                    "Debe ingresar la descripción de la categoría."
                );
                valido = false;
            }
            else if (txtDescripcion.Text.Trim().Length > 200)
            {
                errorProvider1.SetError(
                    txtDescripcion,
                    "La descripción no puede superar los 200 caracteres."
                );
                valido = false;
            }

            return valido;
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
           
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtBuscarProducto.Text))
                {
                    MessageBox.Show(
                        "Ingrese el ID del producto que desea buscar.",
                        "Buscar producto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                
                if (!int.TryParse(txtBuscarProducto.Text.Trim(), out int idBuscado))
                {
                    MessageBox.Show(
                        "El ID debe ser un número entero.",
                        "Dato incorrecto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                bool encontrado = false;

                
                foreach (DataGridViewRow fila in tgvCatalogo.Rows)
                {
                    if (fila.IsNewRow)
                        continue;

                    int idProducto = Convert.ToInt32(fila.Cells["ColID"].Value);

                    if (idProducto == idBuscado)
                    {
                        
                        tgvCatalogo.ClearSelection();
                        fila.Selected = true;
                        tgvCatalogo.CurrentCell = fila.Cells["ColID"];

                        
                        tgvCatalogo_CellClick(
                            tgvCatalogo,
                            new DataGridViewCellEventArgs(0, fila.Index)
                        );

                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    MessageBox.Show(
                        "No se encontró ningún producto con ese ID.",
                        "Producto no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al buscar el producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnFiltrarCategoria_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (cbxFiltrarPorCategoria.SelectedValue == null)
                {
                    MessageBox.Show(
                        "Seleccione una categoría para filtrar.",
                        "Filtrar productos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                int idCategoria = Convert.ToInt32(
                    cbxFiltrarPorCategoria.SelectedValue
                );

                if (idCategoria == 0)
                {
                    CargarProductos();
                    return;
                }

                var productos = productoLogica.Listar();

                var productosFiltrados = productos
                    .Where(p => p.IdCategoria == idCategoria)
                    .ToList();

                tgvCatalogo.DataSource = productosFiltrados;

                if (productosFiltrados.Count == 0)
                {
                    MessageBox.Show(
                        "No hay productos registrados en esta categoría.",
                        "Sin resultados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al filtrar los productos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de que desea salir del sistema?",
                "Cerrar aplicación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}












