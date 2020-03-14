using System;
using System.Windows.Forms;

namespace IFC20110208
{
    public partial class frmLogin : Form
    {
        frmMain frmContent = new frmMain();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "523009")
            {
                frmContent.Visible = true;
                this.Hide();
            }
            else
            {
                DialogResult result=MessageBox.Show("您输入的口令错误！","系统警告",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                if (result == DialogResult.Cancel)
                {
                    Application.Exit();
                }
                btnLogin.Visible = false;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            frmContent.Show();
            frmContent.Visible = false;
            btnLogin.Visible = false;

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.TextLength > 5)
            {
                btnLogin.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
