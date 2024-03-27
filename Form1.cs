using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using ZPA_Meteostanice.data;
using ZPA_Meteostanice.helpers;

namespace ZPA_Meteostanice
{
    public partial class Form1 : Form
    {
        const string connectionUri = "mongodb+srv://bigi:1234@zpa-ukol.aaqrxin.mongodb.net/?retryWrites=true&w=majority&appName=ZPA-Ukol";
        private DatabaseHelper<MeteoData> dbHelper;

        public Form1()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper<MeteoData>(connectionUri, "zpa_ukol", "data");
            LoadDataToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private async void timer_Tick(object sender, EventArgs e)
        {
            var generator = new DataSender();
            var data = generator.sendData();

            await SaveToDb(data);
        }

        private async Task SaveToDb(MeteoData md)
        {
            if (md != null)
            {
                await dbHelper.collection.InsertOneAsync(md);
                label1.Text = "data ulozena do databaze";
            }
        }

        private List<MeteoData> LoadDataToList()
        {
            var filter = Builders<MeteoData>.Filter.Empty;
            var result = dbHelper.collection.Find(filter).ToList();
            return result;
        }
    }
}
