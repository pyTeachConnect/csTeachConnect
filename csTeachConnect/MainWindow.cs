using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace csTeachConnect
{
    public partial class MainWindow : Form
    {
        private string runningPath;
        private string userInfoFilePath;
        private string ipStorageFilePath;
        private string nameStorageFilePath;
        private string logPath;
        private string cachePath;

        public MainWindow()
        {
            InitializeComponent();
            
            // 设置窗体图标
            // 开发中...



            runningPath = Path.GetDirectoryName(Application.ExecutablePath);
            userInfoFilePath = Path.Combine(ApplicationDataPath, "User", "UserInfo.json");
            ipStorageFilePath = Path.Combine(ApplicationDataPath, "cache", "IPs.json");
            nameStorageFilePath = Path.Combine(ApplicationDataPath, "cache", "Names.json");
            logPath = Path.Combine(ApplicationDataPath, "log");
            cachePath = Path.Combine(ApplicationDataPath, "cache");

            Directory.CreateDirectory(Path.Combine(ApplicationDataPath, "User"));
            Directory.CreateDirectory(logPath);
            Directory.CreateDirectory(cachePath);
        }

        public static string ApplicationDataPath
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TConect"); }
        }

        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static Dictionary<string, string> LoadRecentData(string filepath)
        {
            if (File.Exists(filepath))
            {
                var data = File.ReadAllText(filepath);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            }
            return new Dictionary<string, string>();
        }

        public static void SaveRecentData(string filepath, Dictionary<string, string> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var users = LoadRecentData(userInfoFilePath);
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text.Trim();

            var today = DateTime.Today;
            var dateStr = today.ToString("ddMMyyyy");

            if (username == "admin" && password == dateStr)
            {
                this.Hide();
                var adminForm = new AdminMode();
                adminForm.ShowDialog();
                this.Close();
            }
            else if (users.ContainsKey(username) && users[username] == ComputeSha256Hash(password))
            {
                this.Hide();
                var messagingApp = new MessagingApp(username);
                messagingApp.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var users = LoadRecentData(userInfoFilePath);
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名和密码不能为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!users.ContainsKey(username))
            {
                users[username] = ComputeSha256Hash(password);
                SaveRecentData(userInfoFilePath, users);
                MessageBox.Show("注册成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRegister.Text = "已注册";
            }
            else
            {
                MessageBox.Show("用户名已存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var users = LoadRecentData(userInfoFilePath);
            btnRegister.Text = users.Any() ? "已注册" : "注册";
        }
    }
}
