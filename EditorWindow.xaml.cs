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
using static System.Net.Mime.MediaTypeNames;

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        private IGenericRepository<NoteDto>? _repository = new GenericRepository<NoteDto>();
        private NoteDto? _note;
        private MainWindow parent;
        
        public EditorWindow()
        {
            InitializeComponent();
        }

        public EditorWindow(MainWindow mainWindow,long NoteId)
        {
            InitializeComponent();
            this.parent = mainWindow;
            this._note = _repository.GetById(NoteId);

            setNoteContent(_note.Content);
        }

        private void setNoteContent(string content)
        {
            RichTextBox box = this.Content;
            box.Document.Blocks.Clear();
            box.Document.Blocks.Add(new Paragraph(new Run(content)));
        }

        private void Window_Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateNote(object sender, TextChangedEventArgs e)
        {
            var statusLabel = this.LabelSaveStatus;

            if(this._note == null) {
                statusLabel.Content = "";
                return;            
            }

            statusLabel.Content = "...";

            _note.Content = StringFromRichTextBox(this.Content);
            _note.Last_Modified = DateTime.Now;

            statusLabel.Content = _repository.Update(_note) ? "✔" : "⚠️";
            parent.ContentChanged();
        }
        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                        rtb.Document.ContentStart,
                        rtb.Document.ContentEnd);
            return textRange.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            NoteDetailDto note = parent.AddNote();
            new EditorWindow(parent, note.Note_Id).Show();
        }
    }

}


