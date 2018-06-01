using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using belife.DALC;


namespace Belife.Vista
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static BeLifeEntities conector; //Generar como elemento estatico para acceder desde otra capa

        public MainWindow()
        {

            InitializeComponent();
            //Inicializando Cbx CLIENTE
            cbxSexo.Items.Add("Hombre");
            cbxSexo.Items.Add("Mujer");

            cbxEstadoCivil.Items.Add("Soltero");
            cbxEstadoCivil.Items.Add("Casado");
            cbxEstadoCivil.Items.Add("Divorciado");
            cbxEstadoCivil.Items.Add("Viudo");

            //Inicializando Cbx LISTAR CLIENTE
            cbxFiltraSexoCliente.Items.Add("Hombre");
            cbxFiltraSexoCliente.Items.Add("Mujer");

            //Inicializando Cbx plan
            cbxPlan.Items.Add("VID01");
            cbxPlan.Items.Add("VID02");
            cbxPlan.Items.Add("VID03");
            cbxPlan.Items.Add("VID04");
            cbxPlan.Items.Add("VID05");

            //Creando conector
            if (conector == null)
            {
                conector = new BeLifeEntities();

            }
        }

        private Boolean leer()
        {
            try
            {
                //Creo un nuevo cliente, que sea igual al primer resultado de Clientes(BdD)
                //Donde el rut sea igual a este rut
                Cliente cli = conector.Clientes.First(
                cliLeer => cliLeer.RutCliente == this.txtRutCliente.Text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private Boolean leerC()
        {
            try
            {
                //Creo un nuevo contrato, que sea igual al primer resultado de Contratos(BdD)
                //Donde el rut sea igual a este rut
                Contrato cont = conector.Contratoes.First(
                contLeer => contLeer.Numero == this.txtNumero.Text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente c = new Cliente();

                if (leer() == true)
                {
                    MessageBox.Show("Este cliente ya existe");
                }
                else
                {
                    c.RutCliente = txtRutCliente.Text;
                }


                c.Nombres = txtNombre.Text;
                c.Apellidos = txtApellidos.Text;
                //Validar que sea mayor a 18
                c.FechaNacimiento = dtpFechaDeNacimiento.Text;


                if (cbxSexo.SelectedItem.ToString() == "Hombre")
                {
                    c.IdSexo = 1;
                }
                else if (cbxSexo.SelectedItem.ToString() == "Mujer")
                {
                    c.IdSexo = 2;
                }
                else
                {
                    MessageBox.Show("Debe ingresar su sexo!");
                }


                if (cbxEstadoCivil.SelectedItem.ToString() == "Soltero")
                {
                    c.IdEstadoCivil = 1;
                }
                else if (cbxEstadoCivil.SelectedItem.ToString() == "Casado")
                {
                    c.IdEstadoCivil = 2;
                }
                else if (cbxEstadoCivil.SelectedItem.ToString() == "Divorciado")
                {
                    c.IdEstadoCivil = 3;
                }
                else if (cbxEstadoCivil.SelectedItem.ToString() == "Viudo")
                {
                    c.IdEstadoCivil = 4;
                }
                else
                {
                    MessageBox.Show("Debe agregar su estado civil");
                }
                conector.Clientes.Add(c);
                conector.SaveChanges();
                MessageBox.Show("se agrego");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cli = conector.Clientes.First(
                cliBorrar => cliBorrar.RutCliente == this.txtRutCliente.Text);

                conector.Clientes.Remove(cli);
                conector.SaveChanges();
                MessageBox.Show("Se elimino");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



      
        private void btnDesplegar_Click(object sender, RoutedEventArgs e)
        {
            //Actualiza el datagrid con la fuente de items de todos los clientes
            dgListaClientes.ItemsSource = null;
            dgListaClientes.ItemsSource = conector.Clientes.ToList();

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (leer() == true)
            {
                MessageBox.Show("Existe");


                Cliente cli = conector.Clientes.First(
                    cliMostrar => cliMostrar.RutCliente == this.txtRutCliente.Text);

                txtClientes.Text = cli.RutCliente + " " + cli.Nombres + " " + cli.Apellidos + " " + cli.Sexo;
            }
            else
            {
                MessageBox.Show("El usuario buscado no existe");
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (leer() == true)
            {

                Cliente cli = conector.Clientes.First(
                   cliMostrar => cliMostrar.RutCliente == this.txtRutCliente.Text);

                cli.Nombres = txtNombre.Text;
                cli.RutCliente = txtRutCliente.Text;
                cli.FechaNacimiento = dtpFechaDeNacimiento.SelectedDate.ToString();
                cli.Apellidos = txtApellidos.Text;
                if (cbxSexo.SelectedItem.ToString() == "Hombre")
                {
                    cli.IdSexo = 1;
                }
                else if (cbxSexo.SelectedItem.ToString() == "Mujer")
                {
                    cli.IdSexo = 2;
                }
                else
                {
                    MessageBox.Show("Debe ingresar su sexo!");
                }

                if (cbxEstadoCivil.SelectedItem.ToString() == "Soltero")
                {
                    cli.IdEstadoCivil = 1;
                }
                else if (cbxEstadoCivil.SelectedItem.ToString() == "Casado")
                {
                    cli.IdEstadoCivil = 2;
                }
                else if (cbxEstadoCivil.SelectedItem.ToString() == "Divorciado")
                {
                    cli.IdEstadoCivil = 3;
                }
                else if (cbxEstadoCivil.SelectedItem.ToString() == "Viudo")
                {
                    cli.IdEstadoCivil = 4;
                }

                MessageBox.Show("Usuario modificado");
            }
            else
            {
                MessageBox.Show("El usuario a modificar no existe");
            }
        }

        private void btnFiltraSexoCliente_Click(object sender, RoutedEventArgs e)
        {
            int ListaIdSexo = 0;


            if (cbxFiltraSexoCliente.SelectedItem.ToString() == "Hombre")
            {
                ListaIdSexo = 1;
            }
            else if (cbxFiltraSexoCliente.SelectedItem.ToString() == "Mujer")
            {
                ListaIdSexo = 2;
            }
            else
            {
                MessageBox.Show("Debe ingresar un sexo");
            }

            List<Cliente> cli = conector.Clientes.Where(
                   cliListar => cliListar.IdSexo == ListaIdSexo).ToList();
            dgListaClientes.ItemsSource = cli;
        }

        private void btnCAgregar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Contrato c = new Contrato();
                if (leerC() == true)
                {
                    MessageBox.Show("Este CONTRATO ya existe");
                }
                else
                {
                    c.Numero = txtNumero.Text;
                    c.FechaCreacion = dtpFechaCreacion.SelectedDate.Value;
                    c.RutCliente = txtRutClienteC.Text;
                    c.CodigoPlan = cbxPlan.SelectedItem.ToString();
                    c.FechaInicioVigencia = dtpFechaInicioVigencia.SelectedDate.Value;
                    c.FechaFinVigencia = dtpFechaTermino.SelectedDate.Value;
                    if (chkVigente.IsChecked == true)
                    {
                        c.Vigente = true;
                    }
                    else
                    {
                        c.Vigente = false;
                    }

                    if (chkDeclaracionSalud.IsChecked == true)
                    {
                        c.DeclaracionSalud = true;
                    }
                    else
                    {
                        c.DeclaracionSalud = false;
                    }

                    //Modificar
                    c.PrimaAnual = 1000;
                    c.PrimaMensual = 100;

                    c.Observaciones = txtObservaciones.Text;

                    Cliente cli = conector.Clientes.First(
                     cliente => cliente.RutCliente == this.txtRutClienteC.Text);

                    c.Cliente = cli;

                    Plan plan = conector.Plans.First(
                    planC => planC.IdPlan == "VID01");

                    conector.Contratoes.Add(c);
                    conector.SaveChanges();
                    MessageBox.Show("se agrego");

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

            }

        }


        private void btnCBuscar_Click(object sender, RoutedEventArgs e)
        {
            if(txtNumeroBuscar.Text.Length > 0)
            {
                try
                {
                    Contrato con = conector.Contratoes.First(
                    co => co.Numero == this.txtNumeroBuscar.Text);

                    lblDatosContrato.Content = "Numero" + con.Numero + " Rut cliente" + con.RutCliente;  
                }
                catch
                {
                    MessageBox.Show("El contrato buscado no existe");
                }
               
            }
            else
            {
                MessageBox.Show("Debe ingresar el numero del contrato");
            }
            
        }

        
        private void btnCModificar_Click(object sender, RoutedEventArgs e)
        {
            if(leerC() == true)
            {
                Contrato c = conector.Contratoes.First(
                    contr => contr.Numero == this.txtNumero.Text);
                c.Numero = txtNumero.Text;
                c.FechaCreacion = dtpFechaCreacion.SelectedDate.Value;
                c.RutCliente = txtRutClienteC.Text;
                c.CodigoPlan = cbxPlan.SelectedItem.ToString();
                c.FechaInicioVigencia = dtpFechaInicioVigencia.SelectedDate.Value;
                c.FechaFinVigencia = dtpFechaTermino.SelectedDate.Value;
                if (chkVigente.IsChecked == true)
                {
                    c.Vigente = true;
                }
                else
                {
                    c.Vigente = false;
                }

                if (chkDeclaracionSalud.IsChecked == true)
                {
                    c.DeclaracionSalud = true;
                }
                else
                {
                    c.DeclaracionSalud = false;
                }

                //Modificar
                c.PrimaAnual = 1000;
                c.PrimaMensual = 100;

                c.Observaciones = txtObservaciones.Text;

                Cliente cli = conector.Clientes.First(
                 cliente => cliente.RutCliente == this.txtRutClienteC.Text);

                c.Cliente = cli;

                Plan plan = conector.Plans.First(
                planC => planC.IdPlan == "VID01");

               
                MessageBox.Show("Contrato modificado");
            }
            else
            {
                MessageBox.Show("No se puede modificar ya que no existe, si desea crearlo, presione en Agregar ");
            }
        }

        private void btnCEliminar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Contrato cont = conector.Contratoes.First(
                contBorrar => contBorrar.Numero == this.txtNumero.Text);

                //conector.Contratoes.Remove(cont);
                bool vigente = false;
                object FechaFinVigencia = null;
                //   FechaFinVigencia = FechaFinVigencia.AddDays(-1);
                conector.SaveChanges();
                MessageBox.Show("El CONTRATO quedo NO VIGENTE");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDesplegarContratos_Click(object sender, RoutedEventArgs e)
        {
            dgListaContratos.ItemsSource = null;
            dgListaContratos.ItemsSource = conector.Contratoes.ToList();
        }
    }
}