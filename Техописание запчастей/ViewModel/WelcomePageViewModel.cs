namespace Техописание_запчастей.ViewModel
{
    class WelcomePageViewModel:INotifyPropertyChanged
    {
        #region Properties
        private string parts = String.Empty;
        public string? Parts
        {
            get { return parts; }
            set 
            {
                   parts = value;
            }
        }
        public static List<SparePart>? AllSparePartsFromDB;
        public static List<SparePart>? NotValidSpareParts;
        public static List<string>? NotExistDBParts;
        public static List<SparePart>? NotExistPhotoFile;

        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;

        #region Command
        private RelayCommand? validationSpareParts;
        public RelayCommand ValidationSpareParts
        {
            get 
            { 
                return validationSpareParts?? new RelayCommand(obj => { ValidationSpares();}); 
            }
        }
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
            AllSparePartsFromDB = Validator.GetSelectMaterial_by_name(sListPart);
            
            if (AllSparePartsFromDB == null)
            {
                MessageBox.Show("Проверьте ввод, по вашему запросу данных в базе нет");
                return;
            }

            NotValidSpareParts = Validator.GetNotValidSpareParts(AllSparePartsFromDB);
            NotExistDBParts = Validator.GetNotExistDBSpareParts(sListPart);
            NotExistPhotoFile = Validator.GetSpare_NotExistPhotoFile(AllSparePartsFromDB);


            if (!NotValidSpareParts.Any() && !NotExistDBParts.Any())
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
        private List<string> GetInvoiceList()
        {
            List<string> partsList = new List<string>();
            string list = parts.Replace(" ", "");
            list = list.Replace("\r", "");

            string[] words = list.Split('\n');
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
                partsList.Add(parts);

            }

            return partsList;
        }
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }

}
