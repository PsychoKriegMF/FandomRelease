using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FandomRelease
{
    public partial class MainForm : Form
    {
        private DataBase database;

        public MainForm()
        {
            InitializeComponent();
            database = new DataBase();
            database.InitializeDatabase();
            LoadCharacters();
            dgvCharacters.CellContentClick += dgvCharacters_CellContentClick;
        }

        private void LoadCharacters()
        {
            try
            {
                using (var connection = new SQLiteConnection("Data Source=characters.db;Version=3;"))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Characters";
                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            dgvCharacters.DataSource = dataTable; 
                            dgvCharacters.Columns["Id"].Visible = false; // Скрыть колонку с ID
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new AddEditCharacterForm();
            addForm.ShowDialog();
            LoadCharacters();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли какая-либо ячейка в DataGridView
            if (dgvCharacters.CurrentCell != null && dgvCharacters.CurrentCell.RowIndex >= 0)
            {
                // Получаем индекс выбранной строки
                int rowIndex = dgvCharacters.CurrentCell.RowIndex;
                var characterId = Convert.ToInt32(dgvCharacters.Rows[rowIndex].Cells["Id"].Value); // Получаем ID персонажа

                // Подтверждение удаления
                var result = MessageBox.Show(
                    "Вы уверены, что хотите удалить этого персонажа?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (var connection = new SQLiteConnection("Data Source=characters.db;Version=3;"))
                        {
                            connection.Open();
                            string deleteQuery = "DELETE FROM Characters WHERE Id = @Id";
                            using (var command = new SQLiteCommand(deleteQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Id", characterId); // Передаем ID в параметр запроса
                                command.ExecuteNonQuery(); // Выполняем запрос на удаление
                            }                            
                        }

                        MessageBox.Show("Персонаж успешно удален."); // Сообщаем пользователю об успешном удалении
                        LoadCharacters(); // Обновляем список персонажей
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Ошибка при удалении персонажа: " + ex.Message); // Обработка ошибок базы данных
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка: " + ex.Message); // Обработка других ошибок
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите персонажа для удаления."); // Сообщаем, если персонаж не выбран
            }
        }
        private void ExportToCsv(string filePath)
        {
            // Логика экспорта данных в файл CSV
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Name,Description,ImagePath,Links");
                // Проход по всем персонажам и запись в файл
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Сохранить файл как";
                saveFileDialog.FileName = "characters_export.csv"; // Устанавливаем имя файла по умолчанию

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName; // Получаем путь к выбранному файлу

                    try
                    {
                        using (var connection = new SQLiteConnection("Data Source=characters.db;Version=3;"))
                        {
                            connection.Open();
                            string selectQuery = "SELECT * FROM Characters";
                            using (var command = new SQLiteCommand(selectQuery, connection))
                            using (var reader = command.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    MessageBox.Show("Нет данных для экспорта.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                using (var writer = new StreamWriter(filePath))
                                {
                                    // Записываем заголовки столбцов
                                    writer.WriteLine("Id,Name,Description,ImagePath,Links");

                                    // Проходим по всем записям и записываем их в файл
                                    while (reader.Read())
                                    {
                                        // Форматируем строку для записи
                                        var line = $"{reader["Id"]},{EscapeCsvValue(reader["Name"].ToString())}," +
                                                   $"{EscapeCsvValue(reader["Description"].ToString())}," +
                                                   $"{EscapeCsvValue(reader["ImagePath"].ToString())}," +
                                                   $"{EscapeCsvValue(reader["Links"].ToString())}";
                                        writer.WriteLine(line); // Записываем строку в файл
                                    }
                                }
                            }
                        }

                        MessageBox.Show("Данные успешно экспортированы в файл: " + filePath); // Сообщаем пользователю об успешном экспорте
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Ошибка при экспорте данных: " + ex.Message); // Обработка ошибок базы данных
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка: " + ex.Message); // Обработка других ошибок
                    }
                }
            }
        }

        private string EscapeCsvValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return ""; 
            }
            // Экранируем кавычки и оборачиваем значение в кавычки
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        private void dgvCharacters_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что кликнули по допустимому индексу строки (не заголовок и не ссылка)
            if (e.RowIndex >= 0 && dgvCharacters.Columns[e.ColumnIndex].Name != "Links")
            {
                int characterId = Convert.ToInt32(dgvCharacters.Rows[e.RowIndex].Cells["Id"].Value); // Получаем ID персонажа

                // Открываем форму для редактирования персонажа
                using (var addEditCharacterForm = new AddEditCharacterForm(characterId))
                {
                    if (addEditCharacterForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadCharacters(); // Обновляем список персонажей после редактирования
                    }
                }
            }
        }

        private void dgvCharacters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, что кликнули по нужному индексу строки (ссылка)
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvCharacters.Columns["Links"].Index) 
            {
                var link = dgvCharacters.Rows[e.RowIndex].Cells["Links"].Value?.ToString();
                if (!string.IsNullOrEmpty(link))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(link); // Открываем ссылку в браузере
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при открытии ссылки: " + ex.Message); // Обработка ошибок
                    }
                }
            }
        }
    }
}
