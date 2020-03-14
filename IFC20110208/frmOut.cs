using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IFC20110208
{
    public partial class frmOut : Form
    {
        public frmOut()
        {
            InitializeComponent();
        }

        private void btnCost_Click(object sender, EventArgs e)
        {
            int cost;
            int dbalance=0;
            bool state = int.TryParse(txtCost.Text, out cost);

            if (cboExpenditure.SelectedIndex >= 0 && txtCost.Text.Trim() != "" && state)
            {
                string strDiary = "寝室因为" + cboExpenditure.SelectedItem.ToString() + "而支出" + txtCost.Text + "元";
                using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDBFilename=|DataDirectory|\IFC20110208.mdf;Integrated Security=True;User Instance=True"))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT TOP (1) DBalance FROM T_Dormitory ORDER BY DID DESC";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dbalance = reader.GetInt32(reader.GetOrdinal("DBalance"));
                                if (cost > dbalance)
                                {
                                    DialogResult result = MessageBox.Show("寝室余额将为负数，是否仍然继续支出？", "余额不足", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (result == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                                dbalance = dbalance-cost;
                            }
                        }
                        cmd.CommandText = "Insert into T_Cost(CExpenditure,CMoney,CTime) values ('" + cboExpenditure.SelectedItem.ToString() + "','" + cost + "','" + DateTime.Now + "');Insert into T_Dormitory(DType,DMoney,DBalance,DTime) values ('" + "支出" + "','" + cost + "','" + dbalance + "','" + DateTime.Now + "');Insert into T_Diary(LTime,LDiary) values ('" + DateTime.Now + "','" + strDiary + "')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("您已成功为" + cboExpenditure.SelectedItem.ToString() + "支出" + txtCost.Text + "元！", "支出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("输入有误！请检查您的输入是否合法！", "支出失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
        }
    }
}
