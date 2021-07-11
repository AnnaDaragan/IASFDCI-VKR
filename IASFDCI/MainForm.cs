using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using IASFDCI.Connection;
using System.Security.Cryptography;

namespace IASFDCI
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Left = ClientSize.Width / 2 - pictureBox1.Width / 2;
            pictureBox1.Top = ClientSize.Height / 2 - pictureBox1.Height / 2;
            label1.Left = ClientSize.Width / 2 - label1.Width / 2;
            label4.Left = ClientSize.Width / 2 - label4.Width / 2;
            pictureBox2.Left = ClientSize.Width / 2 - pictureBox2.Width / 2;

        }

        private void showPasswCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(showPasswCheckBox.Checked == true)
            {
                passwordBox.UseSystemPasswordChar = false;
            }
            else
            {
                passwordBox.UseSystemPasswordChar = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            RegistrationForm registrationForm = new RegistrationForm(); //обращаемся к RegistrationForm на его основе создаём объект registrationForm и выделяем под него память
            registrationForm.Show();
        }

        static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        private void Loginbut_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(loginTextBox.Text) && !string.IsNullOrEmpty(passwordBox.Text))
            {
                string passwordEnc;
                passwordEnc = Encrypt(passwordBox.Text);

                ConProb.sql = "SELECT * FROM Specialists WHERE Login = @login AND Password = @password";
                ConProb.cmd = new SqlCommand(ConProb.sql, ConProb.con);
                ConProb.cmd.Parameters.Clear();
                ConProb.cmd.Parameters.AddWithValue("login", loginTextBox.Text.Trim());
                ConProb.cmd.Parameters.AddWithValue("password", passwordEnc);

                DataTable userData = ConProb.ExecuteSQL(ConProb.cmd);
                /*string servSQL = string.Empty;

                servSQL += "SELECT * FROM Specialists ";
                servSQL += "WHERE Login = '" + loginTextBox.Text + "' ";
                servSQL += "AND Password = '" + passwordEnc + "'";

                DataTable userData = ConnectionDB.executeSQL(servSQL);*/

                if(userData.Rows.Count > 0)
                {
                    this.Hide();
                    PersonalCabinetForm personalCabinetForm = new PersonalCabinetForm();
                    personalCabinetForm.loginId = loginTextBox.Text;
                    personalCabinetForm.Show();
                }
                else
                {
                    passwordBox.Clear();
                    MessageBox.Show("Неправильно введён логин или пароль!", "Ошибка доступа", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль!", "Ошибка доступа", MessageBoxButtons.OK);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ///Application.Exit();
            Environment.Exit(1);
        }

    }
}
