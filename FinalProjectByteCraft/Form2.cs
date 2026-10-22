using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FinalProjectByteCraft
{
    // Graphical User Interfaces
    public partial class Form2 : Form
    {
        // Designing with Classes
        public class CartItem
        {
            public string Category { get; set; }
            public string ItemName { get; set; }
            public decimal Price { get; set; }

            // String Methods
            public override string ToString()
            {
                // Decision Structures
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

        // Lists and Dictionaries
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

        // Lists and Dictionaries
        private Dictionary<string, decimal> gpuPrices = new Dictionary<string, decimal>()
        {
            { "NVIDIA GeForce RTX 5060 8GB", 28000.00m },
            { "NVIDIA GeForce RTX 5090 32GB", 180000.00m },
            { "NVIDIA GeForce RTX 4060 8GB", 25000.00m },
            { "NVIDIA GeForce RTX 4090 24GB", 120000.00m },
            { "NVIDIA GeForce RTX 3060 12GB", 23500.00m },
            { "NVIDIA GeForce RTX 3090 24GB", 65000.00m }
        };

        // Lists and Dictionaries
        private Dictionary<string, decimal> ramPrices = new Dictionary<string, decimal>()
        {
            { "16GB DDR5 5000MHz", 3500.00m },
            { "16GB DDR5 6000MHz", 4500.00m },
            { "16GB DDR5 7000MHz", 6000.00m },
            { "16GB DDR5 8000MHz", 8000.00m }
        };

        // Lists and Dictionaries
        private Dictionary<string, decimal> storagePrices = new Dictionary<string, decimal>()
        {
            { "500GB HDD", 1500.00m },
            { "1TB HDD", 2800.00m }
        };

        // Lists and Dictionaries
        private Dictionary<string, decimal> psuPrices = new Dictionary<string, decimal>()
        {
            { "500W 80+ Silver PSU", 2500.00m },
            { "750W 80+ Gold PSU", 4000.00m },
            { "1000W 80+ Platinum PSU", 7000.00m }
        };

        // Lists and Dictionaries
        private Dictionary<string, decimal> coolerPrices = new Dictionary<string, decimal>()
        {
            { "Air Cooler", 1000.00m },
            { "Liquid Cooler", 3500.00m }
        };

        // Lists and Dictionaries
        private List<CartItem> shoppingCart = new List<CartItem>();

        // Sequential Structures
        public Form2()
        {
            InitializeComponent();
        }

        // Sequential Structures
        public Form2(string name)
        {
            InitializeComponent();
            customerName = name;
        }

        // Sequential Structures
        private void Form2_Load(object sender, EventArgs e)
        {
            InitializePCBuilderOptions();
            WireUpEvents();
            ShowPanel(panelBuildPC);
        }

        private void WireUpEvents()
        {
            // Decision Structures
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
            // Repetition Structures
            foreach (var kvp in cpuPrices) cmbCPU.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbCPU.SelectedIndex = 0;

            cmbGPU.Items.Clear();
            cmbGPU.Items.Add("Select GPU...");
            // Repetition Structures
            foreach (var kvp in gpuPrices) cmbGPU.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbGPU.SelectedIndex = 0;

            cmbRAM.Items.Clear();
            cmbRAM.Items.Add("Select RAM...");
            // Repetition Structures
            foreach (var kvp in ramPrices) cmbRAM.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbRAM.SelectedIndex = 0;

            cmbStorage.Items.Clear();
            cmbStorage.Items.Add("Select Storage...");
            // Repetition Structures
            foreach (var kvp in storagePrices) cmbStorage.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbStorage.SelectedIndex = 0;

            cmbPSU.Items.Clear();
            cmbPSU.Items.Add("Select Power Supply...");
            // Repetition Structures
            foreach (var kvp in psuPrices) cmbPSU.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbPSU.SelectedIndex = 0;

            cmbCooler.Items.Clear();
            cmbCooler.Items.Add("Select Cooler...");
            // Repetition Structures
            foreach (var kvp in coolerPrices) cmbCooler.Items.Add($"{kvp.Key} - ₱{kvp.Value:N2}");
            cmbCooler.SelectedIndex = 0;

            UpdateTotalPrice();
        }

        // Functions
        private Image LoadResourceImage(string fileName)
        {
            string path = Path.Combine(Application.StartupPath, "Resources", fileName);
            // Decision Structures
            if (File.Exists(path)) return Image.FromFile(path);

            string altPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
            // Decision Structures
            if (File.Exists(altPath)) return Image.FromFile(altPath);

            return null;
        }

        private void btnClearSelections_Click(object sender, EventArgs e)
        {
            // Decision Structures
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
            // Decision Structures
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
            // Decision Structures
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
            // Decision Structures
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
            // Decision Structures
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
            // Decision Structures
            switch (cmbCooler.SelectedIndex)
            {
                case 1: picCooler.Image = LoadResourceImage("air.png"); break;
                case 2: picCooler.Image = LoadResourceImage("liquid.jpg"); break;
                default: picCooler.Image = null; break;
            }
            UpdateTotalPrice();
        }

        // Functions
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

        // Functions
        private decimal GetPriceFromSelection(ComboBox cmb)
        {
            // Decision Structures
            if (cmb == null || cmb.SelectedIndex <= 0) return 0m;

            // String Methods
            string selectedText = cmb.SelectedItem.ToString();
            int dashIndex = selectedText.IndexOf(" - ₱");
            string itemName = (dashIndex != -1) ? selectedText.Substring(0, dashIndex).Trim() : selectedText.Trim();

            // Decision Structures
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

            // Decision Structures
            if (string.IsNullOrWhiteSpace(input)) return;

            // Decision Structures
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

            // Repetition Structures
            foreach (var cpu in cpuPrices)
            {
                // Repetition Structures
                foreach (var gpu in gpuPrices)
                {
                    // Repetition Structures
                    foreach (var ram in ramPrices)
                    {
                        // Repetition Structures
                        foreach (var storage in storagePrices)
                        {
                            // Repetition Structures
                            foreach (var psu in psuPrices)
                            {
                                // Repetition Structures
                                foreach (var cooler in coolerPrices)
                                {
                                    decimal currentTotal = cpu.Value + gpu.Value + ram.Value + storage.Value + psu.Value + cooler.Value;

                                    // Decision Structures
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

            // Decision Structures
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

        // Functions
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

            // Decision Structures
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        // Functions
        private void SelectMatchingComboItem(ComboBox cmb, string itemName)
        {
            // Repetition Structures
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                // String Methods & Decision Structures
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
            // Repetition Structures
            foreach (var box in partBoxes)
            {
                // Decision Structures
                if (box != null && box.SelectedIndex > 0) itemsAdded++;
            }

            // Decision Structures
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

            // Repetition Structures
            for (int i = 0; i < partBoxes.Length; i++)
            {
                // Decision Structures
                if (partBoxes[i] != null && partBoxes[i].SelectedIndex > 0)
                {
                    decimal price = GetPriceFromSelection(partBoxes[i]);
                    string fullText = partBoxes[i].SelectedItem.ToString();
                    // String Methods
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

        // Functions
        private void RefreshCartUI()
        {
            bool isEmpty = (shoppingCart.Count == 0);

            // Decision Structures
            if (lblEmptyCart != null) lblEmptyCart.Visible = isEmpty;
            if (btnGoToBuilder != null) btnGoToBuilder.Visible = isEmpty;
            if (listCartItems != null) listCartItems.Visible = !isEmpty;
            if (btnClearCart != null) btnClearCart.Visible = !isEmpty;
            if (btnCheckout != null) btnCheckout.Visible = !isEmpty;
            if (lblCartTotalPrice != null) lblCartTotalPrice.Visible = !isEmpty;

            // Decision Structures
            if (!isEmpty && listCartItems != null)
            {
                listCartItems.Items.Clear();
                decimal cartTotal = 0;

                // Repetition Structures
                foreach (CartItem item in shoppingCart)
                {
                    listCartItems.Items.Add(item);
                    cartTotal += item.Price;
                }

                // Decision Structures
                if (lblCartTotalPrice != null)
                {
                    lblCartTotalPrice.Text = $"Total: ₱{cartTotal:N2}";
                }
            }
            // Decision Structures
            else if (lblCartTotalPrice != null) 
            {
                lblCartTotalPrice.Text = "Total: ₱0.00";
            }
        }

        private void listCartItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Decision Structures
            if (isUpdatingCheck) return;

            // Decision Structures
            if (listCartItems.Items[e.Index] is CartItem clickedItem)
            {
                isUpdatingCheck = true;

                // Decision Structures
                if (clickedItem.Category == "SET_HEADER")
                {
                    bool targetCheckState = (e.NewValue == CheckState.Checked);
                    int currentIndex = e.Index + 1;

                    // Repetition Structures
                    while (currentIndex < listCartItems.Items.Count)
                    {
                        // Decision Structures
                        if (listCartItems.Items[currentIndex] is CartItem nextItem)
                        {
                            // Decision Structures
                            if (nextItem.Category == "SET_HEADER") break;
                            listCartItems.SetItemChecked(currentIndex, targetCheckState);
                        }
                        currentIndex++;
                    }
                }
                else
                {
                    int headerIndex = -1;
                    // Repetition Structures
                    for (int i = e.Index; i >= 0; i--)
                    {
                        // Decision Structures
                        if (listCartItems.Items[i] is CartItem item && item.Category == "SET_HEADER")
                        {
                            headerIndex = i;
                            break;
                        }
                    }

                    // Decision Structures
                    if (headerIndex != -1)
                    {
                        bool anyItemChecked = false;
                        int checkIndex = headerIndex + 1;

                        // Repetition Structures
                        while (checkIndex < listCartItems.Items.Count)
                        {
                            // Decision Structures
                            if (listCartItems.Items[checkIndex] is CartItem nextItem && nextItem.Category == "SET_HEADER")
                            {
                                break;
                            }

                            // Decision Structures
                            bool isChecked = (checkIndex == e.Index) ? (e.NewValue == CheckState.Checked) : listCartItems.GetItemChecked(checkIndex);

                            // Decision Structures
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
            // Decision Structures
            if (listCartItems.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please check the boxes of the PC sets or items you want to check out.", "No Items Checked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal checkedTotal = 0;
            string receiptText = "=== BYTECRAFT SYSTEMS RECEIPT ===\r\n";
            receiptText += $"Customer: {customerName}\r\n";
            receiptText += $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\r\n";
            receiptText += "----------------------------------------\r\n";

            // Decision Structures
            if (listReceiptItems != null)
            {
                listReceiptItems.Items.Clear();
            }

            // Repetition Structures
            foreach (var checkedItem in listCartItems.CheckedItems)
            {
                // Decision Structures
                if (checkedItem is CartItem item)
                {
                    // Decision Structures
                    if (listReceiptItems != null)
                    {
                        listReceiptItems.Items.Add(item);
                    }

                    receiptText += $"{item.ToString()}\r\n";

                    // Decision Structures
                    if (item.Category != "SET_HEADER")
                    {
                        checkedTotal += item.Price;
                    }
                }
            }

            receiptText += "----------------------------------------\r\n";
            receiptText += $"Total Amount Paid: ₱{checkedTotal:N2}\r\n";

            // Decision Structures
            if (lblReceiptCustomer != null) lblReceiptCustomer.Text = $"Customer: {customerName}";
            if (lblReceiptDate != null) lblReceiptDate.Text = $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            if (lblReceiptGrandTotal != null) lblReceiptGrandTotal.Text = $"Total Amount Paid: ₱{checkedTotal:N2}";

            // Text File Manipulation
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

        // Graphical User Interfaces & Functions
        private void ShowPanel(Panel selectedPanel)
        {
            // Decision Structures
            if (panelBuildPC != null) panelBuildPC.Visible = false;
            if (panelCatalog != null) panelCatalog.Visible = false;
            if (panelCart != null) panelCart.Visible = false;
            if (panelReceipt != null) panelReceipt.Visible = false;

            // Decision Structures
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
            // Sequential Structures
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

            // Decision Structures
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