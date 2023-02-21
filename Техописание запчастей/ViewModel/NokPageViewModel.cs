

namespace Техописание_запчастей.ViewModel;

internal class NokPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    #region Properties

    private List<SparePart>? _allSparePartsFromDb;

    private List<SparePart>? AllSparePartsFromDb
    {
        get => _allSparePartsFromDb;
        set => SetField(ref _allSparePartsFromDb, value);

    }

    public ObservableCollection<SparePart>? NotValidSpareParts { get; set; }
    private SparePart? _selectedMaterial;
    public SparePart? SelectedMaterial
    {
        get => _selectedMaterial;
        set => SetField(ref _selectedMaterial, value);
    }
    private List<string>? _notExistDbParts;
    public List<string>? NotExistDbParts
    {
        get => _notExistDbParts;
        set => SetField(ref _notExistDbParts, value);
    }
    private List<SparePart>? _notExistPhotoFile;
    public List<SparePart>? NotExistPhotoFile
    {
        get => _notExistPhotoFile;
        set => SetField(ref _notExistPhotoFile, value);
    }
    #endregion

    public NokPageViewModel()
    {
        AllSparePartsFromDb = MainViewModel.AllSparePartsFromDb;
        NotValidSpareParts = MainViewModel.NotValidSpareParts;
        NotExistDbParts = MainViewModel.NotExistDbParts;
        NotExistPhotoFile = MainViewModel.NotExistPhotoFile;
        ValidationSpareParts = new BaseCommand(_ => true, _ => RecheckValidation());
    }

    #region Command

    public BaseCommand ValidationSpareParts { get; set; }

    #endregion

    private void RecheckValidation()
    {
        var forUpdateValue = new List<SparePart>();

        if (NotValidSpareParts != null)
        {
            forUpdateValue.AddRange(NotValidSpareParts.Where(item => !string.IsNullOrWhiteSpace(item.Description)
                                                                     && !string.IsNullOrWhiteSpace(
                                                                         item.RusDescription)
                                                                     && !string.IsNullOrWhiteSpace(item.Photo) &&
                                                                     !string.IsNullOrWhiteSpace(item.Unity)));

            if (forUpdateValue.Any())
            {
                foreach (var item in forUpdateValue)
                {
                    NotValidSpareParts.Remove(item);
                }
            }

            var pairs = from totalList in AllSparePartsFromDb
                join itemUpdate in forUpdateValue.AsEnumerable()
                    on totalList.Material equals itemUpdate.Material
                select new { itemUpdate, totalList };

            foreach (var pair in pairs)
            {
                pair.totalList.Description = pair.itemUpdate.Description;
                pair.totalList.RusDescription = pair.itemUpdate.RusDescription;
                pair.totalList.Photo = pair.itemUpdate.Photo;
                pair.totalList.Unity = pair.itemUpdate.Unity;
            }

            if (!NotValidSpareParts.Any())
            {
                MessageBox.Show(("Данные прошли проверку, будут созданы техописания"));
                OkPageViewModel okPageViewModel = new OkPageViewModel();
                okPageViewModel.CreateDoc();
            }
            else
            {
                MessageBox.Show("Заполните оставшиеся данные в первой таблице");
            }
        }
    }
        
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