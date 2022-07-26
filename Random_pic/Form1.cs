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

namespace Random_pic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string path = null;
            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)
                    path = dialog.SelectedPath;
            Console.WriteLine();
            List<string> filesname = Directory.GetFiles(@path).ToList<string>();
            //Создание объекта для генерации чисел
            Random rnd = new Random();

            //Получить очередное (в данном случае - первое) случайное число
            int value = rnd.Next(0, filesname.Count);

            //Вывод полученного числа в консоль
            string patis = @Environment.UserName;
            string pathss = "c:/Users/" + patis + "/Desktop/random_pic.jpg";
            File.Copy(@filesname[value], pathss, true);
            MessageBox.Show("ВСе ^^");
        }
    }
}
