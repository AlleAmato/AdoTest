using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdoTest.Models;

namespace AdoTest.ViewModels
{

    internal class MainWindowViewModel
    {
        internal void Click()
        {
    string connectionString =   @"Server=DESKTOP-24NCMVV\SQLEXPRESS; Database=Test; 
                                        Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //non usare mai concatenazione di stringhe per prendere parametri
                    connection.Open();
                    /*SqlCommand command = new SqlCommand();
                    command.CommandText = "insert into Configurations([key], [value]) values('Font', 'Comic Sans')";
                    command.Connection = connection;
                    command.ExecuteNonQuery();

                    command.CommandText = "select count(*) as Conteggio from Configurations";
                    int righe = (int)command.ExecuteScalar();
                    MessageBox.Show(righe.ToString());*/
                    List<Configuration> configurations = new List<Configuration>();//creo la lista di oggetti configuration
                    SqlCommand command = new SqlCommand("Select * from Configurations", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())//mi permette di scorrere tutto
                        {
                            //prendo le righe e creo gli oggetti dal database
                            Configuration c = new Configuration
                            {
                                Id = (int)reader["Id"],
                                Key = (string)reader["Key"],
                                Value = (string)reader["Value"]
                            };
                            configurations.Add(c);//aggiungo la riga alla lista
                        }
                            
                    }
                }
                catch(Exception){
                    throw;
                }
            }
        }
    }
}