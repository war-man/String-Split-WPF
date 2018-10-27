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

namespace String_Split___Bai_tap_2
{
    // Class Int
    class MyInt
    {
        public int value { get; set; }
        public MyInt()
        {
            value = 0;
        }

        public static Result TryParse(string line)
        {
            var result = new Result();
            var f = new MyInt();
            try
            {
                f.value = int.Parse(line);
                result.Data = f;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Errors.Add(new Error() { Code = 1, Message = ex.Message });
            }

            return result;
        }
        public void Tong1(MyInt[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i].value;
            }
            value = sum;
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

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            String line = txt2.Text;
            string[] temp = line.Split(new string[] { "+" },
                StringSplitOptions.RemoveEmptyEntries);

            int len = temp.Length;
            MyInt[] array = new MyInt[len];
            int checkError = 0;
            for (int i = 0; i < len; i++)
            {
                var result = MyInt.TryParse(temp[i]);
                if (result.IsSuccessful)
                {
                    var f = result.Data as MyInt;
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
                MyInt sum = new MyInt();
                sum.Tong1(array);
                txt2.Text = sum.value + "";
            }
        }
    }
}