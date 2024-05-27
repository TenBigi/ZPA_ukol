using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders;
using System.Runtime.CompilerServices;
using ZPA_Meteostanice.data;
using ZPA_Meteostanice.helpers;

namespace ZPA_Meteostanice
{
    public partial class Form1 : Form
    {
        const string connectionUri = "mongodb+srv://bigi:1234@zpa-ukol.aaqrxin.mongodb.net/?retryWrites=true&w=majority&appName=ZPA-Ukol";
        private DataDbHelper DataHelper;
        private StationDbHelper StationHelper;
        private BindingSource weatherDataSource;
        private List<DataProjection> weatherDataProjection;
        private System.Windows.Forms.Timer timer;
        public Form1()
        {
            InitializeComponent();
            InicializeDataGrid();
            DataHelper = new DataDbHelper(connectionUri, "zpa_ukol");
            StationHelper = new StationDbHelper(connectionUri, "zpa_ukol");
            InicializeTimer();

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadWeatherData();
        }

        private void InicializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000; //stanice posila data kazdych 5s
            timer.Tick += async (sender, e) => await GenerateDataAndRefresh();
            timer.Start();
        }

        //simulace ukladani dat z meteostanice primo do databaze a jejich nasledne nacteni do listu
        private async Task GenerateDataAndRefresh()
        {
            var generator = new DataSender();
            var data = generator.sendData();

            await SaveToDb(data);
            await LoadWeatherData();
        }

        //ulozeni do databaze
        private async Task SaveToDb(MeteoData md)
        {
            if (md != null)
            {
                await dbHelper.collection.InsertOneAsync(md);
                label1.Text = "data ulozena do databaze";
            }
        }

        //naplneni data grid view novymi daty
        private async Task LoadWeatherData()
        {
            var weatherData = await dbHelper.GetDataAsync();

            //premapovani objektu z databaze do classy DataProjection - vynechani nekterych properties, v mem pripade Id
            weatherDataProjection = weatherData.Select(data => new DataProjection
            {
                Timestamp = data.timestamp,
                Temperature = data.temperature,
                WindSpeed = data.windSpeed,
                Humidity = data.humidity,
                Pressure = data.pressure
            }).ToList();

            await UpdateBindingSource();
        }

        private void InicializeDataGrid()
        {
            weatherDataSource = new BindingSource();
            dataGridView1.DataSource = weatherDataSource;
        }

        private async Task UpdateBindingSource()
        {
            weatherDataSource.DataSource = null;
            weatherDataSource.DataSource = weatherDataProjection;
        }
    }
}
