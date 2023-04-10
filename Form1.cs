using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laborator1
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter sqlAdapterAddress;
        private DataSet dataSetAddress;
        private BindingSource bindingSourceAddress;
        private SqlDataAdapter _sqlAdapterClients;
        private DataSet _dataSetClients;
        private BindingSource _clientsBoundingSource;

        public Form1()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            connection = new SqlConnection(connectionString);
            sqlAdapterAddress = new SqlDataAdapter();
            dataSetAddress = new DataSet();
            bindingSourceAddress = new BindingSource();
            _sqlAdapterClients = new SqlDataAdapter();
            _dataSetClients = new DataSet();
            _clientsBoundingSource = new BindingSource();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SqlCommand command = new SqlCommand("SELECT * FROM tables.Client",sc);
            // sc.Open();
            // SqlDataReader dataReader = command.ExecuteReader();
            // bs.DataSource = dataReader;
            // dataGridView1.DataSource = bs;
            // sc.Close();
            string parentSelectCommand = ConfigurationManager.AppSettings["parentSelect"];
            connection.Open();
            sqlAdapterAddress.SelectCommand = new SqlCommand(parentSelectCommand, connection);
            dataSetAddress.Clear();
            sqlAdapterAddress.Fill(dataSetAddress);
            gridAddress.DataSource = dataSetAddress.Tables[0];
            bindingSourceAddress.DataSource = dataSetAddress.Tables[0];
            connection.Close();
            updateClientsGrid();
        }

        private void updateClientsGrid()
        {
            if (gridAddress.SelectedRows.Count > 0)
            {
                string childSelect = ConfigurationManager.AppSettings["childSelect"];
                string childSelectByParameterName = ConfigurationManager.AppSettings["childSelectByParameterName"];
                string childSelectByParameterTableName =
                    ConfigurationManager.AppSettings["childSelectByParameterTableName"];

                _sqlAdapterClients.SelectCommand = new SqlCommand(childSelect, connection);
                String selectedAddressId =
                    gridAddress.SelectedRows[0].Cells[childSelectByParameterTableName].Value.ToString();
                _sqlAdapterClients.SelectCommand.Parameters.AddWithValue(childSelectByParameterName, selectedAddressId);
                connection.Open();
                _sqlAdapterClients.SelectCommand.ExecuteNonQuery();
                connection.Close();
                _dataSetClients.Clear();
                _sqlAdapterClients.Fill(_dataSetClients);
                clientGridView.DataSource = _dataSetClients.Tables[0];
            }
        }

        private void gridAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridAddress.SelectedRows.Count > 0)
            {
                string childSelectByParameterTableName =
                    ConfigurationManager.AppSettings["childSelectByParameterTableName"];
                updateClientsGrid();
                PopulateTextBoxes();
                var control = this.inputPanel.Controls.Find(childSelectByParameterTableName, true);
                control[0].Text = gridAddress.SelectedRows[0].Cells[childSelectByParameterTableName].Value.ToString();
            }
        }


        private void PopulateTextBoxes()
        {
            if (clientGridView.SelectedRows.Count > 0)
            {
                string textBoxNotToUpdate = ConfigurationManager.AppSettings["childSelectByParameterTableName"];
                string[] textBoxesNames = ConfigurationManager.AppSettings["childColumns"].Split(',');
                DataGridViewRow row = clientGridView.SelectedRows[0];
                for (int i = 0; i < textBoxesNames.Length; i++)
                {
                    var control = inputPanel.Controls.Find(textBoxesNames[i], true)[0];
                    if (control.Text != textBoxNotToUpdate)
                    {
                        control.Text = row.Cells[textBoxesNames[i]].Value.ToString();
                    }
                }
                // clientId.Text = row.Cells["id_client"].Value.ToString();
                // firstname.Text = row.Cells["first_name"].Value.ToString();
                // lastname.Text = row.Cells["last_name"].Value.ToString();
                // clientGridView.Text = row.Cells["id_client"].Value.ToString();
                // cnp.Text = row.Cells["personal_identification_number"].Value.ToString();
            }
        }

        private void clientGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (clientGridView.SelectedRows.Count > 0)
            {
                PopulateTextBoxes();
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string insertQuery = ConfigurationManager.AppSettings["childInsertQuery"];
            string[] insertParams = ConfigurationManager.AppSettings["childInsertParams"].Split(',');
            _sqlAdapterClients.InsertCommand = new SqlCommand(insertQuery, connection);
            for (int i = 0; i < insertParams.Length; i++)
            {
                var control = this.inputPanel.Controls.Find(insertParams[i], true)[0];
                string text = control.Text;
                if (String.IsNullOrWhiteSpace(text))
                {
                    return;
                }

                _sqlAdapterClients.InsertCommand.Parameters.AddWithValue(insertParams[i], text);
            }

            try
            {
                connection.Open();
                _sqlAdapterClients.InsertCommand.ExecuteNonQuery();
                MessageBox.Show("Inserted successfully");
                connection.Close();
                updateClientsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (clientGridView.SelectedRows.Count > 0)
            {
                string deleteQuery = ConfigurationManager.AppSettings["childDeleteQuery"];
                string childIdParam = ConfigurationManager.AppSettings["childIdParam"];

                String selectedClientId = clientGridView.SelectedRows[0].Cells[childIdParam].Value.ToString();
                _sqlAdapterClients.DeleteCommand = new SqlCommand(deleteQuery, connection);
                _sqlAdapterClients.DeleteCommand.Parameters.AddWithValue(childIdParam, selectedClientId);
                try
                {
                    connection.Open();
                    _sqlAdapterClients.DeleteCommand.ExecuteNonQuery();
                    MessageBox.Show("Deleted successfully");
                    connection.Close();
                    updateClientsGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            string updateQuery = ConfigurationManager.AppSettings["updateQuery"];
            _sqlAdapterClients.UpdateCommand = new SqlCommand(updateQuery, connection);
            string[] updateParams = ConfigurationManager.AppSettings["updateQueryParams"].Split(',');
            for (int i = 0; i < updateParams.Length; i++)
            {
                var control = this.inputPanel.Controls.Find(updateParams[i], true)[0];
                string text = control.Text;
                if (String.IsNullOrWhiteSpace(text))
                {
                    return;
                }

                _sqlAdapterClients.UpdateCommand.Parameters.AddWithValue(updateParams[i], text);
            }

            try
            {
                connection.Open();
                _sqlAdapterClients.UpdateCommand.ExecuteNonQuery();
                MessageBox.Show("Modified successfully");
                connection.Close();
                updateClientsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }
    }
}