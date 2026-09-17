using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinalProjectByteCraft
{
    public partial class Form2 : Form
    {
        private string customerName;

        // Default constructor (Required by Visual Studio Designer)
        public Form2()
        {
            InitializeComponent();
        }

        // Constructor that receives customer name from Form1
        public Form2(string name)
        {
            InitializeComponent();
            customerName = name;
        }

        // Code that runs when Form2 loads
        private void Form2_Load(object sender, EventArgs e)
        {
            // Set form header text with customer name
            this.Text = $"ByteCraft Systems - PC Builder | Welcome, {customerName}";

            // Load default panel view on startup
            ShowPanel(panelBuildPC);
        }

        // HELPER METHOD: Hides all panels and shows only the selected panel
        private void ShowPanel(Panel selectedPanel)
        {
            // Hide all panels if they exist in designer
            if (panelBuildPC != null) panelBuildPC.Visible = false;
            if (panelCatalog != null) panelCatalog.Visible = false;
            if (panelCart != null) panelCart.Visible = false;
            if (panelReceipt != null) panelReceipt.Visible = false;

            // Show and bring chosen panel to the top layer
            if (selectedPanel != null)
            {
                selectedPanel.Visible = true;
                selectedPanel.BringToFront();
            }
        }

        // TOP-RIGHT RED 'X' CLOSE BUTTON
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // BUTTON 1: BUILD PC
        private void button1_Click(object sender, EventArgs e)
        {
            ShowPanel(panelBuildPC);
        }

        // BUTTON 3: PRODUCT CATALOG
        private void button3_Click(object sender, EventArgs e)
        {
            ShowPanel(panelCatalog);
        }

        // BUTTON 4: MY CART
        private void button4_Click(object sender, EventArgs e)
        {
            ShowPanel(panelCart);
        }

        // BUTTON 5: RECEIPT
        private void button5_Click(object sender, EventArgs e)
        {
            ShowPanel(panelReceipt);
        }

        // BUTTON 6: LOGOUT
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Logout Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Hide(); // Hide dashboard and open Form1 login screen
            }
        }

        // Ensures the entire application terminates if closed via taskbar
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}