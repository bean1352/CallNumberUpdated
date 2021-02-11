using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class findingCallnumbers : Form
    {
        static string correctAnswer;
        string secondLevel;
        string question;
        List<string> firstlevel = new List<string> { };
        List<string> secondlevel = new List<string> { };
        List<string> thirdlevel = new List<string> { };
        TreeNode<string> root;
        static TreeNode<string> secondNode;
        static TreeNode<string> firstNode;
        int mark = 0;
        
        public findingCallnumbers()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void findingCallnumbers_Load(object sender, EventArgs e)
        {
            button4.Hide();
            label3.Text = "Question 1";
            richTextBox1.Text = "Match the First Level Entry to its corrosponding First and Second Level Entry";
            //check if tree is empty
            if (root == null)
            {
                populateTree();
            }

            var list = randomLeafNode(root);

            label1.Text = list[2].Substring(4);
           // label1.Text = list[2];

            List<String> newList = new List<String> {};

            correctAnswer = list[0];
            secondLevel = list[1];
            var newlist = randomFirstLevel(root, list[0]);
            //List sorted Numerically in Ascending order
            var sorted = newlist.OrderBy(d => d).ToArray();
            question = list[2];

            label6.Text = null;

            for (int i = 0; i < 4; i++)
            {
                //listBox1.Items.Add(sorted[i]);
                comboBox1.Items.Add(sorted[i]);

                label6.Text += sorted[i] + "\n";
            }

        }

        public void populateTree()
        {
            //this method populates first, second and third level of the tree using a text document
            string path = @"finalcallnumber.txt";
            string line;
            string str;
            int num;

            //read text document
            using (StreamReader tr = new StreamReader(path))
            {

                while (true)
                {
                    line = tr.ReadLine();
                    //break loop when there are no more lines to read
                    if (line == null)
                    {
                        break;
                    }
                    //get first 3 numbers from each line
                    str = line.Substring(0, 3);
                    num = Convert.ToInt32(str);
                    //if numbers are divisible by 100 (first level)
                    if (num % 100 == 0 || num == 0)
                    {
                        firstlevel.Add(line);
                    }
                    //if numbers are divisible by 10 (second level)
                    else if (num % 10 == 0)
                    {

                        secondlevel.Add(line);



                    }
                    //the rest of the lines are all third level entries
                    else
                    {
                        thirdlevel.Add(line);
                    }

                }
            }
            int u;
            //remove all duplicates of second level by checking the numbers above and below if they are the same
            for (int i = 0; i < secondlevel.Count; i++)
            {

                u = Convert.ToInt32(secondlevel[i].ToString().Substring(0, 3));
                if (Convert.ToInt32(secondlevel[i + 1].ToString().Substring(0, 3)) == u || Convert.ToInt32(secondlevel[i - 1].ToString().Substring(0, 3)) == u)
                {
                    secondlevel.RemoveAt(i);
                }

            }

            //tree lists
            List<TreeNode<string>> firstlevel1 = new List<TreeNode<string>> { };
            List<TreeNode<string>> secondlevel1 = new List<TreeNode<string>> { };
            List<TreeNode<string>> thirdlevel1 = new List<TreeNode<string>> { };


            //insert all data into tree, checks which children should be added to each parent
            root = new TreeNode<string>("root");
            {
                for (int i = 0; i < firstlevel.Count; i++)
                {
                    firstlevel1.Add(root.AddChild(firstlevel[i]));

                    for (int t = 0; t < secondlevel.Count; t++)
                    {
                        if (Convert.ToInt32(secondlevel[t].ToString().Substring(0, 3)) > Convert.ToInt32(firstlevel[i].ToString().Substring(0, 3)) && Convert.ToInt32(secondlevel[t].ToString().Substring(0, 3)) < (Convert.ToInt32(firstlevel[i].ToString().Substring(0, 3)) + 100))
                        {
                            secondlevel1.Add(firstlevel1[i].AddChild(secondlevel[t]));

                            for (int j = 0; j < thirdlevel.Count; j++)
                            {
                                if (Convert.ToInt32(thirdlevel[j].ToString().Substring(0, 3)) >= Convert.ToInt32(secondlevel[t].ToString().Substring(0, 3)) && Convert.ToInt32(thirdlevel[j].ToString().Substring(0, 3)) < (Convert.ToInt32(secondlevel[t].ToString().Substring(0, 3)) + 10))
                                {

                                    thirdlevel1.Add(secondlevel1[t].AddChild(thirdlevel[j]));
                                }
                            }
                        }

                    }

                }
            }
        }

        public static List<String> randomLeafNode(TreeNode<string> root)
        {
            //Method to get random third level node and its ancestors
            Random random = new Random();
            List<String> allLevels = new List<String> {"","","" };
            int ran = random.Next(root.getChildren(root).Count);

            firstNode = root.getChildren(root).ElementAt(ran);
            allLevels[0] = root.getChildren(root).ElementAt(ran).Data;
            int ranFirst = random.Next(firstNode.getChildren(firstNode).Count);


            secondNode = firstNode.getChildren(firstNode).ElementAt(ranFirst);
            allLevels[1] = firstNode.getChildren(firstNode).ElementAt(ranFirst).Data;
            int ranSecond = random.Next(secondNode.getChildren(secondNode).Count);

            allLevels[2] = secondNode.getChildren(secondNode).ElementAt(ranSecond).Data;
            return allLevels;
        }

        public static List<String> randomFirstLevel(TreeNode<string> root, string x)
        {
            //method to get first level entries and the correct answer
            List<String> rand = new List<String> { };
            rand.Add(x);
            Random random = new Random();

            while (true)
            {
                if(rand.Count == 4)
                {
                    break;
                }

                int ran = random.Next(root.getChildren(root).Count);
                string firstNode = root.getChildren(root).ElementAt(ran).Data;

                if (!rand.Contains(firstNode))
                {
                    rand.Add(firstNode);
                }

            }

            return rand;

        }
        public static List<String> randomSecondLevel(TreeNode<string> root, TreeNode<string> firstNode, TreeNode<string> secondNode)
        {
            //method to get second level entries and the correct answer
            List<String> rand = new List<String> { };
            rand.Add(secondNode.ToString());
            Random random = new Random();

            while (true)
            {
                if (rand.Count == 4)
                {
                    break;
                }

                int ranFirst = random.Next(secondNode.getChildren(secondNode).Count);

                secondNode = firstNode.getChildren(firstNode).ElementAt(ranFirst);


                if (!rand.Contains(secondNode.ToString()))
                {
                    rand.Add(secondNode.ToString());
                }

            }

            return rand;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           string answer = comboBox1.SelectedItem.ToString();
           
           if(answer == correctAnswer)
            {
                mark += 1;

                label5.Text = "Second Level Entries";
                label6.Text = null;
                var secondlist = randomSecondLevel(root, firstNode, secondNode);
                var sorted = secondlist.OrderBy(d => d).ToArray();
                comboBox1.Items.Clear();
                for (int i = 0; i < 4; i++)
                {

                    //listBox1.Items.Add(secondlist[i]);
                    label6.Text += sorted[i] + "\n";
                    comboBox1.Items.Add(sorted[i]);
                    comboBox1.SelectedIndex=0;
                }
                button1.Hide();
                button4.Show();
                int p = Convert.ToInt32(correctAnswer.Substring(0, 1));

                label3.Text = "Question 2";
                richTextBox1.Text = "You got the first level correct!\n\nCan you get the second level correct too?\n\n"+"The next answer is in between "+p+"00 and "+(p+1)+"00!";
            }
            else
            {
  
                    MessageBox.Show("That was the Incorrect Answer!\n\nTry Again!");
                    findingCallnumbers r = new findingCallnumbers();
                    this.Dispose();
                    r.Show();
                
            }
            comboBox1.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //reload page
            findingCallnumbers r = new findingCallnumbers();
            this.Dispose();
            r.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string answer;
            try
            {
                answer = comboBox1.SelectedItem.ToString();
            }
            catch
            {
                answer = "";
            }

            if(answer == secondLevel)
            {
                mark += 1;
                richTextBox1.Text = "You correctly chose the first and second level of the third level question!\nWell done!\nThe initial Question had the call number: "+question.Substring(0,3);
                

                results.completedTasks += 1;

                pictureBox1.Location = new Point(682,100);                

            }
            else
            {
                MessageBox.Show("That was the Incorrect Answer!\n\nTry Again!");
                findingCallnumbers r = new findingCallnumbers();
                this.Dispose();
                r.Show();
            }
        }
    }
}
