using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders;
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
            
            LoadData();
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

        private async void LoadData()
        {
            try
            {
                List<MeteoData> dataList = SquareData(await dbHelper.GetDataAsync());
                dataGridView1.DataSource = dataList;
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
            
        }

        private List<MeteoData> SquareData(List<MeteoData> data)
        {
            List<MeteoData> display = new();
            foreach (var item in data)
            {
                display.Add(new MeteoData
                {
                    timestamp = item.timestamp,
                    temperature = Math.Round(item.temperature, 2),
                    humidity = Math.Round(item.humidity, 2),
                    pressure = Math.Round(item.pressure, 2),
                    windSpeed = Math.Round(item.windSpeed, 2),
                });
            }

            return display;
        }
    }
}
