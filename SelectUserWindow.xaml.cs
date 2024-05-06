using DapperGenericRepository.Repository;
using StickyNotes.Models;
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
using System.Windows.Shapes;

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for SelectUserWindow.xaml
    /// </summary>
    public partial class SelectUserWindow : Window
    {
        GenericRepository<UserDtoDetails> _repository = new();
        List<UserDtoDetails> Users = [];
        public SelectUserWindow()
        {
            InitializeComponent();
            UpdateUserList();
        }

        private void Window_Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
                e.Handled = true;
            }
        }

        public void UserChanged()
        {
            UpdateUserList();
        }

        private void UpdateUserList()
        {
            Users = (from user in _repository.GetAll() select user).ToList();
            this.UserList.ItemsSource = Users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenUserNote(object sender, MouseButtonEventArgs e)
        {
            Grid Grid = sender as Grid;
            UserDtoDetails user = Grid.DataContext as UserDtoDetails;
            MainWindow mainWindow = new MainWindow(user.User_Id);
            mainWindow.Show();
        }

        private void MouseDown_Delete(object sender, MouseButtonEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            UserDtoDetails user = menuItem.DataContext as UserDtoDetails;
            _repository.Delete(user);
            UserChanged();
        }
    }
}
