using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD
{
    public partial class frmLogin : Form
    {


        public frmLogin()
        {
            InitializeComponent();
          
        }

        public delegate void sendUserHandler(object sender, DataTable user);
        public event sendUserHandler sendUser;

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string userName = tbUsername.Text;
            string password = tbPassword.Text;

            bool isValidUser= DVLDBusinessLayer.clsManageUsers.isUserExist(userName,password);

            if (isValidUser)
            {
                if (cbRemeberMe.Checked)
                    saveLogInInfo();
                else
                    deleteLogInInfo();

                DataTable user = DVLDBusinessLayer.clsManageUsers.retrieveUser(userName);
                sendUser?.Invoke(this, user);
                this.DialogResult = DialogResult.OK;
                this.Close();
               return;
            }
            this.DialogResult = DialogResult.Cancel;
            MessageBox.Show("wrong username or password");
        }
        private void deleteLogInInfo()
        {
            string filePath = Path.Combine(Application.StartupPath, "loginInfo.txt");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                
            }
        }
        private void saveLogInInfo()
        {
            string filePath = Path.Combine(Application.StartupPath, "loginInfo.txt");

        using(StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(tbUsername.Text);
                writer.WriteLine(tbPassword.Text);
            }


        }

        private void retrieveLogInInfo()
        {
            string filePath = Path.Combine(Application.StartupPath, "loginInfo.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length >= 2)
                {
                    tbUsername.Text = lines[0];
                    tbPassword.Text = lines[1];      
                }
            }
        }

        private bool isRemebmberMeChecked()
        {
            string filePath = Path.Combine(Application.StartupPath, "loginInfo.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length !=0)
                {
                    return true;
                }
                
            }
            return false;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            if(isRemebmberMeChecked())
            {
                cbRemeberMe.Checked = true;
                retrieveLogInInfo();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;

            Environment.Exit(0);

        }
    }
}
