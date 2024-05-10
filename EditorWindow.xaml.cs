using DapperGenericRepository.Repository;
using StickyNotes.Models;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace StickyNotes
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        private IGenericRepository<NoteDto> _repository = new GenericRepository<NoteDto>();
        private NoteDto? _note;
        private MainWindow parent;
        RichTextBox box;

        public EditorWindow(MainWindow mainWindow, long NoteId)
        {
            InitializeComponent();
            this.parent = mainWindow;
            this._note = _repository.GetById(NoteId);
            box = this.Content;

            if(_note != null)
            {
                setNoteContent(_note.Content);
            }
        }

        private void setNoteContent(string content)
        {
            if(string.IsNullOrWhiteSpace(content))
            {
                // Handle empty content
                box.Document = new FlowDocument();
                return;
            }


            using(MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                box.Document = XamlReader.Load(stream) as FlowDocument;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateNote(object sender, TextChangedEventArgs e)
        {
            var statusLabel = this.LabelSaveStatus;

            if(this._note == null)
            {
                statusLabel.Content = "";
                return;
            }

            statusLabel.Content = "...";

            _note.Content = GetNoteContent();
            _note.Last_Modified = DateTime.Now;

            statusLabel.Content = _repository.Update(_note) ? "✔" : "⚠️";
            parent.ContentChanged();
        }

        private async void UpdateNoteAsync(object sender, TextChangedEventArgs e)
        {
            var statusLabel = this.LabelSaveStatus;

            if(this._note == null)
            {
                statusLabel.Content = "";
                return;
            }

            statusLabel.Content = "...";

            _note.Content = GetNoteContent();
            _note.Last_Modified = DateTime.Now;

            statusLabel.Content = await _repository.UpdateAsync(_note) ? "✔" : "⚠️";
            parent.ContentChanged();
        }
        string StringFromRichTextBox(RichTextBox rtb)
        {
            string content;
            using(MemoryStream stream = new MemoryStream())
            {
                XamlWriter.Save(box.Document, stream);
                content = Encoding.UTF8.GetString(stream.ToArray());
            }
            return content;
        }

        private string GetNoteContent()
        {
            string content;
            using(MemoryStream stream = new MemoryStream())
            {
                XamlWriter.Save(box.Document, stream);
                content = Encoding.UTF8.GetString(stream.ToArray());
            }
            return content;
        }


        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            NoteDetailDto note = await parent.AddNote();
            new EditorWindow(parent, note.Note_Id).Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(box.Selection.GetPropertyValue(FontWeightProperty).Equals(FontWeights.Bold))
            {
                box.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                box.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            if(box.Selection.GetPropertyValue(FontStyleProperty).Equals(FontStyles.Italic))
            {
                box.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                box.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if(box.Selection.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline))
            {
                box.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, new TextDecorationCollection());
            }
            else
            {
                box.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if(box.Selection.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Strikethrough))
            {
                box.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, new TextDecorationCollection());
            }
            else
            {
                box.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
            }
        }
    }

}


