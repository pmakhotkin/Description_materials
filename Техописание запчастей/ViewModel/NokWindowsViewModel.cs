using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Техописание_запчастей.ViewModel
{
    internal class NokWindowsViewModel:INotifyPropertyChanged
    {
        #region Properties
        private List<SparePart>? _allSparePartsFromDb;
        private List<SparePart>? AllSparePartsFromDb
        {
            get => _allSparePartsFromDb;
            set => SetField(ref _allSparePartsFromDb, value);

        }
        public ObservableCollection<SparePart>? NotValidSpareParts { get; set; }
        private SparePart _selectedMaterial;
        public SparePart SelectedMaterial
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
        public NokWindowsViewModel() 
        {
            AllSparePartsFromDb = WelcomePageViewModel.AllSparePartsFromDb;
            NotValidSpareParts = WelcomePageViewModel.NotValidSpareParts;
            NotExistDbParts = WelcomePageViewModel.NotExistDbParts;
            NotExistPhotoFile = WelcomePageViewModel.NotExistPhotoFile;
        }

        #region Command
        private RelayCommand? _validationSpareParts;
        public RelayCommand ValidationSpareParts
        {
            get
            {
                return _validationSpareParts ?? new RelayCommand(obj => { RecheckValidation(); });
            }
        }
        #endregion

        #region Metods
       public void RecheckValidation()
       {
           if (NotValidSpareParts != null && NotValidSpareParts.Any())
           {
               var pairs = from totalList in AllSparePartsFromDb
                   join notValid in NotValidSpareParts.AsEnumerable()
                       on totalList.Material equals notValid.Material
                   select new { notValid, totalList };
               foreach (var pair in pairs)
               {
                   pair.totalList.Description = pair.notValid.Description;
                   pair.totalList.RusDescription = pair.notValid.RusDescription;
                   pair.totalList.Photo = pair.notValid.Photo;
                   pair.totalList.Unity = pair.notValid.Unity;
               }
           }
            
           NotValidSpareParts = Validator.GetNotValidSpareParts(AllSparePartsFromDb);
           if (AllSparePartsFromDb != null) WordDocument.CreateDescription(AllSparePartsFromDb);
       }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

}
