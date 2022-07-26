using Neon.Downloader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Model.RequestParams.Market;

namespace Down_loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            await down_wall_picsAsync();
            down_load_albums();
        }


        struct Urles
        {
            public Uri url;
            public string path;


        }
        private void Down_load_vk_pics()
        {


            var api = new VkApi();
            try
            {
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = api_token
                });
            }
            catch
            {
                MessageBox.Show("Сначало нужно авторизоватся");
                return;
            }

            GroupsGetParams groupsGetParams = new GroupsGetParams();
            groupsGetParams.Extended = true;
            var res = api.Groups.Get(groupsGetParams);
            List<string> link_for_Photo = new List<string>();

            string directory = "IMG";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var i = 1;
            PhotoGetParams photoParams = new PhotoGetParams();

            //     //..   photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            int n = 1;



            PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            try
            {
                photoGetAlbumsParams.OwnerId = -Convert.ToInt64(textBox6.Text);
                photoParams.OwnerId = -Convert.ToInt64(textBox6.Text);

            }
            catch
            {
                MessageBox.Show("Ошибка введите группу");
                return;
            }
            var getAlbums = api.Photo.GetAlbums(photoGetAlbumsParams);
            photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;

            var photos = api.Photo.Get(photoParams);

            // string s= photos[0].Sizes[photos[0].Sizes.Count-1].Url.ToString();
            using (WebClient webClient = new WebClient())
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = photos.Count;
                LogWrite("Скачивание со стены");
                string file = @"https://vk.com/";

                foreach (var album in getAlbums)
                {

                    do
                    {
                        photoParams.Count = 50;
                        photos = api.Photo.Get(photoParams);
                        //    photoParams.Offset = 1000 * u;
                        //u++;
                        n = n + photos.Count;

                        LogWrite(n.ToString());

                        foreach (var photo in photos)
                        {
                            link_for_Photo.Add(photo.Sizes[photo.Sizes.Count - 1].Url.ToString());
                        }

                    } while (photos.Count == 1000);



                    directory = "IMG/" + photoGetAlbumsParams.OwnerId + "/" + album.Id;
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Id(album.Id);
                    photos = api.Photo.Get(photoParams);
                    progressBar1.Value = 0;
                    progressBar1.Maximum = photos.Count;
                    foreach (var photo in photos)
                    {

                        try
                        {
                            string savePath = Path.Combine(directory, photo.ToString());
                            // ТАК ТУТ МЕТКА    webClient.DownloadFile(photo.Sizes[photo.Sizes.Count - 1].Url.ToString(), savePath + ".jpg");

                            //   MessageBox.Show(file + photo.ToString());
                            //progressBar1.Increment(1);
                            //  textBox2.Text = progressBar1.Value + " / 1000";
                        }
                        catch
                        {
                            LogWrite("Ошибка загрузки фото: " + photo.Id);
                        }
                        progressBar1.Increment(1);
                        //     textBox2.Clear();
                        //  textBox2.AppendText(progressBar1.Value + " / " + progressBar1.Maximum.ToString());
                    }
                }

            }




            MessageBox.Show("end");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(30000);
        }




        private void LogWrite(string txt)
        {
            DateTime dateTime = DateTime.Now;
            textBox1.AppendText(dateTime.Hour + ":" + dateTime.Minute + "    " + txt + Environment.NewLine);
            textBox1.SelectionStart = textBox1.Text.Length;

            //   dateTime.
            // writer_in_file(@"txt/LOG/" + dateTime.Day + "." + dateTime.Month + "." + dateTime.Year + ".txt", dateTime.ToString() + "   " + txt);
            // form2.richtextbox(dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second + "   " + txt);
            // form2.textBox1_input(i_links.ToString(), str.Count.ToString());
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                AccessToken = api_token
            });

            Console.WriteLine(api.Token);

            GroupsGetParams groupsGetParams = new GroupsGetParams();
            groupsGetParams.Extended = true;
            //  groupsGetParams.
            //   var res = api.Groups.Get(groupsGetParams);

            /*  foreach (var g in res)
              {
                  long id = g.Id; // 214211
                  string name = g.Name; // "Какое либо название группы"

                  textBox1.AppendText(g.Id.ToString() + "  :  " + g.Name);

              }*/
            //   var upload = Api.Photo.GetUploadServer(00, 22822305);

            string directory = "IMG";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // try
            // {



            // List<var> photos = new List<var>()


            PhotoGetParams photoParams = new PhotoGetParams();
            photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            //     photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.

            photoParams.OwnerId = -146182119;
            int n = 0;
            photoParams.Count = 50;


            PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            photoGetAlbumsParams.OwnerId = -45739204;
            // var getAlbums = api.Photo.GetAlbums(photoGetAlbumsParams);

            var photos = api.Photo.Get(photoParams);

            photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            //     photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.

            photoParams.OwnerId = -145113826;
            //int n = 1;
            ulong i = 1;
            do
            {
                photoParams.Count = 1000;
                photos = api.Photo.Get(photoParams);
                photoParams.Offset = 1000 * i;
                i++;
                n = n + photos.Count;
                LogWrite(n.ToString());
            } while (photos.Count == 1000);
            MessageBox.Show(n.ToString());


            //    PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            photoGetAlbumsParams.OwnerId = -45739204;
            //getAlbums = api.Photo.GetAlbums(photoGetAlbumsParams);

            photos = api.Photo.Get(photoParams);

            // string s= photos[0].Sizes[photos[0].Sizes.Count-1].Url.ToString();
            using (WebClient webClient = new WebClient())
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = photos.Count;
                LogWrite("Скачивание со стены");
                string file = @"https://vk.com/";
                foreach (var photo in photos)
                {
                    directory = "IMG/" + photoGetAlbumsParams.OwnerId + "/Wall1";
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    try
                    {
                        string savePath = Path.Combine(directory, photo.ToString());
                        webClient.DownloadFile(photo.Sizes[photo.Sizes.Count - 1].Url.ToString(), savePath + ".jpg");
                        //   MessageBox.Show(file + photo.ToString());

                    }
                    catch
                    {
                        LogWrite("Ошибка загрузки фото: " + photo.Id);
                    }
                    progressBar1.Increment(1);
                    //    //textBox2.Clear();
                    // textBox2.AppendText(progressBar1.Value + " / " + progressBar1.Maximum.ToString());
                }

            }
        }
        string api_token;
        Form2 Form2 = new Form2();
        List<int> list_resi_int = new List<int>();
        private void Button4_Click(object sender, EventArgs e)
        { enter_in_VK(); }
        private void enter_in_VK()

        {
            GroupsGetParams groupsGetParams = new GroupsGetParams();
            groupsGetParams.Extended = true;

            var api = new VkApi();
            LogWrite("Производится вход");
            api.Authorize(new ApiAuthParams
            {
                ApplicationId = 153456,
                Login = "375447228549",
                Password = "1996103Kisa3",
                Settings = Settings.All
            });
            //    MessageBox.Show(api.Token.ToString());
            var res = api.Groups.Get(groupsGetParams);
            api_token = api.Token.ToString();
            /*  foreach (var g in res)
              {
                  long id = g.Id; // 214211
                  string name = g.Name; // "Какое либо название группы"

                  textBox1.AppendText(g.Id.ToString() + "  :  " + g.Name);

              }*/
            //   var upload = Api.Photo.GetUploadServer(00, 22822305);

            string directory = "IMG";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // try
            // {



            // List<var> photos = new List<var>()
            List<string> str = new List<string>();

            var i = 1;
            PhotoGetParams photoParams = new PhotoGetParams();
            photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            //     photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.
            if (File.Exists("complate.txt"))
            {
                StreamReader srts = new StreamReader("complate.txt");
                string srtu = srts.ReadToEnd();
                srts.Close();
                str = new List<string>(srtu.Split('.'));
                Regex r = new Regex(@"\r\n");
                for (int y = 0; y < str.Count; y++)
                {
                    str[y] = r.Replace(str[y], "");
                }
                str.Remove("");
            }
            bool fk = true;
            str.Sort();
            //  res.Sort();
            //   MessageBox.Show(textBox5.Text.Length.ToString());
            int chekedbox_i = 0;
            foreach (var resi in res)
            {
                if (str.Count > 0)
                {
                    for (int y = 0; y < str.Count; y++)
                    {
                        if (resi.Id == Math.Abs(Convert.ToInt32(str[y])))
                        {
                            str.Remove(str[y]);
                            LogWrite("Скачено уже: " +resi.Id+" : " + resi.Name); ;
                            y = str.Count;
                            fk = false;
                            //    y = str.Count;
                        }
                        // textBox5.SelectionStart = textBox5.Text.Length;
                    }
                    if (fk)
                    {
                        DateTime dateTime = DateTime.Now;
                        textBox5.AppendText(resi.Id + " :  " + resi.Name + Environment.NewLine);
                        textBox5.AppendText(new string('═', 54) + Environment.NewLine);
                        checkedListBox1.Items.Insert(chekedbox_i, resi.Name);
                        chekedbox_i++;
                        list_resi_int.Add(Convert.ToInt32(resi.Id));
                    }
                    fk = true;
                }

                else
                {
                    DateTime dateTime = DateTime.Now;
                    textBox5.AppendText(resi.Id + " :  " + resi.Name + Environment.NewLine);
                    textBox5.AppendText(new string('═', 54) + Environment.NewLine);
                }
            }
        }

        private async void Button5_Click(object sender, EventArgs e)
        {
            await down_wall_picsAsync();
        }
        private async Task down_wall_picsAsync()
        {
            Dictionary<Uri, string> dict = new Dictionary<Uri, string>();

            var api = new VkApi();

            try
            {
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = api_token
                });
            }
            catch
            {
                MessageBox.Show("Сначало нужно авторизоватся");
                return;
            }
            Console.WriteLine(api.Token);
            GroupsGetParams groupsGetParams = new GroupsGetParams();
            groupsGetParams.Extended = true;         
            string directory = "IMG";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // try
            // {



            // List<var> photos = new List<var>()


            PhotoGetParams photoParams = new PhotoGetParams();
            photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            //     photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.

            // photoParams.OwnerId = -146182119;
            // int n = 0;
            //   photoParams.Count = 50;
            // ulong l = Convert.ToInt64(textBox6.Text);

            PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            try
            {
                photoGetAlbumsParams.OwnerId = -Convert.ToInt64(textBox6.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка введите группу");
                return;
            }
            // var getAlbums = api.Photo.GetAlbums(photoGetAlbumsParams);

            var photos = api.Photo.Get(photoParams);

            photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            //     photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.

            List<string> link_for_Photo = new List<string>();
            int temp_i = 0;

            photoParams.OwnerId = -Convert.ToInt64(textBox6.Text);
            int n = 0;
            ulong i = 1;
            LogWrite("Началось скачивание со стены: создание списка");
            do
            {
                photoParams.Count = 1000;
                photos = api.Photo.Get(photoParams);
                photoParams.Offset = 1000 * i;
                i++;
                n = n + photos.Count;

                LogWrite(n.ToString());

                foreach (var photo in photos)
                {
                    directory = "IMG/" + photoGetAlbumsParams.OwnerId + "/Wall1";
                    string savePath = Path.Combine(directory, temp_i.ToString() + ".jpg");

                    //  link_for_Photo.Add(photo.Sizes[photo.Sizes.Count - 1].Url.ToString());
                    try
                    {
                        dict.Add(new Uri(photo.Sizes[photo.Sizes.Count - 1].Url.ToString()), savePath);
                    }
                    catch
                    {
                        LogWrite(temp_i.ToString());
                    }
                    temp_i++;
                }

            } while (photos.Count == 1000);
            //   MessageBox.Show(n.ToString());

            text_mesenger("Началось скачивание со стены: " + n.ToString());
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            //         SerializeDictionary(dict);
            await DownloadManyFiles(dict);
            LogWrite("Завершено скачивание со стены");
            System.IO.StreamWriter writer = new System.IO.StreamWriter("complate.txt", true);
            writer.WriteLine(-Convert.ToInt64(textBox6.Text) + ".");
            writer.Close();



            //Console.WriteLine("Текст записан в файл");


            //    PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            // photoGetAlbumsParams.OwnerId = -45739204;
            //getAlbums = api.Photo.GetAlbums(photoGetAlbumsParams);

            //photos = api.Photo.Get(photoParams);

            // string s= photos[0].Sizes[photos[0].Sizes.Count-1].Url.ToString();ъ\




            //ДАЛЬШЕ УДАЛИТЬ 


            /*

            progressBar1.Value = 0;
            progressBar1.Maximum = link_for_Photo.Count;
            LogWrite("Скачивание со стены");
            string file = @"https://vk.com/";
            foreach (var link_for_Photo_temp in link_for_Photo)
            {
                directory = "IMG/" + photoGetAlbumsParams.OwnerId + "/Wall1";
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                try
                {
                    //  using (WebClient webClient = new WebClient())
                    //  {
                    //  
                       //dict.Add(new Uri(link_for_Photo_temp), savePath + ".jpg");
                 //   dict.Add(new Uri("https://sun9-42.userapi.com/c845524/v845524981/160c93/nJ5aaGy-P7I.jpg"), "0.jpg");
                //    dict.Add(new Uri("https://sun9-27.userapi.com/c845524/v845524981/160ccd/rh0P4nvmWqw.jpg"), "1.jpg");
                 //   dict.Add(new Uri("https://sun9-43.userapi.com/c846417/v846417956/1536f1/sph3mHEujyI.jpg"), "2.jpg");

                    await DownloadManyFiles(dict);
                  //  _ = DownloadManyFiles(dict);
                    /// .///   webClient. webClient.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                    //  webClient.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                    //    await webClient.DownloadFileAsync(new Uri(link_for_Photo_temp), savePath + ".jpg");

                    LogWrite("Загружено фото: " + webClient.IsBusy);
                        //   MessageBox.Show(file + photo.ToString());
                    //}
                }
                catch (Exception e_1)
                {
                    LogWrite("ОШИБКА БУСС: " + webClient.IsBusy);

                    LogWrite("Ошибка загрузки фото: " + link_for_Photo_temp);
                    MessageBox.Show(e_1.Message);
                }
                progressBar1.Increment(1);
                textBox2.Clear();
                temp_i++;
                textBox2.AppendText(progressBar1.Value + " / " + progressBar1.Maximum.ToString());
            }*/
            text_mesenger("Скачивание со стены завершено");
            //MessageBox.Show("Dowm_Ok");
        }
        private async Task down_wall_picsAsync(int index_res)
        {
            Dictionary<Uri, string> dict = new Dictionary<Uri, string>();

            var api = new VkApi();

            try
            {
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = api_token
                });
            }
            catch
            {
                MessageBox.Show("Сначало нужно авторизоватся");
                return;
            }

            Console.WriteLine(api.Token);
            GroupsGetParams groupsGetParams = new GroupsGetParams();
            groupsGetParams.Extended = true;
            string directory = "IMG";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            PhotoGetParams photoParams = new PhotoGetParams();
            photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
          //  MessageBox.Show(photoGetAlbumsParams.);
            PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            try
            {
                photoGetAlbumsParams.OwnerId = -index_res;
            }
            catch
            {
                MessageBox.Show("Ошибка введите группу");
                return;
            }
            var photos = api.Photo.Get(photoParams);
            photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            List<string> link_for_Photo = new List<string>();
            int temp_i = 0;
            photoParams.OwnerId = -index_res;
            int n = 0;
            ulong i = 1;
            LogWrite("Началось скачивание со стены: создание списка");
            do
            {
                photoParams.Count = 1000;
                photos = api.Photo.Get(photoParams);
                photoParams.Offset = 1000 * i;
                i++;
                n = n + photos.Count;
                LogWrite(n.ToString());
                foreach (var photo in photos)
                {
                    directory = "IMG/" + photoGetAlbumsParams.OwnerId + "/Wall1";
                    string savePath = Path.Combine(directory, temp_i.ToString() + ".jpg");
                    try
                    {
                        dict.Add(new Uri(photo.Sizes[photo.Sizes.Count - 1].Url.ToString()), savePath);
                    }
                    catch
                    {
                        LogWrite(temp_i.ToString());
                    }
                    temp_i++;
                }

            } while (photos.Count == 1000);
            text_mesenger("Началось скачивание со стены: " + n.ToString());
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            await DownloadManyFiles(dict);
            LogWrite("Завершено скачивание со стены");
            System.IO.StreamWriter writer = new System.IO.StreamWriter("complate.txt", true);
            writer.WriteLine(index_res + ".");
            writer.Close();
            text_mesenger("Скачивание со стены завершено");
        }


        Stopwatch sw = new Stopwatch();
        System.Net.WebClient webClient = new System.Net.WebClient();
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calculate download speed and output it to labelSpeed.
            Form2.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));

            // Update the progressbar percentage only when the value is not the same.
            //  progressBar.Value = e.ProgressPercentage;

            // Show the percentage on our label.
            // labelPerc.Text = e.ProgressPercentage.ToString() + "%";

            // Update the label with how much data have been downloaded so far and the total size of the file we are currently downloading

        }
        public async Task DownloadManyFiles(Dictionary<Uri, string> files)
        {
            progressBar2.Value = 0;
            progressBar2.Maximum = files.Count;
            sw.Start();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);

            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += (s, e) => progressBar1.Value = e.ProgressPercentage;
            wc.DownloadProgressChanged += (s, e) => textBox2.Text = e.BytesReceived.ToString();

            // wc.DownloadProgressChanged += (s, e) => label5.Text= ((e.BytesReceived / 1024d / 1024d).ToString("0.00"),(e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00")).ToString();
            //     .//wc.DownloadProgressChanged += (s, e) => label5.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));

            foreach (KeyValuePair<Uri, string> pair in files)
            {
                //wc.DownloadFileTaskAsync(files[1].Length files[1].Value);
                try
                {
                    await wc.DownloadFileTaskAsync(pair.Key, pair.Value);
                }
                catch (Exception ex)
                {
                    LogWrite(ex.Message);
                }
                //  await wc.DownloadFileTaskAsync(pair.Key, pair.Value);
                progressBar2.Increment(1);
                label4.Text = progressBar2.Value + " / " + progressBar2.Maximum;
                notifyIcon1.Text = label4.Text;
                notifyIcon1.BalloonTipText = label4.Text;
                //       CreateTextIcon(label4.Text);
                //   notifyIcon1.Site = label4.Text;



            }
            wc.Dispose();
        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Reset the stopwatch.
            sw.Reset();

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                LogWrite(sender.ToString());
            }
        }
        /*
        public async Task DownloadManyFiles(Uri uri, string files)
        {
            WebClient wc = new WebClient();
       //     wc.DownloadProgressChanged += (s, e) => progressBar1.Value = e.ProgressPercentage;
            await wc.DownloadFileTaskAsync(uri, files); 
            wc.Dispose();
            MessageBox.Show(files);
        }*/

        private void button8_Click(object sender, EventArgs e)
        {
            Form2.Close();
        }
        /* public void CreateTextIcon(string str)
         {
             Font fontToUse = new Font("Trebuchet MS", 7, FontStyle.Regular, GraphicsUnit.Pixel);
             Brush brushToUse = new SolidBrush(Color.White);
             Bitmap bitmapText = new Bitmap(16,16);
             Graphics g = System.Drawing.Graphics.FromImage(bitmapText);

             IntPtr hIcon;

             g.Clear(Color.Transparent);
             g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
             g.DrawString(str, fontToUse, brushToUse, -2, 0);
             hIcon = (bitmapText.GetHicon());
             notifyIcon1.Icon = System.Drawing.Icon.FromHandle(hIcon);

             //DestroyIcon(hIcon.ToInt32);
         }*/
        private void Button6_Click(object sender, EventArgs e) // тут пизда
        {
            down_load_albums();
        }
        private async void down_load_albums()

        {
            int temp_i = 0;
            Dictionary<Uri, string> dict = new Dictionary<Uri, string>();
            var api = new VkApi();
            try
            {
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = api_token
                });
            }
            catch
            {
                MessageBox.Show("Сначало нужно авторизоватся");
                return;
            }

            Console.WriteLine(api.Token);
            GroupsGetParams groupsGetParams = new GroupsGetParams();
            groupsGetParams.Extended = true;
            var res = api.Groups.Get(groupsGetParams);
            string directory = "IMG";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var i = 1;
            PhotoGetParams photoParams = new PhotoGetParams();
            PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            try
            {
                photoGetAlbumsParams.OwnerId = -Convert.ToInt64(textBox6.Text);
                photoParams.OwnerId = -Convert.ToInt64(textBox6.Text);

            }
            catch
            {
                MessageBox.Show("Ошибка введите группу");
                return;
            }

            // photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            int n = 1;
            // PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            //  photoGetAlbumsParams.OwnerId = -145113826;  

            try
            {
            }
            catch
            {
                LogWrite("Ошибка доступа");
            }
            var getAlbums = api.Photo.GetAlbums(photoGetAlbumsParams);
            progressBar2.Maximum = getAlbums.Count;

            //   = api.Photo.Get(photoParams);
            ulong u = 1;
            int temp_photo_count;

            progressBar3.Value = 0;
            LogWrite("Скачивание с альбомов");
            string file = @"https://vk.com/";
            List<string> link_for_Photo = new List<string>();
            foreach (var album in getAlbums)
            {

                directory = "IMG/" + photoGetAlbumsParams.OwnerId + "/" + album.Title;
                if (!Directory.Exists(directory))
                {
                    try
                    {

                        Directory.CreateDirectory(directory);
                    }
                    catch
                    {
                        directory = "IMG/" + album.Id;
                        Directory.CreateDirectory(directory);
                    }

                }
                if (directory.IndexOf("\n") >= 0)
                    directory = directory.Remove(directory.IndexOf("\n"));
                do
                {
                    photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Id(album.Id);
                    photoParams.Count = 1000;
                    var photos = api.Photo.Get(photoParams);

                    photoParams.Offset = 1000 * u;
                    u++;
                    n = n + photos.Count;
                    temp_photo_count = photos.Count;
                    //  LogWrite(n.ToString());

                    foreach (var photo in photos)
                    {
                        string savePath = Path.Combine(directory, temp_i.ToString() + ".jpg");


                        try
                        {
                            dict.Add(new Uri(photo.Sizes[photo.Sizes.Count - 1].Url.ToString()), savePath);


                        }
                        catch
                        {
                            LogWrite("Не удалось получть фотографию: " + photo.Id);
                        }
                        temp_i++;
                    }
                    LogWrite(album.Title + "     " + photos.Count);
                    //  photos.d
                } while (temp_photo_count == 1000);
                progressBar3.Maximum = link_for_Photo.Count;
                photoParams.Offset = 0;

                //    LogWrite(album.Title + " : " + link_for_Photo.Count.ToString());




            }
            int l_link = 0;
            text_mesenger("Скачивание с альбомов началось : " + dict.Count);

            await DownloadManyFiles(dict);
            System.IO.StreamWriter writer = new System.IO.StreamWriter("complate.txt", true);
            writer.WriteLine(-Convert.ToInt64(textBox6.Text) + ".");
            writer.Close();
            text_mesenger("Скачивание с альбомов завершено");

        }

        private async void down_load_albums(int resi_index)

        {
            int temp_i = 0;
            Dictionary<Uri, string> dict = new Dictionary<Uri, string>();
            var api = new VkApi();
            try
            {
                api.Authorize(new ApiAuthParams
                {
                    AccessToken = api_token
                });
            }
            catch
            {
                MessageBox.Show("Сначало нужно авторизоватся");
                return;
            }

            Console.WriteLine(api.Token);
            GroupsGetParams groupsGetParams = new GroupsGetParams();
            groupsGetParams.Extended = true;
            var res = api.Groups.Get(groupsGetParams);
            string directory = "IMG";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var i = 1;
            PhotoGetParams photoParams = new PhotoGetParams();
            PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            try
            {
                photoGetAlbumsParams.OwnerId = -resi_index;
                photoParams.OwnerId = -resi_index;

            }
            catch
            {
                MessageBox.Show("Ошибка введите группу");
                return;
            }

            // photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Wall;
            int n = 1;
            // PhotoGetAlbumsParams photoGetAlbumsParams = new PhotoGetAlbumsParams();
            //  photoGetAlbumsParams.OwnerId = -145113826;  
            //=api.Photo.GetAlbums(api.Wall);
            try
            {
            }
            catch
            {
                LogWrite("Ошибка доступа");
            }
            try
            {
                var getAlbums = api.Photo.GetAlbums(photoGetAlbumsParams);
                progressBar2.Maximum = getAlbums.Count;
                //   = api.Photo.Get(photoParams);
                ulong u = 1;
                int temp_photo_count;
                progressBar3.Value = 0;
                LogWrite("Скачивание с альбомов");
                string file = @"https://vk.com/";
                List<string> link_for_Photo = new List<string>();
                foreach (var album in getAlbums)
                {
                    directory = "IMG/" + photoGetAlbumsParams.OwnerId + "/" + album.Title;
                    if (!Directory.Exists(directory))
                    {
                        try
                        {

                            Directory.CreateDirectory(directory);
                        }
                        catch
                        {
                            directory = "IMG/" + album.Id;
                            Directory.CreateDirectory(directory);
                        }
                    }
                    if (directory.IndexOf("\n") >= 0)
                        directory = directory.Remove(directory.IndexOf("\n"));
                    do
                    {
                        photoParams.AlbumId = VkNet.Enums.SafetyEnums.PhotoAlbumType.Id(album.Id);
                        photoParams.Count = 1000;
                        var photos = api.Photo.Get(photoParams);

                        photoParams.Offset = 1000 * u;
                        u++;
                        n = n + photos.Count;
                        temp_photo_count = photos.Count;
                        //  LogWrite(n.ToString());
                        foreach (var photo in photos)
                        {
                            string savePath = Path.Combine(directory, temp_i.ToString() + ".jpg");
                            try
                            {
                                dict.Add(new Uri(photo.Sizes[photo.Sizes.Count - 1].Url.ToString()), savePath);


                            }
                            catch
                            {
                                LogWrite("Не удалось получть фотографию: " + photo.Id);
                            }
                            temp_i++;
                        }
                        LogWrite(album.Title + "     " + photos.Count);
                    } while (temp_photo_count == 1000);
                    progressBar3.Maximum = link_for_Photo.Count;
                    photoParams.Offset = 0;
                }

            }
            catch
            {
                LogWrite("Ошибка доступа");
            }
            int l_link = 0;
            text_mesenger("Скачивание с альбомов началось : " + dict.Count);
            await DownloadManyFiles(dict);
            System.IO.StreamWriter writer = new System.IO.StreamWriter("complate.txt", true);
            writer.WriteLine(resi_index + ".");
            writer.Close();
            text_mesenger("Скачивание с альбомов завершено");
        }


        private void WebClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
        bool top_most_form_main = true;
        private void Button7_Click(object sender, EventArgs e)
        {
            if (top_most_form_main)
            {
                this.TopMost = true;
                button7.Text = "На задний план";
                top_most_form_main = false;
            }
            else
            {
                this.TopMost = false;
                button7.Text = "На передний план";
                top_most_form_main = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Some Title";
            notifyIcon1.BalloonTipText = "Some Notification";
            notifyIcon1.Text = label4.Text;

        }

        private void text_mesenger(string str)
        {
            notifyIcon1.Icon = SystemIcons.Exclamation;
            notifyIcon1.BalloonTipTitle = "DOwn_LOader_VK";
            notifyIcon1.BalloonTipText = str;
            //notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(30000);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
            else if (FormWindowState.Normal == this.WindowState)
            { notifyIcon1.Visible = false; }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Form2 ;
            //Form2.insert("23");
            //Form2.Opacity = 50; 
            try
            {
                Form2.Show();
            }
            catch
            {
                Form2 = new Form2();
                Form2.Show();
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = trackBar1.Value / 1000.0;
            // MessageBox.Show(this.Opacity.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (top_most_form_main_counter)
                {
                    Form2.TopMost = true;
                    button10.Text = "На задний план";
                    top_most_form_main_counter = false;
                }
                else
                {
                    Form2.TopMost = false;
                    button10.Text = "На передний план";
                    top_most_form_main_counter = true;
                }
            }
            catch
            {

            }
        }

        bool top_most_form_main_counter = false;

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            Form2.Opacity = trackBar2.Value / 1000.0;
        }

        private void label4_TextChanged(object sender, EventArgs e)
        {
            Form2.insert(label4.Text);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            enter_in_VK();
            Form2.Show();
        }

        private async void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            string checkedStr = "X";
            string uncheckedStr = "-";
            string lbValue = "";
            if (e.NewValue == CheckState.Checked)
            {
                lbValue = checkedStr;
        //        MessageBox.Show(CheckState.Checked.ToString());
            }
            else
            {
                lbValue = uncheckedStr;
            }
            checkedListBox1.Items[e.Index] = lbValue;
       //     MessageBox.Show(e.Index.ToString() + "\n" + list_resi_int[e.Index]);
            await down_wall_picsAsync( list_resi_int[e.Index]);
            down_load_albums(list_resi_int[e.Index]);
        }
    }
}