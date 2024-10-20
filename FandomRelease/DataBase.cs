using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Drawing;

namespace FandomRelease
{
    public class DataBase
    {
        private const string connectionString = "Data Source=characters.db;Version=3;";

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Characters (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Image BLOB,
                        Name TEXT NOT NULL,
                        Description TEXT,
                        ImagePath TEXT,
                        Links TEXT
                    );";
                var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }
        public void AddCharacter(Image image, string name, string description, string imagePath, string links)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Characters (Image, Name, Description, ImagePath, Links) VALUES (@Image, @Name, @Description, @ImagePath, @Links)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Image", ImageToByteArray(image)); // Преобразуем изображение в массив байтов
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@ImagePath", imagePath);
                    command.Parameters.AddWithValue("@Links", links);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для преобразования изображения в массив байтов
        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Сохранить изображение в формате PNG
                return ms.ToArray(); // Возвращаем массив байтов
            }
        }


    }
}
