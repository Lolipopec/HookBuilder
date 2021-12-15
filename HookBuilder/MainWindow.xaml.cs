using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HookBuilder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            List<Point> p = Reader();
            Builder(p);
        }
        static List<Point> p = new List<Point>();
        public List<Point> Reader()
        {
            using (StreamReader sr = new StreamReader(@"X:\KeyLog.txt"))
            {
                while (sr.EndOfStream != true)
                {
                    string[] str = sr.ReadLine().Split(' ');
                    p.Add(new Point() { x = Convert.ToInt32(str[0]), y = Convert.ToInt32(str[1]) });
                }
            }
            return p;
        }
        public struct Point
        {
            public int x;
            public int y;
        }
        void Builder(List<Point> p)
        {
            int column = 10;
            int row = 10;

            double[] widthh = new double[column];
            double[] heightt = new double[row];

            double width = SystemParameters.PrimaryScreenWidth / column;
            double height = SystemParameters.PrimaryScreenHeight / row;
            for (int i = 0; i < column; i++)
            {
                widthh[i] = width * (i + 1);
            }
            for (int i = 0; i < row; i++)
            {
                heightt[i] = height * (i + 1);
            }
            Map.Children.Clear();
            RowDefinition[] rows = new RowDefinition[row];
            ColumnDefinition[] columns = new ColumnDefinition[column];
            for (int i = 0; i < column; i++)
            {
                columns[i] = new ColumnDefinition();
                Map.ColumnDefinitions.Add(columns[i]);
                columns[i].Width = new GridLength(width);

            }
            for (int i = 0; i < row; i++)
            {
                rows[i] = new RowDefinition();
                Map.RowDefinitions.Add(rows[i]);
                rows[i].Height = new GridLength(height);
            }
            double MaxValues = 0;
            List<Point> l = p.ToList();
            for (int i = 0; i < row; i++)
            {
                
                for (int j = 0; j < column; j++)
                {
                    int count = 0;
                    for (int t = 0; t < l.Count; t++)
                    {
                        if (l[t].x < widthh[j] && l[t].y < heightt[i])
                        {
                            count++;
                            if (count > MaxValues)
                            {
                                MaxValues = count;
                            }
                            l.RemoveAt(t);
                            t--;
                        }

                    }
                }
            }
            for (int i = 0; i < row; i++)
            {
               
                for (int j = 0; j < column; j++)
                {
                    int count = 0;
                    for (int t = 0; t < p.Count; t++)
                    {
                        if (p[t].x < widthh[j] && p[t].y < heightt[i])
                        {
                            count++;
                            p.RemoveAt(t);
                            t--;
                        }

                    }
                    Rectangle myRect = new System.Windows.Shapes.Rectangle();
                    Map.Children.Add(myRect);      
                    myRect.Fill = System.Windows.Media.Brushes.White;
                    if (count > (MaxValues / 6))
                    {
                        myRect.Fill = System.Windows.Media.Brushes.Green;
                    }
                    if (count > (MaxValues / 5))
                    {
                        myRect.Fill = System.Windows.Media.Brushes.DarkGreen;
                    }
                    if (count > (MaxValues / 4))
                    {
                        myRect.Fill = System.Windows.Media.Brushes.Orange;
                    }
                    if (count > (MaxValues / 3))
                    {   
                        myRect.Fill = System.Windows.Media.Brushes.DarkOrange;          
                    }
                    if (count > (MaxValues / 2))
                    {               
                        myRect.Fill = System.Windows.Media.Brushes.Red;           
                    }
                    if (count == MaxValues)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.Black;
                    }
                    Grid.SetColumn(myRect, j);
                    Grid.SetRow(myRect, i);
                }
            }
        }
    }
}
