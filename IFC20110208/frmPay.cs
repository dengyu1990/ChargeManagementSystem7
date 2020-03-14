using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IFC20110208
{
    public partial class frmPay : Form
    {
        public frmPay()
        {
            InitializeComponent();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            int incoming; //用户输入寝费收入金额；
            int dbalance=0; //寝费总余额；
            int balance=0; //个人缴纳寝费总额；
            bool state=int.TryParse(txtPay.Text,out incoming);
            string strReturnFee = (0 - incoming).ToString();
            if (cboPerson.SelectedIndex >= 0 && txtPay.Text.Trim() != "" && state)
            {
                int uid = 10001 + cboPerson.SelectedIndex;
                string strDiary = cboPerson.SelectedItem.ToString() + "交纳寝费" + txtPay.Text + "元";
                if (incoming < 0)
                {
                    strDiary = "寝室退还" + cboPerson.SelectedItem.ToString() + "寝费" + strReturnFee + "元";
                }
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
                                dbalance = incoming + dbalance;
                            }
                        }
                        cmd.CommandText = "Select Balance from T_Count where UID='" + uid + "'";
                        using (SqlDataReader reader = cmd.ExecuteReader()) 
                        {
                            if (reader.Read())
                            {
                                balance = reader.GetInt32(reader.GetOrdinal("Balance"));
                                balance = incoming + balance;//更新个人缴费总额；
                            }
                        }  

                        cmd.CommandText = "Insert into T_Pay(PRoommate,PMoney,PTime) values ('" + cboPerson.SelectedItem.ToString() + "','" + incoming + "','" + DateTime.Now + "');Insert into T_Dormitory(DType,DMoney,DBalance,DTime) values ('" + "收入" + "','" + incoming + "','" + dbalance + "','" + DateTime.Now + "');Update T_Count set Balance='" + balance + "' where UID='" + uid + "' ;Insert into T_Diary(LTime,LDiary) values ('"+DateTime.Now+"','"+strDiary+"')";
                        cmd.ExecuteNonQuery();
                        if (incoming < 0)
                        {
                            MessageBox.Show("您已成功为" + cboPerson.SelectedItem.ToString() + "退还寝费" + strReturnFee + "元！", "退还成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else 
                        {
                            MessageBox.Show("您已成功为" + cboPerson.SelectedItem.ToString() + "缴费" + txtPay.Text + "元！", "缴费成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        this.Close();
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("输入有误！请检查您的输入是否合法！", "缴费失败", MessageBoxButtons.RetryCancel, MessageBoxIcon.Stop);
                if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
            
        }
    }
}
