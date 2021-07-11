using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IASFDCI.Connection;
using System.Security.Cryptography;

namespace IASFDCI
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Left = ClientSize.Width / 2 - pictureBox1.Width / 2;
            pictureBox1.Top = ClientSize.Height / 2 - pictureBox1.Height / 2;
            FNameTextBox.Select();
        }

        string Gender;

        private void MaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Мужской";
        }

        private void FemaleRadioButton_CheckedChanged_1(object sender, EventArgs e)
        {
            Gender = "Женский";
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void showPasswCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showPasswCheckBox.Checked == true)
            {
                PassTextBox.UseSystemPasswordChar = false;
                ConfirmPassTextBox.UseSystemPasswordChar = false;
            }
            else
            {
                PassTextBox.UseSystemPasswordChar = true;
                ConfirmPassTextBox.UseSystemPasswordChar = true;
            }
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

        private void RegistrButton_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            string cap = "Ошибка доступа";

            if(string.IsNullOrEmpty(FNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите Фамилию.", cap, btn);
                FNameTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(SNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите Имя.", cap, btn);
                SNameTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(TNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите Отчество.", cap, btn);
                TNameTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(NumPhoneTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите Номер телефона.", cap, btn);
                NumPhoneTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите E-mail.", cap, btn);
                EmailTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(Gender))
            {
                MessageBox.Show("Пожалуйста, укажите Ваш пол.", cap, btn);
                return;
            }

            if (string.IsNullOrEmpty(UsNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите Логин.", cap, btn);
                UsNameTextBox.Select();
                return;
            }

            ConProb.sql = "SELECT Login FROM Specialists WHERE Login = @userName";
            ConProb.cmd = new SqlCommand(ConProb.sql, ConProb.con);
            ConProb.cmd.Parameters.Clear();
            ConProb.cmd.Parameters.AddWithValue("userName", UsNameTextBox.Text.Trim());
            DataTable checkDuplicate = ConProb.ExecuteSQL(ConProb.cmd);

            if (checkDuplicate.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует, пожалуйтса введите новый.", "Ошибка доступа", MessageBoxButtons.OK);
                return;
            }

            if (string.IsNullOrEmpty(PassTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите Пароль.", cap, btn);
                PassTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(ConfirmPassTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, подтвердите пароль.", cap, btn);
                ConfirmPassTextBox.Select();
                return;
            }

            if (PassTextBox.Text != ConfirmPassTextBox.Text)
            {
                MessageBox.Show("Пароли не совпадают, введите пароль заново.", cap, btn);
                ConfirmPassTextBox.SelectAll();
                return;
            }

            PassTextBox.Text = Encrypt(PassTextBox.Text);

            MailSendForm sendForm = new MailSendForm();
            sendForm.mailReg = EmailTextBox.Text;
            sendForm.ShowDialog();

            ConProb.sql = "INSERT INTO Specialists (FirstName, SecondName, ThirdName, Gender, NumberPhone, Email, Login, Password)" +
                "VALUES ('" + FNameTextBox.Text + "','" + SNameTextBox.Text + "','" + TNameTextBox.Text + "','" + Gender + "'," +
                "'" + NumPhoneTextBox.Text + "','" + EmailTextBox.Text + "','" + UsNameTextBox.Text + "','" + PassTextBox.Text + "')";
            ConProb.cmd = new SqlCommand(ConProb.sql, ConProb.con);
            ConProb.ExecuteSQL(ConProb.cmd);

            this.Hide();
            PersonalCabinetForm personalCabinetForm = new PersonalCabinetForm();
            personalCabinetForm.loginId = UsNameTextBox.Text;
            personalCabinetForm.Show();
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
