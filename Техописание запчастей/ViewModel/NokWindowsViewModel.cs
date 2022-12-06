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
