using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using IASFDCI.Connection;

namespace IASFDCI
{
    public partial class PersonalCabinetForm : Form
    {
        public PersonalCabinetForm()
        {
            InitializeComponent();
            RepPanel.Visible = false;
            comboBox1.Text = comboBox1.Items[0].ToString();
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox3.Text = comboBox3.Items[0].ToString();
            comboBox4.Text = comboBox4.Items[0].ToString();
            comboBox5.Text = comboBox5.Items[0].ToString();
        }

        public string loginId, userId;
        public int intSpecialistId;

        private void reset()
        {
            NumPolisTextBox.Clear();
            FNameTextBox.Clear();
            SNameTextBox.Clear();
            TNameTextBox.Clear();
            NumPhoneTextBox.Clear();
            DataBirthTextBox.Clear();
            MaleRadioButton.Checked = false;
            FemaleRadioButton.Checked = false;
        }

        private void execute(string querySql)
        {
            ConProb.cmd = new SqlCommand(querySql, ConProb.con);
            ConProb.ExecuteSQL(ConProb.cmd);
        }

        private void PersonalCabinetForm_Load(object sender, EventArgs e)
        {
            string com = "SELECT (SecondName + ' ' + ThirdName) FROM Specialists WHERE Login = '" + loginId + "'";
            string NameSecUser = ResultQuerySql(com);
            HelloLabel.Text = "Добро пожаловать, " + NameSecUser;

            loadData("");
        }

        static string ResultQuerySql(string com)
        {
            ConProb.sql = com;
            ConProb.cmd = new SqlCommand(ConProb.sql, ConProb.con);
            DataTable userData = ConProb.ExecuteSQL(ConProb.cmd);

            var str = "";
            foreach (DataRow row in userData.Rows)
            {
                for (int i = 0; i < userData.Columns.Count; i++)
                {
                    str = row[i].ToString();
                }
            }

            return str;
        }

        private void OutputBut_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();

        }

        private void loadData(string keyword)
        {
            string com = "SELECT id FROM Specialists WHERE Login = '" + loginId + "'";
            string specialistId = ResultQuerySql(com);
            intSpecialistId = Convert.ToInt32(specialistId);

            ConProb.sql = "SELECT NumPolis, (FirstName + ' ' + SecondName + ' ' + ThirdName), Gender," +
                "NumberPhone, DataBirth, FirstName, SecondName, ThirdName, id FROM Patients WHERE id_specialist = '" + intSpecialistId + "'" +
                "AND (FirstName + ' ' + SecondName + ' ' + ThirdName) LIKE '%' + @keyword + '%' OR NumPolis LIKE '%' + @keyword + '%'";
            ConProb.cmd = new SqlCommand(ConProb.sql, ConProb.con);
            ConProb.cmd.Parameters.Clear();
            ConProb.cmd.Parameters.AddWithValue("keyword", keyword);
            DataTable pacientData = ConProb.ExecuteSQL(ConProb.cmd);

            dataGridView1.DataSource = pacientData;
            dataGridView1.Columns[0].HeaderText = "Номер полиса";
            dataGridView1.Columns[1].HeaderText = "ФИО пациента";
            dataGridView1.Columns[2].HeaderText = "Пол";
            dataGridView1.Columns[3].HeaderText = "Номер телефона";
            dataGridView1.Columns[4].HeaderText = "Дата рождения";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 110;

            ConProb.sql = "SELECT id, NNInterval, MeasurementDate FROM KIG WHERE NNInterval LIKE '%' + @keyword + '%'";
            ConProb.cmd = new SqlCommand(ConProb.sql, ConProb.con);
            ConProb.cmd.Parameters.Clear();
            ConProb.cmd.Parameters.AddWithValue("keyword", keyword);
            DataTable NNData = ConProb.ExecuteSQL(ConProb.cmd);
            dataGridView2.DataSource = NNData;
            dataGridView2.Columns[0].Width = 98;
            dataGridView2.Columns[1].Width = 150;
            dataGridView2.Columns[2].Width = 150;

            dataGridView2.Columns[0].HeaderText = "Номер записи";
            dataGridView2.Columns[1].HeaderText = "Название файла";
            dataGridView2.Columns[2].HeaderText = "Дата снятия";
        }

