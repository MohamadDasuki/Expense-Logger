using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Expense_Logger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            // Get the text from the first textbox
            string type = comboBox1.Text;
            

            // Get the number from the second textbox (convert to int)
            if (double.TryParse(textBox1.Text, out double amount))
            {
                
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            // Get the date from the DateTimePicker
            DateTime selectedDate = dateTimePicker1.Value;

            // Get the text from the third textbox
            string receipt = textBox2.Text;

            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(receipt) || amount == 0)
            {
                MessageBox.Show("Please make sure all fields are filled correctly.");
                return;
            }

            string csvline = $"{type},{amount},{selectedDate.ToShortDateString()},{receipt}";

            string filepath = "Log.csv";

            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(csvline);
            }

            MessageBox.Show("Data has been appended to the CSV file.");



        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Set up the OpenFileDialog
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.Title = "Open CSV File";

            // Show the dialog and get the result
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the file path
                string filePath = openFileDialog1.FileName;

                try
                {
                    // Read all lines from the CSV file
                    string[] lines = File.ReadAllLines(filePath);

                    // Check if the file has lines
                    if (lines.Length > 0)
                    {
                        // Parse the values in the specified column (assuming column index 1, change if needed)
                        int columnIndex = 1; // Change to the index of the column you want to sum

                        double sum = lines
                            //.Skip(1) // Skip the header row
                            .Select(line => line.Split(',')[columnIndex])
                            .Select(value => double.TryParse(value, out double result) ? result : 0)
                            .Sum();

                        // Display the result in the label
                        totalLabel.Text = $"$ {sum}";
                    }
                    else
                    {
                        MessageBox.Show("The file is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while reading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
