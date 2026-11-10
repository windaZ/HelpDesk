using System;
using System.Windows;
using System.Windows.Controls;
using HelpDesk.Models;

namespace HelpDesk.Views
{
    public partial class TicketEditWindow : Window
    {
        private Ticket _selectedTicket;

        public TicketEditWindow(Ticket selectedTicket)
        {
            InitializeComponent();

            _selectedTicket = selectedTicket;

            LoadTicketData();
        }

        private void LoadTicketData()
        {
            if (_selectedTicket == null) return;

            TicketIdTextBlock.Text = _selectedTicket.Id.ToString();

            DescriptionTextBox.Text = _selectedTicket.Description;


            SetComboBoxValue(CategoryComboBox, _selectedTicket.Category);

            SetComboBoxValue(PriorityComboBox, _selectedTicket.Priority);

            SetComboBoxValue(StatusComboBox, _selectedTicket.Status);
        }

        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Content.ToString() == value)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show(
                    "Описание проблемы не может быть пустым.",
                    "Проверка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show(
                    "Выберите категорию заявки.",
                    "Проверка данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            _selectedTicket.Category = (CategoryComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            _selectedTicket.Description = DescriptionTextBox.Text.Trim();
            _selectedTicket.Priority = (PriorityComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            _selectedTicket.Status = (StatusComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            MessageBox.Show(
                "Изменения успешно сохранены в памяти приложения!\nНа следующих занятиях данные будут обновляться в MS SQL Server.",
                "HelpDesk",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
