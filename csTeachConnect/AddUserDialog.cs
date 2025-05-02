using System;
using System.Windows.Forms;

namespace csTeachConnect
{
    public partial class AddUserDialog : Form
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public AddUserDialog()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Username = txtUsername.Text.Trim();
            Password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("用户名和密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
