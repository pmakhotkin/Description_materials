using Page = System.Windows.Controls.Page;

namespace Техописание_запчастей.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
        #region Properties
        private string? _parts = String.Empty;
        public string? Parts
        {
            get => _parts;
            set => _parts = value;
        }
        public static List<SparePart>? AllSparePartsFromDb;
        public static ObservableCollection<SparePart>? NotValidSpareParts;
        public static List<string>? NotExistDbParts;
        public static List<SparePart>? NotExistPhotoFile;
        private Page? _currentPage;
        public  sys.Page? CurrentPage
        {
            get => _currentPage;
            set => SetField(ref _currentPage, value);
        }
        public double FrameOpacity { get; set; }

        private double _valueStatusBar;
        public double ValueStatusBar
        {
            get => _valueStatusBar;
            set => SetField(ref _valueStatusBar, value);
        }
        #endregion
        
        public MainViewModel()
        {
            ValidationSpareParts = new BaseCommand(_ => true, _ => ValidationSpares());
        }

        #region Command
        public BaseCommand ValidationSpareParts { get; set; }
        #endregion

        #region Metods

        private void ValidationSpares()
        {
            var sListPart = GetInvoiceList();
            if (sListPart.Count() == 1 && String.IsNullOrWhiteSpace(sListPart[0])) 
            {
                MessageBox.Show("Не введено значение");
                return;
            }
            AllSparePartsFromDb = Validator.GetSelectMaterial_by_name(sListPart);
            
            if (AllSparePartsFromDb == null)
            {
                MessageBox.Show("Проверьте ввод, по вашему запросу данных в базе нет");
                return;
            }

            NotValidSpareParts = Validator.GetNotValidSpareParts(AllSparePartsFromDb);
            NotExistDbParts = Validator.GetNotExistDbSpareParts(sListPart);
            NotExistPhotoFile = Validator.GetSpare_NotExistPhotoFile(AllSparePartsFromDb);


            if (!NotValidSpareParts.Any() && !NotExistDbParts.Any())
            {
                CurrentPage = null;
                CurrentPage = new OkPage();
            }
            else
            {
                CurrentPage = null;
                CurrentPage  = new NokPage();
            }
        }
        private List<string?> GetInvoiceList()
        {
            var partsList = new List<string?>();
            if (_parts == null) return partsList;
            string list = _parts.Replace(" ", "");
            list = list.Replace("\r", "");

            string?[] words = list.Split('\n');
            if (words.Any())
            {
                foreach (var item in words)
                {
                    if (item.Length>1)
                    {
                        partsList.Add(item);
                    }
                }
            }
            else
            {
                partsList.Add(_parts);
            }
            return partsList;
        }

        #endregion
        
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
}
