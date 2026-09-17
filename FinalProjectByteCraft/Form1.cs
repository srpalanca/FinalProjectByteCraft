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

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        // LOGIN BUTTON CLICK
        private void button1_Click(object sender, EventArgs e)
        {
            // Read customer name from textBox1
            string username = textBox1.Text.Trim();

            // Validate that the name is not empty
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter your name to access the PC Builder.", "Login Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pass customer name into Form2 dashboard
            Form2 dashboard = new Form2(username);
            dashboard.Show();
            this.Hide(); // Hide Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();    
        }
    }
}