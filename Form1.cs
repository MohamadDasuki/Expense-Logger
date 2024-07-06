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

            // Display the values in a message box
            string message = $"Expense Type: {type}\nAmount: %{amount}\nDate: {selectedDate.ToShortDateString()}\nReceipt No.: {receipt}";
            MessageBox.Show(message);


            string csvline = $"{type},{amount},{selectedDate.ToShortDateString()},{receipt}";

            string filepath = "Log.csv";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(csvline);
            }

            MessageBox.Show("Data has been appended to the CSV file.");



        }
    }
}
