using DapperGenericRepository.Repository;
using StickyNotes.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GenericRepository<NoteDetailDto> _NoteRepository = new();
        List<NoteDetailDto> notes = [];
        private int Owner_ID;
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

        public MainWindow(int Owner_ID)
        {
            this.Owner_ID = Owner_ID;
            InitializeComponent();
            updateTextList();
        }

        private void updateTextList()
        {
            notes = (from note in _NoteRepository.GetAll()
                     where note.Owner_Id == Owner_ID
                     orderby note.Last_Modified descending
                     select note).ToList();
            this.data.ItemsSource = notes;
        }

        public void ContentChanged()
        {
            updateTextList();
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var note = await AddNote();

            new EditorWindow(this, note.Note_Id).Show();
        }

        private NoteDetailDto getLastNote()
        {
            return notes.Last();
        }

        public async Task<NoteDetailDto> AddNote()
        {
            NoteDetailDto note = new();
            note.Owner_Id = Owner_ID;
            note.Last_Modified = null;
            note.Created_Date = DateTime.Now;
            note.Category = "";
            note.Content = "";

            // Set the content of the newly created note in the editor
            notes.Add(note);
            await _NoteRepository.AddAsync(note);
            ContentChanged();

            return getLastNote();
        }


        private void buttonQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            new EditorWindow(this, note.Note_Id).Show();
        }

        private void MouseEnter(object sender, MouseEventArgs e)
        {
            MenuItem menuitem = e.Source as MenuItem;
            menuitem.Visibility = Visibility.Hidden;
        }
    }
}