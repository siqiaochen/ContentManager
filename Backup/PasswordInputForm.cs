using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LocalPlayer.TCPConnection;

namespace LocalPlayer
{
    public partial class PasswordInputForm : Form
    {
        public PasswordInputForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            TcpPlayerClient tcp = new TcpPlayerClient(textBoxIP.Text,(int)numericUpDownPort.Value);
            tcp.User = textBoxPlayer.Text;
            tcp.Password = textBoxPass.Text;
            tcp.Connect();
            tcp.Login();
            tcp.Release();

        }
    }
}
