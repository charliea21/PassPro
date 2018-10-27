using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace PassPro
{
    public partial class Form1 : Form
    {

        private string CurrentDirectory = Directory.GetCurrentDirectory() + "\\";

        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Maximum = int.MaxValue;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PassPro - V1.0.0", "About - PassPro", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string PASSWORD = string.Empty;

            Random rd = new Random();


            if (MessageBox.Show("Would you like to generate a complex password? (Reccomended)", "Confirmation - PassPro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                char[] dc = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
'1','2','3','4','5','6','7','8','9','0', '!', '#', '~','¬', '|', '/', '`', '\\'};

                ((int)(numericUpDown1.Value)).Times(() =>
                {
                    int cv = rd.Next(1, 3);

                    if (cv == 1)
                    {
                        PASSWORD = PASSWORD + dc[rd.Next(0, dc.Length)].ToString().ToUpper();
                    } else if (cv == 2)
                    {
                        PASSWORD = PASSWORD + dc[rd.Next(0, dc.Length)].ToString().ToLower();
                    } else
                    {
                        PASSWORD = PASSWORD + dc[rd.Next(0, dc.Length)].ToString();
                    }
                });
            } else
            {
                char[] dc = {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
'1','2','3','4','5','6','7','8','9','0'};

                ((int)(numericUpDown1.Value)).Times(() =>
                {
                    PASSWORD = PASSWORD + dc[rd.Next(0, dc.Length)].ToString();
                });
            }

            

            

            textBox1.Text = PASSWORD;
            MessageBox.Show("Password Generated", "Sucess - PassPro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(textBox1.Text);
                MessageBox.Show("Copied to clipboard!", "Sucess - PassPro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch
            {
                MessageBox.Show("An error occured!", "Error - PassPro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.AddExtension = true;
            openFileDialog1.Filter = "Password Package (*.ppkg)|*.ppkg";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() => MessageBox.Show("Checking your password against 1 million common ones. Please wait. You will be prompted once your password has been checked.", "Important - PassPro", MessageBoxButtons.OK, MessageBoxIcon.Information));
            t.Start();

            string URL = "https://raw.githubusercontent.com/danielmiessler/SecLists/master/Passwords/Common-Credentials/10-million-password-list-top-1000000.txt";

            try
            {
                using (WebClient wc = new WebClient())
                {
                    string top = wc.DownloadString(URL);
                    string[] lines = top.Split(
        new[] { "\r\n", "\r", "\n" },
        StringSplitOptions.None
    );
                    bool pfound = false;

                    foreach (string pass in lines)
                    {
                        if (pass.ToLower() == textBox1.Text.ToLower())
                        {
                            pfound = true;
                            MessageBox.Show("Your password is common!", "Error - PassPro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        }
                    }

                    if (!pfound)
                        MessageBox.Show("Your password is not common!", "Sucess - PassPro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            } catch
            {
                MessageBox.Show("An unknown error occured! Check your internet connection!", "Error - PassPro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
