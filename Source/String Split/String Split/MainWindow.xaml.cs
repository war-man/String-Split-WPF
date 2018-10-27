using System;
using System.Collections.Generic;
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

namespace String_Split
{
        class Fraction 
        {
            public static string SEPERATOR = "/";
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public Fraction()
            {
                Numerator = 0;
                Denominator = 1;
            }

            public static Result TryParse(string line)
            {
                var result = new Result();
                var f = new Fraction();

                var tokens = line.Split(new string[] { SEPERATOR },
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(token => token.Trim()) 
                    .ToArray();

                try
                {
                    int num = int.Parse(tokens[0]);
                    int den = int.Parse(tokens[1]);
                    f.Numerator = num;
                    f.Denominator = den;
                    result.Data = f;

                    if (den == 0)
                    {
                        result.IsSuccessful = false;
                        result.Errors.Add(new Error() { Code = 2, Message = "Divided by Zero" });
                    }

                } catch (Exception ex)
                {
                    result.IsSuccessful = false;
                    result.Errors.Add(new Error() { Code = 1, Message = ex.Message });
                }                

                return result;
            }
            private int UocChungLonNhat(int a, int b)
            {
                if (b == 0) return a;
                return UocChungLonNhat(b, a % b);
            }
            public void Tong1(Fraction[] array)
            {
                Fraction sum = new Fraction();
                sum.Numerator = 0;
                sum.Denominator = 1;
                int i = array.Length;
                for (int j = 0; j < i; j++)
                {
                    sum.Numerator = sum.Numerator * array[j].Denominator + sum.Denominator * array[j].Numerator;
                    sum.Denominator = sum.Denominator * array[j].Denominator;
                }
                int ucln = UocChungLonNhat(sum.Numerator, sum.Denominator);
                sum.Numerator /= ucln;
                sum.Denominator /= ucln;
                Numerator = sum.Numerator;
                Denominator = sum.Denominator;
            }
        }
        class Error
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }

        class Result
        {
            public bool IsSuccessful { get; set; }
            public List<Error> Errors { get; set; }
            public object Data { get; set; }

            public Result()
            {
                IsSuccessful = true;
                Errors = new List<Error>();
            }
        }
           
    

    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            
            String line = txt1.Text;
            string[] temp = line.Split(new string[] { "+" },  
                StringSplitOptions.RemoveEmptyEntries);

            int len = temp.Length;
            Fraction[] array = new Fraction[len];
            int checkError = 0;
            
            for (int i = 0; i < len; i++)
            {
                var result = Fraction.TryParse(temp[i]);
                if (result.IsSuccessful)
                {
                    var f = result.Data as Fraction;           
                    array[i] = f;
                }
                else
                {
                    checkError = 1;
                    StringBuilder sError = new StringBuilder();
                    foreach (var error in result.Errors)
                    {
                        string s2 = error.Code + " " + error.Message + "\n";
                        sError.Append(s2);
                    }
                    MessageBox.Show("Co loi khi khoi tao : " + sError.ToString());
                    break;
                }
            }
            if (checkError == 0)
            {
                Fraction sum = new Fraction();
                sum.Tong1(array);
                txt1.Text = sum.Numerator + "/" + sum.Denominator;
            }
        }
    }
}
