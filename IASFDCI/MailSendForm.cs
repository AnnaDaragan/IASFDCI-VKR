using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace IASFDCI
{
    public partial class MailSendForm : Form
    {
        public MailSendForm()
        {
            InitializeComponent();
        }

        string bytes = "";
        public string mailReg;

        private void MailSendForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Left = ClientSize.Width / 2 - pictureBox1.Width / 2;
            pictureBox1.Top = ClientSize.Height / 2 - pictureBox1.Height / 2;

            var rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                bytes += rand.Next(0, 10).ToString();
            }

            SmtpClient smptClient = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "ias.fdci@gmail.com",
                    Password = "10916997a!"
                }
            };

            MailAddress fromAddress = new MailAddress("ias.fdci@gmail.com");
            MailAddress toAddress = new MailAddress(mailReg);
            MailMessage mess = new MailMessage()
            {
                From = fromAddress,
                Subject = "Код для подтверждения почты",
                Body = bytes,
            };
            mess.To.Add(toAddress);

            smptClient.Send(mess);

        }

        private void ConfBut_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ConfCodeTextBox.Text))
            {
                if (ConfCodeTextBox.Text == bytes)
                {
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Код не совпадает", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите код", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
