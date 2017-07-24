using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Board
{
    public partial class Open : Form
    {
        bool file_exist = false;
        DirectoryInfo myDir = new DirectoryInfo(Application.StartupPath + "\\save");
        List<string> fileList = new List<string>();

        public bool File_exist
        {
            get
            {
                return file_exist;
            }

            set
            {
                file_exist = value;
            }
        }

        public DirectoryInfo MyDir
        {
            get
            {
                return myDir;
            }

            set
            {
                myDir = value;
            }
        }

        public List<string> FileList
        {
            get
            {
                return fileList;
            }

            set
            {
                fileList = value;
            }
        }

        public Open()
        {
            InitializeComponent();
            if (MyDir.Exists)
            {
                foreach (FileInfo file in MyDir.GetFiles())
                {
                    File_exist = true;
                    FileList.Add(file.Name.Substring(0, file.Name.Length - 4));
                }
                if (File_exist)
                {
                    listBox1.DataSource = FileList;
                }
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.cCancel_MouseOver;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.cCancel;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.Play_MouseOver;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Board.Properties.Resources.Play;
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            VanCo.Open(Application.StartupPath + "\\save\\" + Convert.ToString(listBox1.SelectedValue) + ".ccb");
            VanCo.DangChoi = true;
            this.Close();
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Board.Properties.Resources.Del_MouseOver;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Board.Properties.Resources.Del;
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            FileInfo fileDel = new FileInfo(Application.StartupPath + "\\save\\" + Convert.ToString(listBox1.SelectedValue) + ".ccb");
            List<string> newfileList = new List<string>();
            fileDel.Delete();
            if (MyDir.Exists)
            {
                foreach (FileInfo file in MyDir.GetFiles())
                {
                    File_exist = true;
                    newfileList.Add(file.Name.Substring(0, file.Name.Length - 4));
                }
                if (File_exist)
                {
                    listBox1.DataSource = newfileList;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VanCo.Open(Application.StartupPath + "\\save\\" + Convert.ToString(listBox1.SelectedValue) + ".ccb");
            VanCo.DangChoi = true;
            this.Close();
        }
    }
}