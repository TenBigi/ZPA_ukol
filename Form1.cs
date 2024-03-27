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
        private BindingSource weatherDataSource;
        private List<MeteoData> weatherData;

        public Form1()
        {
            InitializeComponent();
            InicializeDataGrid();
            dbHelper = new DatabaseHelper<MeteoData>(connectionUri, "zpa_ukol", "data");
            weatherData = dbHelper.GetData();
            weatherDataSource.DataSource = weatherData;

            timer.Start();
        }

        private async void timer_Tick(object sender, EventArgs e)
        {
            await GenerateDataAndSaveIt();
        }

        private async Task GenerateDataAndSaveIt()
        {
            var generator = new DataSender();
            var data = generator.sendData();

            await SaveToDb(data);
            weatherData = await dbHelper.GetDataAsync();

            await UpdateBindingSource();
        }

        private async Task SaveToDb(MeteoData md)
        {
            if (md != null)
            {
                await dbHelper.collection.InsertOneAsync(md);
                label1.Text = "data ulozena do databaze";
            }
        }

        private void InicializeDataGrid()
        {
            weatherDataSource = new BindingSource();
            dataGridView1.DataSource = weatherDataSource;
        }

        private async Task UpdateBindingSource()
        {
            weatherDataSource.DataSource = null;
            weatherDataSource.DataSource = weatherData;
        }
    }
}
