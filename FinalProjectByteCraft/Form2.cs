using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FinalProjectByteCraft
{
    public partial class Form2 : Form
    {
        public class CartItem
        {
            public string Category { get; set; }
            public string ItemName { get; set; }
            public decimal Price { get; set; }

            public override string ToString()
            {
                if (Category == "SET_HEADER")
                {
                    return ItemName;
                }
                return $"   [{Category}] {ItemName} - ₱{Price:N2}";
            }
        }

        private string customerName;
        private int buildSetCount = 0;
        private bool isUpdatingCheck = false;

        private Dictionary<string, decimal> cpuPrices = new Dictionary<string, decimal>()
        {
            { "Intel Core i9-12900", 30000.00m },
            { "Intel Core i9-13900", 35000.00m },
            { "Intel Core i9-14900", 45000.00m },
            { "Intel Core i7-12700", 20500.00m },
            { "Intel Core i7-13700", 25000.00m },
            { "Intel Core i7-14700", 27000.00m },
            { "Intel Core i5-12500", 11500.00m },
            { "Intel Core i5-13500", 13000.00m },
            { "Intel Core i5-14500", 17000.00m }
        };

        private Dictionary<string, decimal> gpuPrices = new Dictionary<string, decimal>()
        {
            { "NVIDIA GeForce RTX 5060 8GB", 28000.00m },
            { "NVIDIA GeForce RTX 5090 32GB", 180000.00m },
            { "NVIDIA GeForce RTX 4060 8GB", 25000.00m },
            { "NVIDIA GeForce RTX 4090 24GB", 120000.00m },
            { "NVIDIA GeForce RTX 3060 12GB", 23500.00m },
            { "NVIDIA GeForce RTX 3090 24GB", 65000.00m }
        };

        private Dictionary<string, decimal> ramPrices = new Dictionary<string, decimal>()
        {
            { "16GB DDR5 5000MHz", 3500.00m },
            { "16GB DDR5 6000MHz", 4500.00m },
            { "16GB DDR5 7000MHz", 6000.00m },
            { "16GB DDR5 8000MHz", 8000.00m }
        };

        private Dictionary<string, decimal> storagePrices = new Dictionary<string, decimal>()
        {
            { "500GB HDD", 1500.00m },
            { "1TB HDD", 2800.00m }
        };

        private Dictionary<string, decimal> psuPrices = new Dictionary<string, decimal>()
        {
            { "500W 80+ Silver PSU", 2500.00m },
            { "750W 80+ Gold PSU", 4000.00m },
            { "1000W 80+ Platinum PSU", 7000.00m }
        };

        private Dictionary<string, decimal> coolerPrices = new Dictionary<string, decimal>()
        {
            { "Air Cooler", 1000.00m },
            { "Liquid Cooler", 3500.00m }
        };

        private List<CartItem> shoppingCart = new List<CartItem>();

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string name)
        {
            InitializeComponent();
            customerName = name;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializePCBuilderOptions();
            WireUpEvents();
            ShowPanel(panelBuildPC);
        }

        private void WireUpEvents()
        {
            if (cmbCPU != null) 
            { 
                cmbCPU.SelectedIndexChanged -= cmbCPU_SelectedIndexChanged; 
                cmbCPU.SelectedIndexChanged += cmbCPU_SelectedIndexChanged; 
            }
            
            if (cmbGPU != null) 
            { 
                cmbGPU.SelectedIndexChanged -= cmbGPU_SelectedIndexChanged; 
                cmbGPU.SelectedIndexChanged += cmbGPU_SelectedIndexChanged; 
            }
            
            if (cmbRAM != null) 
            { 
                cmbRAM.SelectedIndexChanged -= cmbRAM_SelectedIndexChanged;
                cmbRAM.SelectedIndexChanged += cmbRAM_SelectedIndexChanged; 
            }

            if (cmbStorage != null) 
            { 
                cmbStorage.SelectedIndexChanged -= cmbStorage_SelectedIndexChanged; 
                cmbStorage.SelectedIndexChanged += cmbStorage_SelectedIndexChanged; 
            }

            if (cmbPSU != null) 
            { 
                cmbPSU.SelectedIndexChanged -= cmbPSU_SelectedIndexChanged;
                cmbPSU.SelectedIndexChanged += cmbPSU_SelectedIndexChanged; 
            }

            if (cmbCooler != null) 
            { 
                cmbCooler.SelectedIndexChanged -= cmbCooler_SelectedIndexChanged; 
                cmbCooler.SelectedIndexChanged += cmbCooler_SelectedIndexChanged; 
            }

            if (btnAddToCartBuild != null) 
            { 
                btnAddToCartBuild.Click -= btnAddToCartBuild_Click; 
                btnAddToCartBuild.Click += btnAddToCartBuild_Click; 
            }
           
            if (btnClearSelections != null) 
            { 
                btnClearSelections.Click -= btnClearSelections_Click; 
                btnClearSelections.Click += btnClearSelections_Click; 
            }

            if (btnAutoBuild != null) 
            { 
                btnAutoBuild.Click -= btnAutoBuild_Click;
                btnAutoBuild.Click += btnAutoBuild_Click; 
            }

            if (btnClearCart != null) 
            { 
                btnClearCart.Click -= btnClearCart_Click; 
                btnClearCart.Click += btnClearCart_Click; 
            }

            if (btnCheckout != null) 
            { 
                btnCheckout.Click -= btnCheckout_Click; 
                btnCheckout.Click += btnCheckout_Click; 
            }

            if (listCartItems != null)
            {
                listCartItems.ItemCheck -= listCartItems_ItemCheck;
                listCartItems.ItemCheck += listCartItems_ItemCheck;
            }

            if (btnGoToBuilder != null) 
            { 
                btnGoToBuilder.Click -= btnGoToBuilder_Click; 
                btnGoToBuilder.Click += btnGoToBuilder_Click; 
            }

            if (btnBackToBuilder != null) 
            { 
                btnBackToBuilder.Click -= btnBackToBuilder_Click; 
                btnBackToBuilder.Click += btnBackToBuilder_Click; 
            }
        }

        private void InitializePCBuilderOptions()
        {
            cmbCPU.Items.Clear();
            cmbCPU.Items.Add("Select CPU...");
            foreach (var kvp in cpuPrices) cmbCPU.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbCPU.SelectedIndex = 0;

            cmbGPU.Items.Clear();
            cmbGPU.Items.Add("Select GPU...");
            foreach (var kvp in gpuPrices) cmbGPU.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbGPU.SelectedIndex = 0;

            cmbRAM.Items.Clear();
            cmbRAM.Items.Add("Select RAM...");
            foreach (var kvp in ramPrices) cmbRAM.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbRAM.SelectedIndex = 0;

            cmbStorage.Items.Clear();
            cmbStorage.Items.Add("Select Storage...");
            foreach (var kvp in storagePrices) cmbStorage.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbStorage.SelectedIndex = 0;

            cmbPSU.Items.Clear();
            cmbPSU.Items.Add("Select Power Supply...");
            foreach (var kvp in psuPrices) cmbPSU.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbPSU.SelectedIndex = 0;

            cmbCooler.Items.Clear();
            cmbCooler.Items.Add("Select Cooler...");
            foreach (var kvp in coolerPrices) cmbCooler.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbCooler.SelectedIndex = 0;

            UpdateTotalPrice();
        }

        private Image LoadResourceImage(string fileName)
        {
            string path = Path.Combine(Application.StartupPath, "Resources", fileName);
            if (File.Exists(path)) return Image.FromFile(path);

            string altPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
            if (File.Exists(altPath)) return Image.FromFile(altPath);

            return null;
        }

        private void btnClearSelections_Click(object sender, EventArgs e)
        {
            if (cmbCPU != null) cmbCPU.SelectedIndex = 0;
            if (cmbGPU != null) cmbGPU.SelectedIndex = 0;
            if (cmbRAM != null) cmbRAM.SelectedIndex = 0;
            if (cmbStorage != null) cmbStorage.SelectedIndex = 0;
            if (cmbPSU != null) cmbPSU.SelectedIndex = 0;
            if (cmbCooler != null) cmbCooler.SelectedIndex = 0;

            UpdateTotalPrice();
        }

        private void cmbCPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbCPU.SelectedIndex)
            {
                case 1: case 2: case 3: picCPU.Image = LoadResourceImage("i9.png"); break;
                case 4: case 5: case 6: picCPU.Image = LoadResourceImage("i7.png"); break;
                case 7: case 8: case 9: picCPU.Image = LoadResourceImage("i5.png"); break;
                default: picCPU.Image = null; break;
            }
            UpdateTotalPrice();
        }

        private void cmbGPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbGPU.SelectedIndex)
            {
                case 1: picGPU.Image = LoadResourceImage("5060.jpg"); break;
                case 2: picGPU.Image = LoadResourceImage("5090.png"); break;
                case 3: picGPU.Image = LoadResourceImage("4060.png"); break;
                case 4: picGPU.Image = LoadResourceImage("4090.png"); break;
                case 5: picGPU.Image = LoadResourceImage("3060.jpg"); break;
                case 6: picGPU.Image = LoadResourceImage("3090.png"); break;
                default: picGPU.Image = null; break;
            }
            UpdateTotalPrice();
        }

        private void cmbRAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            picRAM.Image = (cmbRAM.SelectedIndex > 0) ? LoadResourceImage("ram.png") : null;
            UpdateTotalPrice();
        }

        private void cmbStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbStorage.SelectedIndex)
            {
                case 1: picStorage.Image = LoadResourceImage("500.png"); break;
                case 2: picStorage.Image = LoadResourceImage("1TB.png"); break;
                default: picStorage.Image = null; break;
            }
            UpdateTotalPrice();
        }

        private void cmbPSU_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbPSU.SelectedIndex)
            {
                case 1: picPSU.Image = LoadResourceImage("silver.png"); break;
                case 2: picPSU.Image = LoadResourceImage("gold.png"); break;
                case 3: picPSU.Image = LoadResourceImage("platinum.png"); break;
                default: picPSU.Image = null; break;
            }
            UpdateTotalPrice();
        }

        private void cmbCooler_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbCooler.SelectedIndex)
            {
                case 1: picCooler.Image = LoadResourceImage("air.png"); break;
                case 2: picCooler.Image = LoadResourceImage("liquid.jpg"); break;
                default: picCooler.Image = null; break;
            }
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            decimal total = 0;
            total += GetPriceFromSelection(cmbCPU);
            total += GetPriceFromSelection(cmbGPU);
            total += GetPriceFromSelection(cmbRAM);
            total += GetPriceFromSelection(cmbStorage);
            total += GetPriceFromSelection(cmbPSU);
            total += GetPriceFromSelection(cmbCooler);

            lblEstimatedTotal.Text = $"Estimated Total: ₱{total:N2}";
        }

        private decimal GetPriceFromSelection(ComboBox cmb)
        {
            if (cmb == null || cmb.SelectedIndex <= 0) return 0m;

            string selectedText = cmb.SelectedItem.ToString();
            int dashIndex = selectedText.IndexOf(" - ₱");
            string itemName = (dashIndex != -1) ? selectedText.Substring(0, dashIndex).Trim() : selectedText.Trim();

            if (cmb == cmbCPU && cpuPrices.ContainsKey(itemName)) return cpuPrices[itemName];
            if (cmb == cmbGPU && gpuPrices.ContainsKey(itemName)) return gpuPrices[itemName];
            if (cmb == cmbRAM && ramPrices.ContainsKey(itemName)) return ramPrices[itemName];
            if (cmb == cmbStorage && storagePrices.ContainsKey(itemName)) return storagePrices[itemName];
            if (cmb == cmbPSU && psuPrices.ContainsKey(itemName)) return psuPrices[itemName];
            if (cmb == cmbCooler && coolerPrices.ContainsKey(itemName)) return coolerPrices[itemName];

            return 0m;
        }

        private void btnAutoBuild_Click(object sender, EventArgs e)
        {
            string input = ShowInputDialog("ByteCraft Auto-Budget Builder", "Enter your maximum budget in Pesos (₱):");

            if (string.IsNullOrWhiteSpace(input)) return;

            if (!decimal.TryParse(input, out decimal maxBudget))
            {
                MessageBox.Show("Please enter a valid numeric budget.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KeyValuePair<string, decimal> bestCpu = default;
            KeyValuePair<string, decimal> bestGpu = default;
            KeyValuePair<string, decimal> bestRam = default;
            KeyValuePair<string, decimal> bestStorage = default;
            KeyValuePair<string, decimal> bestPsu = default;
            KeyValuePair<string, decimal> bestCooler = default;

            decimal bestTotal = -1;

            foreach (var cpu in cpuPrices)
            {
                foreach (var gpu in gpuPrices)
                {
                    foreach (var ram in ramPrices)
                    {
                        foreach (var storage in storagePrices)
                        {
                            foreach (var psu in psuPrices)
                            {
                                foreach (var cooler in coolerPrices)
                                {
                                    decimal currentTotal = cpu.Value + gpu.Value + ram.Value + storage.Value + psu.Value + cooler.Value;

                                    if (currentTotal <= maxBudget && currentTotal > bestTotal)
                                    {
                                        bestTotal = currentTotal;
                                        bestCpu = cpu;
                                        bestGpu = gpu;
                                        bestRam = ram;
                                        bestStorage = storage;
                                        bestPsu = psu;
                                        bestCooler = cooler;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (bestTotal == -1)
            {
                MessageBox.Show($"Sorry, your budget of ₱{maxBudget:N2} is too low for our cheapest PC Components!", "Budget Too Low", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectMatchingComboItem(cmbCPU, bestCpu.Key);
            SelectMatchingComboItem(cmbGPU, bestGpu.Key);
            SelectMatchingComboItem(cmbRAM, bestRam.Key);
            SelectMatchingComboItem(cmbStorage, bestStorage.Key);
            SelectMatchingComboItem(cmbPSU, bestPsu.Key);
            SelectMatchingComboItem(cmbCooler, bestCooler.Key);
        }

        private string ShowInputDialog(string title, string promptText)
        {
            Form prompt = new Form()
            {
                Width = 420,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = promptText, Width = 360, Font = new Font("Segoe UI", 10) };
            TextBox textBox = new TextBox() { Left = 20, Top = 55, Width = 360, Text = "50000", Font = new Font("Segoe UI", 10) };
            Button confirmation = new Button() { Text = "OK", Left = 290, Width = 90, Top = 95, DialogResult = DialogResult.OK, Font = new Font("Segoe UI", 9, FontStyle.Bold) };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void SelectMatchingComboItem(ComboBox cmb, string itemName)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (cmb.Items[i].ToString().StartsWith(itemName))
                {
                    cmb.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btnAddToCartBuild_Click(object sender, EventArgs e)
        {
            ComboBox[] partBoxes = { cmbCPU, cmbGPU, cmbRAM, cmbStorage, cmbPSU, cmbCooler };
            string[] categories = { "CPU", "GPU", "RAM", "Storage", "Power Supply", "Cooler" };

            int itemsAdded = 0;
            foreach (var box in partBoxes)
            {
                if (box != null && box.SelectedIndex > 0) itemsAdded++;
            }

            if (itemsAdded == 0)
            {
                MessageBox.Show("Please select at least one component before adding to cart.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            buildSetCount++;

            shoppingCart.Add(new CartItem
            {
                Category = "SET_HEADER",
                ItemName = $"[Custom PC Build Set #{buildSetCount}]",
                Price = 0m
            });

            for (int i = 0; i < partBoxes.Length; i++)
            {
                if (partBoxes[i] != null && partBoxes[i].SelectedIndex > 0)
                {
                    decimal price = GetPriceFromSelection(partBoxes[i]);
                    string fullText = partBoxes[i].SelectedItem.ToString();
                    string itemName = fullText.Substring(0, fullText.IndexOf(" - ₱"));

                    shoppingCart.Add(new CartItem
                    {
                        Category = categories[i],
                        ItemName = itemName,
                        Price = price
                    });
                }
            }

            RefreshCartUI();
            MessageBox.Show($"Custom Build Set #{buildSetCount} added to your cart successfully!", "Cart Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshCartUI()
        {
            bool isEmpty = (shoppingCart.Count == 0);

            if (lblEmptyCart != null) lblEmptyCart.Visible = isEmpty;
            if (btnGoToBuilder != null) btnGoToBuilder.Visible = isEmpty;
            if (listCartItems != null) listCartItems.Visible = !isEmpty;
            if (btnClearCart != null) btnClearCart.Visible = !isEmpty;
            if (btnCheckout != null) btnCheckout.Visible = !isEmpty;
            if (lblCartTotalPrice != null) lblCartTotalPrice.Visible = !isEmpty;

            if (!isEmpty && listCartItems != null)
            {
                listCartItems.Items.Clear();
                decimal cartTotal = 0;

                foreach (CartItem item in shoppingCart)
                {
                    listCartItems.Items.Add(item);
                    cartTotal += item.Price;
                }

                if (lblCartTotalPrice != null)
                {
                    lblCartTotalPrice.Text = $"Total: ₱{cartTotal:N2}";
                }
            }
            else if (lblCartTotalPrice != null)
            {
                lblCartTotalPrice.Text = "Total: ₱0.00";
            }
        }

        private void listCartItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isUpdatingCheck) return;

            if (listCartItems.Items[e.Index] is CartItem clickedItem)
            {
                isUpdatingCheck = true;

                if (clickedItem.Category == "SET_HEADER")
                {
                    bool targetCheckState = (e.NewValue == CheckState.Checked);
                    int currentIndex = e.Index + 1;

                    while (currentIndex < listCartItems.Items.Count)
                    {
                        if (listCartItems.Items[currentIndex] is CartItem nextItem)
                        {
                            if (nextItem.Category == "SET_HEADER") break;
                            listCartItems.SetItemChecked(currentIndex, targetCheckState);
                        }
                        currentIndex++;
                    }
                }
                else
                {
                    int headerIndex = -1;
                    for (int i = e.Index; i >= 0; i--)
                    {
                        if (listCartItems.Items[i] is CartItem item && item.Category == "SET_HEADER")
                        {
                            headerIndex = i;
                            break;
                        }
                    }

                    if (headerIndex != -1)
                    {
                        bool anyItemChecked = false;
                        int checkIndex = headerIndex + 1;

                        while (checkIndex < listCartItems.Items.Count)
                        {
                            if (listCartItems.Items[checkIndex] is CartItem nextItem && nextItem.Category == "SET_HEADER")
                            {
                                break;
                            }

                            bool isChecked = (checkIndex == e.Index) ? (e.NewValue == CheckState.Checked) : listCartItems.GetItemChecked(checkIndex);

                            if (isChecked)
                            {
                                anyItemChecked = true;
                                break;
                            }
                            checkIndex++;
                        }

                        listCartItems.SetItemChecked(headerIndex, anyItemChecked);
                    }
                }

                isUpdatingCheck = false;
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            shoppingCart.Clear();
            buildSetCount = 0;
            RefreshCartUI();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (listCartItems.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please check the boxes of the PC sets or items you want to check out.", "No Items Checked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal checkedTotal = 0;
            string  receiptText = $"Customer: {customerName}\r\n";
            receiptText += $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\n";

            if (listReceiptItems != null)
            {
                listReceiptItems.Items.Clear();
            }

            foreach (var checkedItem in listCartItems.CheckedItems)
            {
                if (checkedItem is CartItem item)
                {
                    if (listReceiptItems != null)
                    {
                        listReceiptItems.Items.Add(item);
                    }

                    receiptText += $"{item.ToString()}\r\n";

                    if (item.Category != "SET_HEADER")
                    {
                        checkedTotal += item.Price;
                    }
                }
            }

            receiptText += "----------------------------------------\r\n";
            receiptText += $"Total Amount Paid: ₱{checkedTotal:N2}\r\n";

            if (lblReceiptCustomer != null) lblReceiptCustomer.Text = $"Customer: {customerName}";
            if (lblReceiptDate != null) lblReceiptDate.Text = $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            if (lblReceiptGrandTotal != null) lblReceiptGrandTotal.Text = $"Total Amount Paid: ₱{checkedTotal:N2}";

            try
            {
                string fileName = $"Receipt_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string filePath = Path.Combine(Application.StartupPath, fileName);
                File.WriteAllText(filePath, receiptText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save receipt file: " + ex.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ShowPanel(panelReceipt);
        }  

        private void btnGoToBuilder_Click(object sender, EventArgs e) => ShowPanel(panelBuildPC);
        private void btnBackToBuilder_Click(object sender, EventArgs e) => ShowPanel(panelBuildPC);

        private void ShowPanel(Panel selectedPanel)
        {
            if (panelBuildPC != null) panelBuildPC.Visible = false;
            if (panelCatalog != null) panelCatalog.Visible = false;
            if (panelCart != null) panelCart.Visible = false;
            if (panelReceipt != null) panelReceipt.Visible = false;

            if (selectedPanel != null)
            {
                selectedPanel.Visible = true;
                selectedPanel.BringToFront();
            }
        }

        private void button1_Click(object sender, EventArgs e) => ShowPanel(panelBuildPC);
        private void button3_Click(object sender, EventArgs e) => ShowPanel(panelCatalog);

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshCartUI();
            ShowPanel(panelCart);
        }

        private void button5_Click(object sender, EventArgs e) => ShowPanel(panelReceipt);
        private void button2_Click(object sender, EventArgs e) => Application.Exit();

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
                this.Hide();
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

        private void panelBuildPC_Paint(object sender, PaintEventArgs e) { }
        private void lblEstimatedTotal_Click(object sender, EventArgs e) { }
        private void panelCatalog_Paint(object sender, PaintEventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label94_Click(object sender, EventArgs e) { }
        private void label54_Click(object sender, EventArgs e) { }
    }
}