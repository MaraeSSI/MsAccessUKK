using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetaMart12
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    this.barangBetaTableAdapter.Fill(this.mydbDataSet.BarangBeta);
                    barangBetaBindingSource.DataSource = this.mydbDataSet.BarangBeta;
                    //dataGridView1.DataSource = barangBetaBindingSource;
                }
                else
                {
                    var query = from o in this.mydbDataSet.BarangBeta
                                where o.NamaBarang.Contains(txtSearch.Text) || o.JenisBarang == txtSearch.Text || o.Tanggal == txtSearch.Text || o.Deskripsi.Contains(txtSearch.Text)
                                select o;
                    barangBetaBindingSource.DataSource = query.ToList();
                    //dataGridView1.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Hapus record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    barangBetaBindingSource.RemoveCurrent();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using(OpenFileDialog ofd = new OpenFileDialog() { Filter="JPEG|*.jpg", ValidateNames=true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtNama.Focus();
                this.mydbDataSet.BarangBeta.AddBarangBetaRow(this.mydbDataSet.BarangBeta.NewBarangBetaRow());
                barangBetaBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBetaBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtNama.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            barangBetaBindingSource.ResetBindings(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                barangBetaBindingSource.EndEdit();
                barangBetaTableAdapter.Update(this.mydbDataSet.BarangBeta);
                panel.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBetaBindingSource.ResetBindings(false);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mydbDataSet.BarangBeta' table. You can move, or remove it, as needed.
            this.barangBetaTableAdapter.Fill(this.mydbDataSet.BarangBeta);
            barangBetaBindingSource.DataSource = this.mydbDataSet.BarangBeta;

        }
    }
}
