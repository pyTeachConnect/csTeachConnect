using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text; // 添加对 System.Text 命名空间的引用

namespace csTeachConnect
{
    public partial class MessagingApp : Form
    {
        private string username;
        private string ipStorageFilePath;
        private string nameStorageFilePath;

        private Dictionary<string, string> recentIps;
        private Dictionary<string, string> recentNames; // 修改为 Dictionary<string, string>

        public MessagingApp(string username)
        {
            InitializeComponent();
            
            // 设置窗体图标
            // 开发中...

            this.username = username;
            ipStorageFilePath = Path.Combine(MainWindow.ApplicationDataPath, "cache", "IPs.json");
            nameStorageFilePath = Path.Combine(MainWindow.ApplicationDataPath, "cache", "Names.json");

            LoadRecentData();
        }

        private void LoadRecentData()
        {
            recentIps = MainWindow.LoadRecentData(ipStorageFilePath);
            recentNames = MainWindow.LoadRecentData(nameStorageFilePath);

            cmbName.Items.Clear();
            cmbName.Items.AddRange(recentNames.Keys.ToArray());
            cmbName.SelectedIndex = -1;

            cmbIp.Items.Clear();
            foreach (var kv in recentIps)
            {
                cmbIp.Items.Add($"{kv.Value} - {kv.Key}");
            }
            cmbIp.SelectedIndex = -1;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string name = cmbName.Text.Trim();
            string ipWithNote = cmbIp.Text.Trim();
            string message = txtMessage.Text.Trim();
            string portOption = cmbPort.SelectedItem.ToString();

            int port = portOption switch
            {
                "普通提醒" => 11224,
                "明显提醒" => 11223,
                "混合提醒（教室端版本v0.3.0-Beta及以上）" => 11224,
                _ => 11224
            };

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ipWithNote) || string.IsNullOrEmpty(message))
            {
                MessageBox.Show("所有字段都必须填写！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string note, ip;
                if (ipWithNote.Contains(" - "))
                {
                    var parts = ipWithNote.Split(" - ", 2);
                    note = parts[0];
                    ip = parts[1];
                }
                else if (ipWithNote.Contains("-"))
                {
                    var parts = ipWithNote.Split("-", 2);
                    note = parts[0];
                    ip = parts[1];
                }
                else
                {
                    MessageBox.Show("IP 格式无效！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!recentNames.ContainsKey(name))
                {
                    recentNames[name] = "true"; // 修改为字符串 "true"
                    MainWindow.SaveRecentData(nameStorageFilePath, recentNames);
                }

                recentIps[ip] = note;
                MainWindow.SaveRecentData(ipStorageFilePath, recentIps);

                var data = JsonConvert.SerializeObject(new { name = name, message = message });
                using (var s = new TcpClient())
                {
                    s.Connect(ip, port);
                    using (var stream = s.GetStream())
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(data); // 使用 Encoding.UTF8
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                MessageBox.Show("消息已成功发送！", "发送成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送失败：请检查网络连接或目标教室未启动程序\nTips: 请尝试使用明显提醒或普通提醒模式\n\n错误信息: {ex.Message}", "发送失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
