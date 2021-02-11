using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class replacingBooks : Form
    {
        int i = 1;
        //list containing call numbers
        List<string> list;
        //list containing call numbers only numbers of list
        List<double> numericSort = new List<double>();
        //list containing call numbers only letters of list
        List<string> alphabetSort = new List<string>();
        //sorted numeric
        List<double> SnumericSort;
        //sorted alphabet
        List<string> SalphabetSort;

        public replacingBooks()
        {
            
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            listBox1.DoubleClick += new EventHandler(listBox1_DoubleClick);
        }

        private void replacingBooks_Load(object sender, EventArgs e)
        {

            richTextBox1.Text = "Put the items in the box above in the correct order!\n\nThen Click Submit to see if you got it Right!";

            listBox3.Visible = false;
            listBox1.AllowDrop = true;

            list = new List<string>();

            Random random = new Random();
            double x = 0;
          
            StringBuilder str_build = new StringBuilder();
            

            char letter;


            string k;
            //generate 10 random call numbers
            for (int i = 0; i < 10; i++)
            {
                
                double randNumber = random.NextDouble() * (1000 - 100) + 100;
                x = Convert.ToDouble(randNumber.ToString("f" + 2));
                //generate random string
                for (int p = 0; p < 3; p++)
                {
                    double flt = random.NextDouble();
                    int shift = Convert.ToInt32(Math.Floor(25 * flt));
                    letter = Convert.ToChar(shift + 65);
                    str_build.Append(letter);
                }
                k = str_build.ToString();

                
                list.Add(x.ToString()+k) ;
                str_build.Clear();

            }
            //add random call number to listbox
            for (int j = 0; j < 10; j++)
            {

                listBox1.Items.Add((j) + ". " + list[j]);
            }
            //replace list with sort by number
            string cut;
            
            for(int q = 0; q < 10; q++)
            {
                //take first 3 numbers from call number
                cut = list[q].Substring(0,list[q].Length-3);

                numericSort.Add(double.Parse(cut));

            }
            //replace list with sort by alphabet
            string cutt;

            for (int q = 0; q < 10; q++)
            {
                //take last 3 chars from call number
                cutt = list[q].Substring(list[q].Length - 3);

                alphabetSort.Add(cutt);

            }

        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            //add all call numbers to other listbox
            if (listBox2.Items.Count == 10)
            {
                MessageBox.Show("You have added all the items!");
            }
            else
            {
                if (listBox1.SelectedItem != null)
                {

                    if (listBox1.SelectedIndex == 9)
                    {
                        listBox2.Items.Add(listBox1.SelectedItem.ToString().Substring(3).Trim());
                       
                    }
                    else
                    {
                        listBox2.Items.Add(listBox1.SelectedItem.ToString().Substring(2).Trim());

                    }
                    listBox1.Items.Remove(listBox1.SelectedItem.ToString());

                }
            }
            i++;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox3.Visible = true;
            int mark = 0;
            //numeric sort algorithm (Selection Sort)
            if (radioButton1.Checked)
            {
                button1.Enabled = false;
                SnumericSort = new List<double>(numericSort.Count);
                for (int i = 0; i < numericSort.Count; i++)
                {
                    var item = numericSort[i];
                    var currentIndex = i;

                    while (currentIndex > 0 && SnumericSort[currentIndex - 1] > item)
                    {
                        currentIndex--;
                    }

                    SnumericSort.Insert(currentIndex, item);


                }
                
                //check if user got order correct vs the sort
                try
                {
                    results.completedTasks += 1;

                    for (int i = 0; i < 10; i++)
                    {
                        
                        if (double.Parse(listBox2.Items[i].ToString().Substring(0, list[i].Length - 3).Trim()) == double.Parse(SnumericSort[i].ToString().Trim()))
                        {
                            mark = mark + 1;
                            if (mark == 10)
                            {
                                //MessageBox.Show("Full Marks"+mark);
                                richTextBox1.AppendText("\n\nYou got 10 out of 10! Well done");
                                
                            }
                        }

                    }
                    if (mark != 10)
                    {
                        richTextBox1.AppendText("\n\nClose! You got "+mark+" out of 10!");
                        
                    }
                }
                catch
                {
                    
                    richTextBox1.AppendText("\n\nYou got " + mark + " out of 10!");
                }

                listBox3.Items.Add("Correct Answer:");

                for (int u = 0; u < 10; u++)
                {
                    listBox3.Items.Add(SnumericSort[u]);
                }


            }
            //alphabetical sort (Bubble sort)
            else if (radioButton2.Checked)
            {
                button1.Enabled = false;
                
                mark = 0;

                int l = 10;
                string temp;
                SalphabetSort = new List<string>(alphabetSort.Count);
                SalphabetSort = alphabetSort;
                for (i = 0; i < l; i++)
                {
                    for (int j = 0; j < l - 1; j++)
                    {
                        if (SalphabetSort[j].CompareTo(SalphabetSort[j + 1]) > 0)
                        {
                            temp = SalphabetSort[j];
                            SalphabetSort[j] = SalphabetSort[j + 1];
                            SalphabetSort[j + 1] = temp;
                        }
                    }
                }
                try
                {

                    for (int i = 0; i < 10; i++)
                    {
                    
                    if (listBox2.Items[i].ToString().Substring(list[i].Length - 3).Trim() == SalphabetSort[i].ToString().Trim())
                        {
                            mark = mark + 1;
                            if (mark == 10)
                            {
                                richTextBox1.AppendText("\n\nYou got 10 out of 10! Well done");
                            }
                        }

                    }
                    if (mark != 10)
                    {
                        richTextBox1.AppendText("\n\nClose! You got " + mark + " out of 10!");
                    }
                }
                catch
            {
                    richTextBox1.AppendText("\n\nYou got " + mark + " out of 10!");
                }

            listBox3.Items.Add("Correct Answer:");

                for (int u = 0; u < 10; u++)
                {
                    listBox3.Items.Add(SalphabetSort[u]);
                }
            }
            else
            {
                MessageBox.Show("Please Select a Radio button");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            replacingBooks r = new replacingBooks();
            this.Dispose();
            r.Show();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
