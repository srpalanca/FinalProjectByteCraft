using System;
using System.Windows.Forms;

namespace FinalProjectByteCraft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username cannot be empty.", "Login Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form2 dashboard = new Form2(username);
            dashboard.Show();
            this.Hide(); 
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();  
        }


        private void label1_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}