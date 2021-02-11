
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace callNumberWord
{

    class Program
    {

        static void Main(string[] args)
        {

            List<string> firstlevel = new List<string> { };
            List<string> secondlevel = new List<string> { };
            List<string> thirdlevel = new List<string> { };
            string path = "C:\\Users\\Dean\\Desktop\\Prog7312_Task2_18002023\\WindowsFormsApp2\\finalcallnumber.txt";
            string line;
            string str;
            int num;


            using (StreamReader tr = new StreamReader(path))
            {

                while (true)
                {
                    line = tr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    str = line.Substring(0, 3);
                    num = Convert.ToInt32(str);

                    if (num % 100 == 0 || num == 0)
                    {
                        firstlevel.Add(line);
                    }
                    else if (num % 10 == 0)
                    {

                        secondlevel.Add(line);



                    }
                    else
                    {
                        thirdlevel.Add(line);
                    }

                }
            }
            int u;
            for (int i = 0; i < secondlevel.Count; i++)
            {

                u = Convert.ToInt32(secondlevel[i].ToString().Substring(0, 3));
                if (Convert.ToInt32(secondlevel[i + 1].ToString().Substring(0, 3)) == u || Convert.ToInt32(secondlevel[i - 1].ToString().Substring(0, 3)) == u)
                {
                    secondlevel.RemoveAt(i);
                }

            }

            MultilevelLists(firstlevel,secondlevel,thirdlevel);


        }

        public static void MultilevelLists(List<string> first, List<string> second, List<string> third)
        {
            string documentPath = "C:\\Users\\Dean\\Desktop\\MultilevelLists1.docx";

         
            DocumentCore dc = new DocumentCore();

           
            Section s = new Section(dc);
            dc.Sections.Add(s);

            string[] myCollection = new string[] { "One", "Two", "Three", "Four", "Five" };

           
            ListStyle ls = new ListStyle("MyListDot", ListTemplateType.NumberWithDot);
            dc.Styles.Add(ls);

            ListStyle ls1 = new ListStyle("MyListBullet", ListTemplateType.Bullet);
            dc.Styles.Add(ls1);


            for (int i = 0; i < first.Count; i++)
            {
                 Paragraph p = new Paragraph(dc);
                    dc.Sections[0].Blocks.Add(p);

                    p.Content.End.Insert(first[i], new CharacterFormat() { Size = 14.0, FontColor = Color.Black });
                    p.ListFormat.Style = ls1;

                for (int t = 0; t < second.Count; t++)
                {
                    if (Convert.ToInt32(second[t].Substring(0, 3)) > Convert.ToInt32(first[i].ToString().Substring(0, 3)) && Convert.ToInt32(second[t].ToString().Substring(0, 3)) < (Convert.ToInt32(first[i].ToString().Substring(0, 3)) + 100))
                    {
                        Paragraph o = new Paragraph(dc);
                        dc.Sections[0].Blocks.Add(o);

                        o.Content.End.Insert(second[t], new CharacterFormat() { Size = 14.0, FontColor = Color.Black });
                        o.ListFormat.Style = ls1;
                        o.ListFormat.ListLevelNumber = 1;

                        for (int j = 0; j < third.Count; j++)
                        {
                            if (Convert.ToInt32(third[j].ToString().Substring(0, 3)) >= Convert.ToInt32(second[t].ToString().Substring(0, 3)) && Convert.ToInt32(third[j].ToString().Substring(0, 3)) < (Convert.ToInt32(second[t].ToString().Substring(0, 3)) + 10))
                            {

                                Paragraph l = new Paragraph(dc);
                                dc.Sections[0].Blocks.Add(l);

                                l.Content.End.Insert(third[j], new CharacterFormat() { Size = 14.0, FontColor = Color.Black });
                                l.ListFormat.Style = ls1;
                                l.ListFormat.ListLevelNumber = 2;
                            }
                        }
                    }

                }

            }

            dc.Save(documentPath, new DocxSaveOptions());

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(documentPath) { UseShellExecute = true });
        }
    }
    
}    

