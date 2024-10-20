using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace FandomRelease
{
    public partial class AddEditCharacterForm : Form
    {
        private int characterId; // ID персонажа для редактирования

        public AddEditCharacterForm(int id = -1)
        {
            InitializeComponent();
            characterId = id;

            if (characterId != -1)
            {
                LoadCharacterData(); // Загружаем данные персонажа для редактирования
            }
            else    // Открытие формы с доступными для заполнения полями и кнопками
            {
                btnEdit.Visible = false;
                btnSave.Visible = true;
                btnSelectImage.Visible = true;
                tbDescription.ReadOnly = false;
                tbName.ReadOnly = false;
                tbLink.ReadOnly = false;                
            }

        }
        private async void LoadCharacterData()
        {
            try
            {
                using (var connection = new SQLiteConnection("Data Source=characters.db;Version=3;"))
                {
                    await connection.OpenAsync(); // Асинхронное открытие соединения
                    string selectQuery = "SELECT * FROM Characters WHERE Id = @Id"; // Подготовка запроса
                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", characterId); // Передаем ID персонажа в параметр запроса

                        using (var reader = await command.ExecuteReaderAsync()) // Асинхронное выполнение запроса
                        {
                            if (await reader.ReadAsync()) // Асинхронное чтение данных
                            {
                                tbName.Text = reader["Name"].ToString(); // Заполнение полей данными
                                tbDescription.Text = reader["Description"].ToString();
                                tbImagePath.Text = reader["ImagePath"].ToString();
                                tbLink.Text = reader["Links"].ToString();

                                // Загрузить изображение
                                string imagePath = reader["ImagePath"].ToString();
                                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                                {
                                    pictureBox.Image = Image.FromFile(imagePath); // Загрузка изображения
                                }
                            }
                            else
                            {
                                MessageBox.Show("Персонаж не найден.");
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Ошибка при загрузке данных о персонаже: " + ex.Message); // Обработка ошибок базы данных
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message); // Обработка других ошибок
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;

            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Сохраняем изображение в формате PNG
                return ms.ToArray(); // Возвращаем массив байтов
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Проверяем, заполнены ли необходимые поля
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (имя и описание).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем изображение из PictureBox, если оно есть
            Image characterImage = pictureBox.Image;
            byte[] imageBytes = characterImage != null ? ImageToByteArray(characterImage) : null; // Конвертируем изображение в массив байтов

            try
            {
                using (var connection = new SQLiteConnection("Data Source=characters.db;Version=3;"))
                {
                    await connection.OpenAsync(); // Асинхронное открытие соединения
                    if (characterId == -1) // Добавляем нового персонажа
                    {
                        string insertQuery = "INSERT INTO Characters (Image, Name, Description, ImagePath, Links) VALUES (@Image, @Name, @Description, @ImagePath, @Links)";
                        using (var command = new SQLiteCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Name", tbName.Text);
                            command.Parameters.AddWithValue("@Image", imageBytes != null ? (object)imageBytes : DBNull.Value);
                            command.Parameters.AddWithValue("@Description", tbDescription.Text);
                            command.Parameters.AddWithValue("@ImagePath", tbImagePath.Text);
                            command.Parameters.AddWithValue("@Links", tbLink.Text);
                            await command.ExecuteNonQueryAsync(); // Асинхронное выполнение команды
                        }
                        MessageBox.Show("Персонаж успешно добавлен.");
                    }
                    else // Обновляем существующего персонажа
                    {
                        string updateQuery = "UPDATE Characters SET Image = @Image, Name = @Name, Description = @Description, ImagePath = @ImagePath, Links = @Links WHERE Id = @Id";
                        using (var command = new SQLiteCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Id", characterId);
                            command.Parameters.AddWithValue("@Image", imageBytes != null ? (object)imageBytes : DBNull.Value);
                            command.Parameters.AddWithValue("@Name", tbName.Text);
                            command.Parameters.AddWithValue("@Description", tbDescription.Text);
                            command.Parameters.AddWithValue("@ImagePath", tbImagePath.Text);
                            command.Parameters.AddWithValue("@Links", tbLink.Text);
                            await command.ExecuteNonQueryAsync(); // Асинхронное выполнение команды
                        }
                        MessageBox.Show("Персонаж успешно обновлён.");
                    }
                }            
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Ошибка при сохранении данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите изображение персонажа";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files (*.*)|*.*"; 
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); // Начальная директория - "Мои изображения"

                // Проверка, была ли выбрана картинка
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Получаем путь к выбранному изображению
                    string selectedImagePath = openFileDialog.FileName;

                    // Отображаем путь в текстовом поле
                    tbImagePath.Text = selectedImagePath;

                    // Загружаем и отображаем изображение в PictureBox 
                    try
                    {
                        if (File.Exists(selectedImagePath))
                        {
                            // Загружаем изображение в PictureBox
                            using (Image image = Image.FromFile(selectedImagePath)) // Используем using для освобождения ресурсов
                            {
                                pictureBox.Image = new Bitmap(image); // Создаем новый Bitmap для PictureBox
                            }
                        }
                        else
                        {
                            MessageBox.Show("Файл не существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Файл не является допустимым изображением.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Обработка ошибок при загрузке изображения
                    }
                }
            }
        }
        //Открытие функция редактирования
        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            btnSelectImage.Visible = true;
            tbDescription.ReadOnly = false;
            tbName.ReadOnly = false;
            tbLink.ReadOnly = false;
        }
    }
}
