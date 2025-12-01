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

        private void LoadData()
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
