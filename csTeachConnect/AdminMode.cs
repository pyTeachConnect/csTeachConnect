using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace csTeachConnect
{
    public partial class AdminMode : Form
    {
        private string userInfoFilePath;

        public AdminMode()
        {
            InitializeComponent();

            userInfoFilePath = Path.Combine(MainWindow.ApplicationDataPath, "User", "UserInfo.json");
        }

        private void btnShowUserInfo_Click(object sender, EventArgs e)
        {
            var users = MainWindow.LoadRecentData(userInfoFilePath);
            if (users.Count > 0)
            {
                string userInfo = string.Join(Environment.NewLine, users.Select(kv => $"{kv.Key}: {kv.Value}"));
                MessageBox.Show(userInfo, "用户信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("没有用户信息！", "用户信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (var addDialog = new AddUserDialog())
            {
                if (addDialog.ShowDialog() == DialogResult.OK)
                {
                    var username = addDialog.Username;
                    var password = addDialog.Password;

                    var users = MainWindow.LoadRecentData(userInfoFilePath);
                    if (!users.ContainsKey(username))
                    {
                        users[username] = MainWindow.ComputeSha256Hash(password);
                        MainWindow.SaveRecentData(userInfoFilePath, users);
                        MessageBox.Show("添加用户成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("用户名已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            using (var deleteDialog = new DeleteUserDialog())
            {
                if (deleteDialog.ShowDialog() == DialogResult.OK)
                {
                    var username = deleteDialog.Username;

                    var users = MainWindow.LoadRecentData(userInfoFilePath);
                    if (users.ContainsKey(username))
                    {
                        users.Remove(username);
                        MainWindow.SaveRecentData(userInfoFilePath, users);
                        MessageBox.Show("删除用户成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("用户名不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
