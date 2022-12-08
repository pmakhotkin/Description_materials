

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
            ValidationSpareParts = new BaseCommand(_ => true, _=>RecheckValidation());
        }

        #region Command
        public BaseCommand ValidationSpareParts { get; set; }
        #endregion

        #region Metods
       private void RecheckValidation()
       {
           var forUpdateValue = new List<SparePart>();
        
               forUpdateValue.AddRange(NotValidSpareParts.Where(item => !string.IsNullOrWhiteSpace(item.Description)
                                                                        && !string.IsNullOrWhiteSpace(item.RusDescription) 
                                                                        && !string.IsNullOrWhiteSpace(item.Photo) 
                                                                        && !string.IsNullOrWhiteSpace(item.Unity)));
               if (forUpdateValue.Any())
               {
                   foreach (var item in forUpdateValue)
                   {
                       NotValidSpareParts.Remove(item);
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
               }

               if (!NotValidSpareParts.Any())
               {
                   var okWindows = new OkWindows();
                   okWindows.Show();
               }
               else
               {
                   MessageBox.Show("Заполните оставшиеся данные в первой таблице");
               }
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
