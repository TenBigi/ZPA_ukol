using MongoDB.Driver;

namespace ZPA_Meteostanice
{
    public partial class Form1 : Form
    {
        const string connectionUri = "mongodb+srv://bigi:1234@zpa-ukol.aaqrxin.mongodb.net/?retryWrites=true&w=majority&appName=ZPA-Ukol";
        private DatabaseHelper dbHelper;

        public Form1()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper(connectionUri, "sample_mflix", "movies");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dbHelper.Search("title", "Back to the Future");
        }
    }
}
