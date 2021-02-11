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
    public partial class identifyingAreas : Form
    {
        //define dictionary to store call numbers and their definitions
        IDictionary<Double, string> dic = new Dictionary<Double, string>();
        //declare bools to check which data set is in column A
        bool callNum = false;
        bool def = false;
        //declare lists
        List<Double> calllNumberList;
        List<Double> calllNumberListTest;
        List<String> definitionList;
        List<String> listt;
        Random random = new Random();        
        StringBuilder str_build = new StringBuilder();
        
        public identifyingAreas()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void identifyingAreas_Load(object sender, EventArgs e)
        {
            callNum = false;
            def = false;
            calllNumberList = new List<Double>();
            calllNumberListTest = new List<Double>(new double[10]);
            //populate list of definitions
            definitionList = new List<string>() { "Generalities", "Philosophy & Psychology", "Religion", "Social Sciences", "Language", "Natural sciences & Mathematics", "Technology (Applied sciences)", "The Arts", "Literature & Rhetoric", "Geography & history" }; 
            Random random = new Random();
            double x = 0;
            StringBuilder str_build = new StringBuilder();
            
            while (calllNumberList.Count != 9)
            {
               
                //add 10 random call numbers into list (Call numbers have to be in different hundredths)
                double randNumber = random.NextDouble() * (1000 - 100) + 100;
                x = Convert.ToDouble(randNumber.ToString("f" + 2));
                if (!calllNumberListTest.Contains(Double.Parse(x.ToString().Substring(0, 1))))
                {
                    calllNumberList.Add(x);
                    calllNumberListTest.Add(Double.Parse(x.ToString().Substring(0, 1)));
                }
                
            }
            var u = calllNumberList;
            if (calllNumberList.Count == 9)
            {
                calllNumberList.Add(random.Next(0, 100));
            }
            var p = calllNumberList;
            //call function to populate dictionary with random number and its corrosponding definiton
            load();
            //generate random number to decide which data set is in column A
            int num = random.Next(50);
            //Column A is call number
            if (num%2 == 0)
            {
                callNum = true;
                //populate listbox (Column A)
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add((i + 1) + ". " + calllNumberList[i]);
                }

                //randomize list
                var listt = definitionList.OrderBy(a => Guid.NewGuid()).ToList();
                char cha = 'A';
                //populate listbox (Column B)
                for (int i = 0; i < 10; i++)
                {
                    listBox2.Items.Add(cha + ". " + listt[i]);
                    cha++;
                }
                //populate answers
                for (int i = 0; i < 10; i++)
                {
                    comboBox1.Items.Add(listt[i]);
                    comboBox2.Items.Add(listt[i]);
                    comboBox3.Items.Add(listt[i]);
                    comboBox4.Items.Add(listt[i]);
                }

            }
            //Column A is Definition
            else
            {
                def = true;
              
                //randomize list
                listt = definitionList.OrderBy(a => Guid.NewGuid()).ToList();
                //populate listbox (Column A)
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add((i + 1) + ". " + listt[i]);
                }

                //randomize list
                
                char cha = 'A';
                //populate listbox (Column B)
                for (int i = 0; i < 10; i++)
                {
                    listBox2.Items.Add(cha + ". " + calllNumberList[i]);
                    cha++;
                }
                //populate answers
                for (int i = 0; i < 10; i++)
                {
                    comboBox1.Items.Add(calllNumberList[i]);
                    comboBox2.Items.Add(calllNumberList[i]);
                    comboBox3.Items.Add(calllNumberList[i]);
                    comboBox4.Items.Add(calllNumberList[i]);
                }

            }

           

        }
        public void load()
        {
            //Assign the correct definition for each call number in dictionary
            for (int i = 0; i < 10; i++)
            {

                if (calllNumberList[i] >= 0 && calllNumberList[i] < 100)
                {
                    
                    dic.Add(calllNumberList[i], "Generalities");
                    
                }
                else if (calllNumberList[i] >= 100 && calllNumberList[i] < 200)
                {
                    dic.Add(calllNumberList[i], "Philosophy & Psychology");
                }
                else if (calllNumberList[i] >= 200 && calllNumberList[i] < 300)
                {
                    dic.Add(calllNumberList[i], "Religion");
                }
                else if (calllNumberList[i] >= 300 && calllNumberList[i] < 400)
                {
                    dic.Add(calllNumberList[i], "Social Sciences");
                }
                else if (calllNumberList[i] >= 400 && calllNumberList[i] < 500)
                {
                    dic.Add(calllNumberList[i], "Language");
                }
                else if (calllNumberList[i] >= 500 && calllNumberList[i] < 600)
                {
                    dic.Add(calllNumberList[i], "Natural sciences & Mathematics");
                }
                else if (calllNumberList[i] >= 600 && calllNumberList[i] < 700)
                {
                    dic.Add(calllNumberList[i], "Technology (Applied sciences)");
                }
                else if (calllNumberList[i] >= 700 && calllNumberList[i] < 800)
                {
                    dic.Add(calllNumberList[i], "The Arts");
                }
                else if (calllNumberList[i] >= 800 && calllNumberList[i] < 900)
                {
                    dic.Add(calllNumberList[i], "Literature & Rhetoric");
                }
                else if (calllNumberList[i] >= 900 && calllNumberList[i] <= 999)
                {
                    dic.Add(calllNumberList[i], "Geography & history");
                }

                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //capture answers
            string a1, a2, a3, a4;

            a1 = comboBox1.Text;
            a2 = comboBox2.Text;
            a3 = comboBox3.Text;
            a4 = comboBox4.Text;
            int mark = 0;
            
            string found = null; ;
            //list of answers
            var answers = new List<string> { a1, a2, a3, a4 };
            //check if all answers have been answered
            if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1 && comboBox4.SelectedIndex != -1)
            {
                results.completedTasks += 1;
                //check which data is in column A
                if (callNum == true)
                {
                    for (int i = 0; i < 4; i++)
                    {  
                        //return true if answer exists with value
                        if(dic.TryGetValue(calllNumberList[i], out found) && found == answers[i])
                        {
                            //increment mark
                            mark = mark + 1;
                            

                        }  
                    }


                }
                //check which data is in column A
                else if (def == true)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        //return true if answer exists with value
                        if (dic.TryGetValue(double.Parse(answers[i]), out found) && found == listt[i])
                        {
                            //increment mark
                            mark = mark + 1;
                            

                        }
                    }  
                }
                else
                {
                    MessageBox.Show("Error has occured");
                }

                // MessageBox.Show(mark.ToString());

                richTextBox1.Text = "You got "+mark.ToString()+" out of 4!\nIf you would like to play again, Click RETRY!";

            
                
            }
            else
            {
                MessageBox.Show("Please answer all the questions");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //show hint for user
            MessageBox.Show("000-099 - Generalities\n100-199 - Philosophy and Pyschology\n200-299 - Religion\n300-399 - Social Sciences\n400-499 - Language\n500-599 - Natural sciences & Mathematics\n600-699 - Technology(Applied sciences)\n700-799 - The Arts\n800-899 - Literature\n900-999 - Geography & history");
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //go back to main form
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //reload page
            identifyingAreas r = new identifyingAreas();
            this.Dispose();
            r.Show();

        }
    }
}
