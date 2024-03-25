using MongoDB.Driver;
using MongoDB.Bson;

namespace ZPA_Meteostanice
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}