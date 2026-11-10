using HelpDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HelpDesk.Views
{
    public partial class MainWindow : Window
    {
        private List<Ticket> tickets;

        public MainWindow()
        {
            InitializeComponent();

            tickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 1,
                    Category = "Сеть",
                    Description = "Нет доступа в Интернет",
                    Priority = "Высокий",
                    Status = "Новая",
                    CreatedAt = DateTime.Now
                },
                new Ticket
                {
                    Id = 2,
                    Category = "ПО",
                    Description = "Не запускается Microsoft Word",
                    Priority = "Средний",
                    Status = "В работе",
                    CreatedAt = DateTime.Now
                },
                new Ticket
                {
                    Id = 3,
                    Category = "Оборудование",
                    Description = "Не работает принтер",
                    Priority = "Низкий",
                    Status = "Выполнена",
                    CreatedAt = DateTime.Now
                }
            };

            TicketsDataGrid.ItemsSource = tickets;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim().ToLower();

            var result = tickets.Where(ticket =>
                ticket.Description.ToLower().Contains(searchText)
            ).ToList();

            TicketsDataGrid.ItemsSource = result;
        }

        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            StatusComboBox.SelectedIndex = 0;
            TicketsDataGrid.ItemsSource = tickets;
        }

        private void AddTicketButton_Click(object sender, RoutedEventArgs e)
        {
            TicketCreateWindow window = new TicketCreateWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void EditTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsDataGrid.SelectedItem == null)
            {
                MessageBox.Show(
                    "Сначала выберите заявку.",
                    "Редактирование",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            Ticket selectedTicket = TicketsDataGrid.SelectedItem as Ticket;

            if (selectedTicket == null)
            {
                MessageBox.Show(
                    "Не удалось получить данные выбранной заявки.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            TicketEditWindow window = new TicketEditWindow(selectedTicket);
            window.Owner = this;

            bool? result = window.ShowDialog();

            if (result == true)
            {
                TicketsDataGrid.Items.Refresh();
            }
        }

        private void DeleteTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsDataGrid.SelectedItem == null)
            {
                MessageBox.Show(
                    "Выберите заявку для удаления.",
                    "Удаление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить выбранную заявку?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show(
                    "На следующих занятиях здесь будет выполняться удаление записи из MS SQL Server.",
                    "HelpDesk",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Выйти из учётной записи?",
                "HelpDesk",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