        private void SearchBut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text.Trim()))
            {
                loadData("");
            }
            else
            {
                loadData(SearchTextBox.Text.Trim());
            }
        }

        string Gender;

        private void MaleRadioButton_CheckedChanged_1(object sender, EventArgs e)
        {
            Gender = "Мужской";
        }

        private void FemaleRadioButton_CheckedChanged_1(object sender, EventArgs e)
        {
            Gender = "Женский";
        }

        private void CreateBut_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            string cap = "Ошибка доступа";

            if (string.IsNullOrEmpty(FNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите фамилию пациента.", cap, btn);
                FNameTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(SNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите имя пациента.", cap, btn);
                SNameTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(TNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите отчество пациента.", cap, btn);
                TNameTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(Gender))
            {
                MessageBox.Show("Пожалуйста, укажите пол пациента.", cap, btn);
                return;
            }

            if (string.IsNullOrEmpty(NumPhoneTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите номер телефона пациента.", cap, btn);
                NumPhoneTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(DataBirthTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите дату рождения пациента.", cap, btn);
                NumPhoneTextBox.Select();
                return;
            }

            ConProb.sql = "INSERT INTO Patients (NumPolis, FirstName, SecondName, ThirdName, Gender, NumberPhone, DataBirth, id_specialist)" +
                "VALUES ('" + NumPolisTextBox.Text + "','" + FNameTextBox.Text + "','" + SNameTextBox.Text + "','" + TNameTextBox.Text + "','" + Gender + "'," +
                "'" + NumPhoneTextBox.Text + "','" + DataBirthTextBox.Text + "','" + intSpecialistId + "')";
            execute(ConProb.sql);
            loadData("");
            reset();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            userId = Convert.ToString(dataGridView1.CurrentRow.Cells[8].Value);
            NumPolisTextBox.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            FNameTextBox.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
            SNameTextBox.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value);
            TNameTextBox.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[7].Value);
            NumPhoneTextBox.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
            DataBirthTextBox.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);

            if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) == "Мужской")
            {
                MaleRadioButton.Checked = true;
            }
            else
            {
                FemaleRadioButton.Checked = true;
            }

        }

        private void UpdateBut_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {

            }

            ConProb.sql = "UPDATE Patients SET NumPolis = '" + NumPolisTextBox.Text + "',  FirstName = '" + FNameTextBox.Text + "', SecondName = '" + SNameTextBox.Text + "'," +
                "ThirdName = '" + TNameTextBox.Text + "', Gender = '" + Gender + "', NumberPhone = '" + NumPhoneTextBox.Text + "'," +
                "DataBirth = '" + DataBirthTextBox.Text + "', id_specialist = '" + intSpecialistId + "' WHERE id = '" + userId + "'";
            execute(ConProb.sql);
            loadData("");
            reset();
        }

        private void DelBut_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {

            }

            ConProb.sql = "DELETE FROM Patients WHERE id = '" + userId + "'";
            execute(ConProb.sql);
            loadData("");
            reset();

        }

        private void reset1()
        {
            NumPolisTextBox2.Clear();
            DateNNTextBox.Clear();
            WayFileTextBox.Clear();
            comboBox1.Text = comboBox1.Items[0].ToString();
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox3.Text = comboBox3.Items[0].ToString();
            comboBox4.Text = comboBox4.Items[0].ToString();
            comboBox5.Text = comboBox5.Items[0].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = @"C:\Users\yanak\Desktop\";
            openFile.Filter = "txt files (*.txt) |*.txt";
            openFile.RestoreDirectory = true;

            string sqlQuery = "SELECT FirstName FROM Patients WHERE NumPolis = '" + NumPolisTextBox2.Text + "'";
            string[] date = DateNNTextBox.Text.Split(' ');
            string[] time = date[1].Split(':');

            string fileName = NumPolisTextBox2.Text + "_" + date[0] + "_" + time[0] + "_" + time[1] + ".txt";
            Console.WriteLine(fileName);
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                WayFileTextBox.Text = openFile.FileName;
                var fileStream = openFile.OpenFile();
                var newFile = File.Create("C:/Users/yanak/Desktop/DataDB/" + fileName);
                newFile.Close();
                StreamWriter sw = new StreamWriter("C:/Users/yanak/Desktop/DataDB/" + fileName);

                using (var reader = new StreamReader(fileStream))
                {
                    int s = 1;
                    while (!reader.EndOfStream)
                    {
                        string j = s.ToString() + "  ";
                        var line = reader.ReadLine();
                        if (line.StartsWith(j))
                        {
                            var arr = line.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                            sw.WriteLine(arr[2]);
                            s += 1;
                        }
                    }
                    sw.Close();
                    reader.Close();
                }
            }

            sqlQuery = "SELECT id FROM Patients WHERE NumPolis = '" + NumPolisTextBox2.Text + "'";
            string idPac = ResultQuerySql(sqlQuery);
            ConProb.sql = "INSERT INTO KIG (MeasurementDate, NNInterval, id_patient)" +
                          "VALUES ('" + DateNNTextBox.Text + "','" + fileName + "','" + Convert.ToInt32(idPac) + "')";
            execute(ConProb.sql);

            sqlQuery = "SELECT id FROM KIG WHERE NNInterval = '" + fileName + "'";
            string idKIG = ResultQuerySql(sqlQuery);
            ConProb.sql = "INSERT INTO InitialState (PopulationType, Wellbeing, Dyspnea, Heartache, DiseasesCVS, id_patient, id_KIG)" +
                           "VALUES ('" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + comboBox4.Text + "','" + comboBox5.Text + "'," +
                           "'" + Convert.ToInt32(idPac) + "','" + Convert.ToInt32(idKIG) + "')";
            execute(ConProb.sql);
            loadData("");
            reset1();
        }

        private void SearchBut2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox2.Text.Trim()))
            {
                loadData("");
            }
            else
            {
                loadData(SearchTextBox2.Text.Trim());
            }
        }

        public Tuple<double, double, double[], List<double>, List<double>> qusiAttract(string doc)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            List<double> arr = new List<double>();

            double prom;
            string sqlQuery = "SELECT NNInterval FROM KIG WHERE id = '" + doc + "'";
            string fileName = ResultQuerySql(sqlQuery);
            using (var reader = new StreamReader("C:/Users/yanak/Desktop/DataDB/" + fileName))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    prom = Math.Round(double.Parse(line), 2);
                    arr.Add(prom);
                }
                reader.Close();
            }

            List<double> dx = new List<double>();
            List<double> dx2 = new List<double>();
            for (int i = 0; i < (arr.Count - 1); i++)
            {
                dx.Add(Math.Round(arr[i + 1] - arr[i], 2));
            }
            dx.Add(0.0);

            for (int i = 0; i < (dx.Count - 1); i++)
            {
                dx2.Add(Math.Round(dx[i + 1] - dx[i], 2));
            }
            dx2.Add(0.0);

            double Ska, Vka;
            Ska = Math.Round((arr.Max() - arr.Min()) * (dx.Max() - dx.Min()), 2);
            Vka = Math.Round((arr.Max() - arr.Min()) * (dx.Max() - dx.Min()) * (dx2.Max() - dx2.Min()), 2);
            double[] coord = new double[2];
            coord[0] = Math.Round((arr.Max() + arr.Min()) / 2, 2);
            coord[1] = Math.Round((dx2.Max() + dx2.Min()) / 2, 2);

            ConProb.sql = "SELECT id_KIG FROM Quasi_attractors WHERE id_KIG = @idKIG";
            ConProb.cmd = new SqlCommand(ConProb.sql, ConProb.con);
            ConProb.cmd.Parameters.Clear();
            ConProb.cmd.Parameters.AddWithValue("idKIG", doc);
            DataTable checkDuplicate = ConProb.ExecuteSQL(ConProb.cmd);

            if (checkDuplicate.Rows.Count > 0)
            {

            }
            else
            {
                ConProb.sql = "INSERT INTO Quasi_attractors (Volume, Square, CenterCoordinatesX, CenterCoordinatesY, id_KIG)" +
                "VALUES ('" + Vka + "','" + Ska + "','" + coord[0] + "','" + coord[1] + "','" + Convert.ToInt32(doc) + "')";
                execute(ConProb.sql);
            }

            return Tuple.Create(Ska, Vka, coord, arr, dx2);
        }

        private void printGraphCI(int numSer, List<double> arr)
        {
            NNChart.ChartAreas[0].AxisX.Minimum = 0;
            NNChart.ChartAreas[0].AxisX.Maximum = arr.Count + 1;
            NNChart.Series[numSer].Points.Clear();
            for (int i = 0; i < arr.Count; i++)
            {
                NNChart.Series[numSer].Points.AddXY(i + 1, arr[i]);
            }
        }

        private void printGraphQA(int numSer, List<double> arr, List<double> dx2, int numCoord, double[] coord)
        {
            QAchart.ChartAreas[0].AxisX.Minimum = 0;
            QAchart.ChartAreas[0].AxisX.Maximum = arr.Max() + 100;
            QAchart.Series[numSer].Points.Clear();
            for (int i = 0; i < arr.Count; i++)
            {
                QAchart.Series[numSer].Points.AddXY(arr[i], dx2[i]);
            }

            QAchart.Series[numCoord].Points.Clear();
            QAchart.Series[numCoord].Points.AddXY(coord[0], coord[1]);
        }

        static string[] RetPacState(string idKig)
        {
            ConProb.sql = "SELECT PopulationType, Wellbeing, Dyspnea, Heartache, DiseasesCVS FROM InitialState WHERE id_KIG = '" + idKig + "'";
            ConProb.cmd = new SqlCommand(ConProb.sql, ConProb.con);
            DataTable pacState = ConProb.ExecuteSQL(ConProb.cmd);

            string[] retmasPacState = new string[pacState.Columns.Count];
            foreach (DataRow row in pacState.Rows)
            {
                for (int i = 0; i < pacState.Columns.Count; i++)
                {
                    retmasPacState[i] = row[i].ToString();
                }
            }

            return retmasPacState;
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
            var res = qusiAttract(NumbForRepTextBox1.Text);

            RepPanel.Visible = true;
            VLabel1.Text = res.Item1.ToString();
            SLabel1.Text = res.Item2.ToString();
            CoordLabel1.Text = "(" + res.Item3[0].ToString() + "; " + res.Item3[1].ToString() + ")";
            printGraphCI(0, res.Item4);
            printGraphQA(0, res.Item4, res.Item5, 2, res.Item3);

            var res1 = qusiAttract(NumbForRepTextBox2.Text);
            VLabel2.Text = res1.Item1.ToString();
            SLabel2.Text = res1.Item2.ToString();
            CoordLabel2.Text = "(" + res1.Item3[0].ToString() + "; " + res.Item3[1].ToString() + ")";
            printGraphCI(1, res1.Item4);
            printGraphQA(1, res1.Item4, res1.Item5, 3, res1.Item3);

            string sqlQuery = "SELECT MeasurementDate FROM KIG WHERE id = '" + NumbForRepTextBox1.Text + "'";
            var dataMeas = ResultQuerySql(sqlQuery);
            DateLabel1.Text = dataMeas.ToString();
            sqlQuery = "SELECT MeasurementDate FROM KIG WHERE id = '" + NumbForRepTextBox2.Text + "'";
            dataMeas = ResultQuerySql(sqlQuery);
            DateLabel2.Text = dataMeas.ToString();

            sqlQuery = "SELECT id_patient FROM KIG WHERE id = '" + NumbForRepTextBox1.Text + "'";
            var pacId = ResultQuerySql(sqlQuery);
            sqlQuery = "SELECT (FirstName + ' ' + SecondName + ' ' + ThirdName) FROM Patients WHERE id = '" + pacId + "'";
            var fio = ResultQuerySql(sqlQuery);
            FIOLabel.Text = fio;
            sqlQuery = "SELECT NumPolis FROM Patients WHERE id = '" + pacId + "'";
            string polis = ResultQuerySql(sqlQuery);
            PolisLabel.Text = polis;
            sqlQuery = "SELECT DataBirth FROM Patients WHERE id = '" + pacId + "'";
            DateTime dateBirth = DateTime.Parse(ResultQuerySql(sqlQuery));
            AgeLabel.Text = ((int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(dateBirth.ToString("yyyyMMdd")))/10000).ToString();
            sqlQuery = "SELECT Gender FROM Patients WHERE id = '" + pacId + "'";
            GenderLabel.Text = ResultQuerySql(sqlQuery);

            var masPacState = RetPacState(NumbForRepTextBox1.Text);

            TypePopLabel1.Text = masPacState[0];
            WellLabel1.Text = masPacState[1];
            DysLabel1.Text = masPacState[2];
            HeartLabel1.Text = masPacState[3];
            DisLabel1.Text = masPacState[4];

            var masPacState1 = RetPacState(NumbForRepTextBox2.Text);

            TypePopLabel2.Text = masPacState1[0];
            WellLabel2.Text = masPacState1[1];
            DysLabel2.Text = masPacState1[2];
            HeartLabel2.Text = masPacState1[3];
            DisLabel2.Text = masPacState1[4];

            double RatioV = Math.Round(res.Item2 / res1.Item2, 4);
            ResOtnLabel.Text = Convert.ToString(RatioV);
            if (RatioV <= 2 && RatioV >= 0.5)
            {
                sqlQuery = "SELECT type FROM State_CVS WHERE id = '" + 1 + "'";
                ResSostLabel.Text = ResultQuerySql(sqlQuery);
            }
            else
            {
                sqlQuery = "SELECT type FROM State_CVS WHERE id = '" + 2 + "'";
                ResSostLabel.Text = ResultQuerySql(sqlQuery);
            }

            NumbForRepTextBox1.Clear();
            NumbForRepTextBox2.Clear();
        }

        private void CloseRepButton_Click(object sender, EventArgs e)
        {
            RepPanel.Visible = false;
        }

        private void SaveRepBut_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            WayFileTextBox.Text = "C:/Users/yanak/Desktop/DataDB/" + Convert.ToString(dataGridView2.CurrentRow.Cells[1].Value);

        }

        private void DelFileBut_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {

            }

            File.Delete(WayFileTextBox.Text);

            ConProb.sql = "DELETE FROM KIG WHERE NNInterval = '" + Convert.ToString(dataGridView2.CurrentRow.Cells[1].Value) + "'";
            execute(ConProb.sql);
            loadData("");
            reset();
        }

        private void PersonalCabinetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ///Application.Exit();
            Environment.Exit(1);
        }

    }
}
