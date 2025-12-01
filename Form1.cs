using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace OracleWinFormsApp
{
    public partial class Form1 : Form
    {
        private TextBox txtName;
        private TextBox txtEmail;
        private Button btnSave;
        private DataGridView dgv;
        private Label lblStatus;

        // cambie la cadena de conexion por la suya o lea de un archivo de confirguracion.
        private string connectionString = "User Id=system;Password=Tapiero123;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=127.0.0.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));";

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Oracle WinForms App - Registro sencillo";
            this.Width = 700;
            this.Height = 480;
            this.StartPosition = FormStartPosition.CenterScreen;

            var lbl1 = new Label() { Text = "Nombre:", Left = 20, Top = 20, Width = 80 };
            txtName = new TextBox() { Left = 110, Top = 18, Width = 400 };

            var lbl2 = new Label() { Text = "Email:", Left = 20, Top = 58, Width = 80 };
            txtEmail = new TextBox() { Left = 110, Top = 56, Width = 400 };

            btnSave = new Button() { Text = "Guardar", Left = 110, Top = 92, Width = 120 };
            btnSave.Click += BtnSave_Click;

            lblStatus = new Label() { Text = "", Left = 250, Top = 96, Width = 400 };

            dgv = new DataGridView()
            {
                Left = 20,
                Top = 140,
                Width = 640,
                Height = 280,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            this.Controls.Add(lbl1);
            this.Controls.Add(txtName);
            this.Controls.Add(lbl2);
            this.Controls.Add(txtEmail);
            this.Controls.Add(btnSave);
            this.Controls.Add(lblStatus);
            this.Controls.Add(dgv);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Ingrese un nombre. ", "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try {                 
                using (var db = new DataAccess(connectionString))
                {
                    db.InsertPerson(name, email);
                }
                lblStatus.Text = "Guardado correctamente .";
                txtName.Text = "";
                txtEmail.Text = "";
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error: " + ex.Message;
            }
        }

        private void LoadData()
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
