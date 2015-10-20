using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace SistemaCelulares
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "[+] Esperando credenciales de ingreso.";
            textBox1.BackColor = Color.LightGreen;
            textBox2.BackColor = Color.LightGreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string passw = textBox2.Text;
            bool encontrado = buscar_usuario(nombre, passw);

            if (!encontrado)
                MessageBox.Show("Error de usuario o password");
        }

        private bool buscar_usuario(string nombre, string passw)
        {
            bool validado = false;
            if (nombre.Length <= 3 || passw.Length <= 3)
            {
                MessageBox.Show("Error los campos estan vacios o muy cortos!");
                textBox1.Focus();
                textBox1.SelectAll();
                validado = false;
            }
            else
            {
                try
                {
                    string myConnection = "datasource=localhost;port=3306;user=root;password=Darkgory13";
                    MySqlConnection myCon = new MySqlConnection(myConnection);
                    MySqlCommand command = new MySqlCommand("SELECT * FROM sistema.usuarios WHERE usuario='"+ this.textBox1.Text +"' AND password='"+ this.textBox2.Text +"';", myCon);
                    MySqlDataReader reader;
                    myCon.Open();
                    reader = command.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count = count + 1;
                    }

                    if (count == 1)
                    {
                        MessageBox.Show("Bienvenido!");
                        toolStripStatusLabel1.Text = "[V] Usuario identificado con exito.";
                        validado = true;
                        //TODO: LLamar a la ventana principal del sistema
                        this.Hide();
                        FormPrincipal fp = new FormPrincipal();
                        fp.Show();
                    }
                    else if (count > 1)
                    {
                        MessageBox.Show("Usuario o password duplicados!");
                        textBox1.Focus();
                        textBox1.SelectAll();
                        textBox1.BackColor = Color.LightCoral;
                        textBox2.BackColor = Color.LightCoral;
                        validado = false;
                    }
                    else
                    {
                        MessageBox.Show("Error de usuario o password");
                        textBox1.Focus();
                        textBox1.SelectAll();
                        textBox1.BackColor = Color.LightCoral;
                        textBox2.BackColor = Color.LightCoral;
                        validado = false;
                    }
                    myCon.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la consulta: ", ex.Message);
                }
                validado = true;
            }
                

            return validado;
        }
    }
}
