using System.Collections.ObjectModel;

namespace Техописание_запчастей.ViewModel
{
    class WelcomePageViewModel:INotifyPropertyChanged
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

        #endregion

        public WelcomePageViewModel()
        {
            ValidationSpareParts = new BaseCommand(_ => true, _ => ValidationSpares());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
                OkWindows okWindows = new OkWindows();
                okWindows.Show();
            }
            else
            {
                NokWindows nokWindows  = new NokWindows();
                nokWindows.Show();
            }
        }
        private List<string?> GetInvoiceList()
        {
            List<string?> partsList = new List<string?>();
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

    }

}
