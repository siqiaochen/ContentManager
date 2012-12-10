using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LocalPlayer.TCPConnection;
using LocalPlayer.WCFCConnection;
using LocalPlayer.PlayElements;
namespace LocalPlayer
{
    public partial class PasswordInputForm : Form
    {
        public PasswordInputForm()
        {
            InitializeComponent();
            AccountHelper accounthelper = new AccountHelper();
            if (accounthelper.ReadFromFile())
            {
                textBoxPlayer.Text = accounthelper.Account;
                textBoxPass.Text = accounthelper.Password;
            }


        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            WCFClient client = new WCFClient(textBoxPlayer.Text,textBoxPass.Text);
            int count = client.CheckScheduleCount();
            List<Schedule> schedules = new List<Schedule>();
            if (count >= 0)
            {
                for (int i = 0; i < count; i++)
                {

                    Schedule sche = client.CheckSchedule(i);
                    if (sche != null)
                        schedules.Add(sche);
                }
                MessageBox.Show("Test Succeed!", "Result");
            }
            else
                MessageBox.Show("Test Failed!", "Result");
        }

        private void btnSet_Click(object sender, EventArgs e)
        {

            AccountHelper accounthelper = new AccountHelper();
            accounthelper.Account = textBoxPlayer.Text;
            accounthelper.Password = textBoxPass.Text;
            accounthelper.WriteToFile();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
