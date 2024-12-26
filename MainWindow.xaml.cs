using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MilitaryDraft
{
    public partial class MainWindow : Window
    {
        public class Prizyvnik
        {
            public string FullName { get; set; } // ФИО
            public DateTime DateOfBirth { get; set; } // Дата рождения
            public string HealthGroup { get; set; } // Группа здоровья

            public override string ToString()
            {
                return $"{FullName} | Дата рождения: {DateOfBirth:dd.MM.yyyy} | Группа здоровья: {HealthGroup}";
            }
        }
        // Коллекция для хранения призывников
        private List<Prizyvnik> prizyvniki = new List<Prizyvnik>();

        public bool IsVisible { get; set; }

        // Добавление призывника
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || comboHealthGroup.SelectedItem == null || datePickerBirth.SelectedDate == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создание нового призывника
            Prizyvnik newPrizyvnik = new Prizyvnik
            {
                FullName = txtFullName.Text,
                DateOfBirth = datePickerBirth.SelectedDate.Value,
                HealthGroup = (comboHealthGroup.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            // Добавление в список
            prizyvniki.Add(newPrizyvnik);
            UpdateListBox();
            ClearFields();
        }

        // Сортировка по алфавиту
        private void Button_Sort_Click(object sender, RoutedEventArgs e)
        {
            prizyvniki = prizyvniki.OrderBy(p => p.FullName).ToList();
            UpdateListBox();
        }

        // Обратная сортировка по алфавиту
        private void Button_ReverseSort_Click(object sender, RoutedEventArgs e)
        {
            prizyvniki = prizyvniki.OrderByDescending(p => p.FullName).ToList();
            UpdateListBox();
        }

        // Удаление выбранного призывника
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxPrizyvniki.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите призывника для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Удаляем выбранный элемент из списка
            prizyvniki.RemoveAt(listBoxPrizyvniki.SelectedIndex);
            UpdateListBox();
        }

        // Обновление ListBox
        private void UpdateListBox()
        {
            listBoxPrizyvniki.Items.Clear();
            foreach (var p in prizyvniki)
            {
                listBoxPrizyvniki.Items.Add(p.ToString());
            }
        }

        // Очистка полей ввода
        private void ClearFields()
        {
            txtFullName.Clear();
            datePickerBirth.SelectedDate = null;
            comboHealthGroup.SelectedIndex = -1;
        }

        public void Show()
        {
            throw new NotImplementedException();
        }
    }
}
