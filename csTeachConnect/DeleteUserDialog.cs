using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace csTeachConnect
{
    public partial class DeleteUserDialog : Form
    {
        public string Username { get; private set; }
        private string userInfoFilePath; // 添加 userInfoFilePath 字段

        public DeleteUserDialog()
        {
            InitializeComponent();

            userInfoFilePath = Path.Combine(MainWindow.ApplicationDataPath, "User", "UserInfo.json");
            LoadUsernames();
        }

        private void LoadUsernames()
        {
            var users = MainWindow.LoadRecentData(userInfoFilePath);
            foreach (var username in users.Keys)
            {
                cmbUsername.Items.Add(username);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbUsername.SelectedItem != null)
            {
                Username = cmbUsername.SelectedItem.ToString();

                if (string.IsNullOrEmpty(Username))
                {
                    MessageBox.Show("请选择一个用户名！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
