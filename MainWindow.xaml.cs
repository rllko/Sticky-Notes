using DapperGenericRepository.Repository;
using StickyNotes.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly GenericRepository<NoteDetailDto> _NoteRepository = new();
        List<NoteDetailDto> notes = [];
        public MainWindow()
        {
            InitializeComponent();
            NoteDetailDto note = new()
            {
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book"
            };
            notes.Add(note);
            
            this.data.ItemsSource = notes;
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        public void addNote()
        {

        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditorWindow objEditorWindow = new();
            objEditorWindow.Show();
        }

        private void buttonQuit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Dragbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
                e.Handled = true;
            }
        }

        private void Window_Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
                e.Handled = true;
            }
        }

        private void DataContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Move_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Open_Note(object sender, MouseButtonEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            NoteDetailDto note = panel.DataContext as NoteDetailDto;
            new EditorWindow(note.Content, 0).Show();


        }
    }
}