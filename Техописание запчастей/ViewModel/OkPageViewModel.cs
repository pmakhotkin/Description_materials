namespace Техописание_запчастей.ViewModel;

public class OkPageViewModel:INotifyPropertyChanged
{
    private Cursor _pageOkCursor = Cursors.Arrow;
    public Cursor PageOkCursor 
    { 
      get => _pageOkCursor;
        set => SetField(ref _pageOkCursor, value);
    }
    public OkPageViewModel()
    {
        CreateDocs = new BaseCommand(_=>true,_=> CreateDoc());
    }
    
    #region Command
    public BaseCommand CreateDocs { get; set; }
    #endregion
    public void CreateDoc()
    {

        if (MainViewModel.AllSparePartsFromDb != null)
        {
            PageOkCursor= Cursors.Wait;
            WordDocument.CreateDescription(MainViewModel.AllSparePartsFromDb);
            PageOkCursor = Cursors.Arrow;
            MessageBox.Show("Описания созданы и файл сохранен в папке с файлом запуска");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}