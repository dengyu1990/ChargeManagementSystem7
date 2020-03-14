using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using IFC20110208.dstPrinterTableAdapters;

namespace IFC20110208
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.t_PrinterTableAdapter.Fill(this.dstPrinter.T_Printer);
            this.t_DiaryTableAdapter.Fill(this.iFC20110208DataSet4.T_Diary);
            this.t_DormitoryTableAdapter.Fill(this.iFC20110208DataSet3.T_Dormitory);
            this.t_CountTableAdapter.Fill(this.iFC20110208DataSet2.T_Count);
            this.t_CostTableAdapter.Fill(this.iFC20110208DataSet1.T_Cost);
            this.t_PayTableAdapter.Fill(this.iFC20110208DataSet.T_Pay);
            int Fee;
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
                            Fee = reader.GetInt32(reader.GetOrdinal("DBalance"));
                            lblFee.Text = Fee.ToString() + "元";
                            if (Fee < 0)
                            {
                                lblFee.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            frmPay frmInc = new frmPay();
            frmInc.Show();
        }

        private void btnCost_Click(object sender, EventArgs e)
        {
            frmOut frmCost = new frmOut();
            frmCost.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("您确认要安全退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("软件版本：v4.0 支持服务：jardy@vip.qq.com", "帮助信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }

        private void tabPagePrinter_Leave(object sender, EventArgs e)
        {
            btnPay.Enabled = true;
            btnCost.Enabled = true;
            btnHelp.Enabled = true;
            btnExit.Enabled = true;
            lblFee.ForeColor = Color.FromArgb(0, 64, 0);
            label1.ForeColor = Color.FromArgb(0, 64, 0);
        }

        private void tabPagePrinter_Enter(object sender, EventArgs e)//鼠标进入此管理中心时的加载事件；
        {
            btnPay.Enabled = false;
            btnCost.Enabled = false;
            btnHelp.Enabled = false;
            btnExit.Enabled = false;
            lblFee.ForeColor = Color.LightGray;
            label1.ForeColor = Color.LightGray;

            T_PrinterTableAdapter adapterT = new T_PrinterTableAdapter();
            int lastCount = Convert.ToInt32(adapterT.GetLastTotalCount());
            lblTotalCount.Text = "总资产：" + lastCount + "元";

            T_InvestorTableAdapter adapter = new T_InvestorTableAdapter();
            dstPrinter.T_InvestorDataTable data = adapter.GetData();
            lblRat.Text = "耗子：" + data[0].VCount.ToString() + "元";
            lblCat.Text = "八哥：" + data[1].VCount.ToString() + "元";
            lblFish.Text = "邓子：" + data[2].VCount.ToString() + "元";
        }

        private void btnInvest_Click(object sender, EventArgs e)
        {
            int invest;
            bool state = int.TryParse(txtInvest.Text, out invest);
            if (cboPartner.SelectedIndex >= 0 && state)
            {
                string msgInvest = "投资-" + cboPartner.SelectedItem.ToString();
                //将投资额写入数据库；
                if (invest < 0)
                {
                    DialogResult result = MessageBox.Show("您确定要收回投资吗？系统将支付给" + cboPartner.SelectedItem.ToString() + (0 - invest) + "元！", "收回投资", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    msgInvest = cboPartner.SelectedItem.ToString() + "-收回" + (0 - invest) + "元";
                }
                T_PrinterTableAdapter adapter = new T_PrinterTableAdapter();
                dstPrinter.T_PrinterDataTable table = new dstPrinter.T_PrinterDataTable();
                int lastCount = Convert.ToInt32(adapter.GetLastTotalCount());
                lastCount += invest;
                adapter.Insert(msgInvest, invest, lastCount, DateTime.Now.Date);
                adapter.Update(table);
                this.t_PrinterTableAdapter.Fill(this.dstPrinter.T_Printer);
                lblTotalCount.Text = "总资产：" + lastCount + "元";
                txtInvest.Text = "";
           ////////////////////////////////////////////////////////////////////////////////////////////
                T_InvestorTableAdapter adapterP = new T_InvestorTableAdapter();
                dstPrinter.T_InvestorDataTable data = adapterP.GetData();
                switch (cboPartner.SelectedIndex)
                {
                    case 0: data[0].VCount -= invest;
                        break;
                    case 1: data[1].VCount -= invest;
                        break;
                    case 2: data[2].VCount -= invest;
                        break;
                }
                adapterP.Update(data);
                lblRat.Text = "耗子：" + data[0].VCount.ToString() + "元";
                lblCat.Text = "八哥：" + data[1].VCount.ToString() + "元";
                lblFish.Text = "邓子：" + data[2].VCount.ToString() + "元";
           ////////////////////////////////////////////////////////////////////////////////////////////
                if (invest < 0)
                {
                    MessageBox.Show(cboPartner.SelectedItem.ToString() + "已经收回投资" + (0 - invest) + "元！", "收回成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                MessageBox.Show("股东" + cboPartner.SelectedItem.ToString() + "已经成功投资" + invest + "元！","投资成功",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("请检查输入是否合理！", "投资失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnProfit_Click(object sender, EventArgs e)
        {
            int profit;
            bool state = int.TryParse(txtSell.Text, out profit);
            if (state && profit > 0)
            {
                txtSell.Text = "";
                //销售额写入数据库；
                T_PrinterTableAdapter adapter = new T_PrinterTableAdapter();
                dstPrinter.T_PrinterDataTable table = new dstPrinter.T_PrinterDataTable();
                int lastCount = Convert.ToInt32(adapter.GetLastTotalCount());
                lastCount += profit;
                adapter.Insert("盈利", profit, lastCount, DateTime.Now.Date);
                adapter.Update(table);
                this.t_PrinterTableAdapter.Fill(this.dstPrinter.T_Printer);
                lblTotalCount.Text = "总资产：" + lastCount + "元";
                MessageBox.Show("本次管理中心盈利共" + profit + "元！", "入账成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("销售额不能为负数！", "输入有误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            int expense;
            bool state = int.TryParse(txtExpense.Text, out expense);
            if (cboPartner.SelectedIndex >= 0 && state && expense > 0)
            {
                //将开销额写入数据库；
                string msgExpense = "开销-" + cboProject.SelectedItem.ToString();
                T_PrinterTableAdapter adapter = new T_PrinterTableAdapter();
                dstPrinter.T_PrinterDataTable table = new dstPrinter.T_PrinterDataTable();
                int lastCount = Convert.ToInt32(adapter.GetLastTotalCount());
                lastCount -= expense;
                adapter.Insert(msgExpense, (0 - expense), lastCount, DateTime.Now.Date);
                adapter.Update(table);
                this.t_PrinterTableAdapter.Fill(this.dstPrinter.T_Printer);
                lblTotalCount.Text = "总资产：" + lastCount + "元";
                txtExpense.Text = "";
                MessageBox.Show("管理中心为" + cboProject.SelectedItem.ToString() + "开销支出" + expense + "元！", "开销成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("请检查输入是否合理！", "开销失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void btnSalary_Click(object sender, EventArgs e)
        {
            int salary;
            bool state=int.TryParse(txtSalary.Text,out salary);
            if (state && salary%3==0 && salary>=3)
            {
                txtSalary.Text = "";
                //发放工资写入数据库；
                string msgSalary = "分钱-人均" + salary / 3 + "元";
                T_PrinterTableAdapter adapter = new T_PrinterTableAdapter();
                dstPrinter.T_PrinterDataTable table = new dstPrinter.T_PrinterDataTable();
                int lastCount = Convert.ToInt32(adapter.GetLastTotalCount());
                lastCount -= salary;
                adapter.Insert(msgSalary, (0 - salary), lastCount, DateTime.Now.Date);
                adapter.Update(table);
                this.t_PrinterTableAdapter.Fill(this.dstPrinter.T_Printer);
                lblTotalCount.Text = "总资产：" + lastCount + "元";
                ////////////////////////////////////////////////////////////////////////////////////////////
                T_InvestorTableAdapter adapterP = new T_InvestorTableAdapter();
                dstPrinter.T_InvestorDataTable data = adapterP.GetData();
                data[0].VCount += salary / 3;
                data[1].VCount += salary / 3;
                data[2].VCount += salary / 3;
                adapterP.Update(data);
                lblRat.Text = "耗子：" + data[0].VCount.ToString() + "元";
                lblCat.Text = "八哥：" + data[1].VCount.ToString() + "元";
                lblFish.Text = "邓子：" + data[2].VCount.ToString() + "元";
                ////////////////////////////////////////////////////////////////////////////////////////////
                MessageBox.Show("管理中心本次共发放工资" + salary + "元！（人均" + salary / 3 + "元）", "发放成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("发放最低标准为3元！（且要为3的倍数）", "输入有误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {
            int salary;
            bool state=int.TryParse(txtSalary.Text,out salary);
            if (state && salary % 3 == 0 && salary >= 3)
            {
                lblMsgSalary.Text = "平均每人分得" + salary / 3 + "元";
                lblMsgSalary.Visible = true;
            }
            else
            {
                lblMsgSalary.Visible = false;
            }
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            this.t_DiaryTableAdapter.Fill(this.iFC20110208DataSet4.T_Diary);
            this.t_DormitoryTableAdapter.Fill(this.iFC20110208DataSet3.T_Dormitory);
            this.t_CountTableAdapter.Fill(this.iFC20110208DataSet2.T_Count);
            this.t_CostTableAdapter.Fill(this.iFC20110208DataSet1.T_Cost);
            this.t_PayTableAdapter.Fill(this.iFC20110208DataSet.T_Pay);

            int Fee;
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
                            Fee = reader.GetInt32(reader.GetOrdinal("DBalance"));
                            lblFee.Text = Fee.ToString() + "元";
                            if (Fee < 0)
                            {
                                lblFee.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
        }
    }
}
