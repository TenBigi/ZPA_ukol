using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ZPA_Meteostanice
{
    public partial class Form1 : Form
    {
        const string connectionUri = "mongodb+srv://bigi:1234@zpa-ukol.aaqrxin.mongodb.net/?retryWrites=true&w=majority&appName=ZPA-Ukol";
        private DatabaseHelper dbHelper;

        public Form1()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(connectionUri, "zpa_ukol", "data");
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
                await dbHelper.collection.InsertOneAsync(md.ToBsonDocument());
                label1.Text = "data ulozena do databaze";
            }
            
        }
    }
}
