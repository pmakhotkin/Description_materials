namespace Техописание_запчастей.ViewModel
{
    class WelcomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        #region Properties
        private string parts = String.Empty;
        public string? Parts
        {
            get { return parts; }
            set 
            {
                if (value != null)
                {
                    parts = value;
                    NotifyPropertyChanged("Parts");
                }
            }
        }
        private List<SparePart>? allSparePartsFromDB;
        public List<SparePart> AllSparePartsFromDB
        {
            get { return allSparePartsFromDB; }
            set
            {
                allSparePartsFromDB = value;
                NotifyPropertyChanged("AllSparePartsFromDB");
            }
        }

        private List<SparePart>? notValidSpareParts;
        public List<SparePart> NotValidSpareParts
        {
            get { return notValidSpareParts; }
            set
            {
                notValidSpareParts = value;
                NotifyPropertyChanged("NotValidSpareParts");
            }
        }

        private List<string>? notExistDBParts;
        public List<string>? NotExistDBParts
        {
            get { return notExistDBParts; }
            set
            {
                notExistDBParts = value;
                NotifyPropertyChanged("NotExistDBParts");
            }
        }

        private List<SparePart>? notExistPhotoFile;
        public List<SparePart> NotExistPhotoFile
        {
            get { return notExistPhotoFile; }
            set
            {
                notExistPhotoFile = value;
                NotifyPropertyChanged("NotExistPhotoFile");
            }
        }


        #endregion

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
            AllSparePartsFromDB = Validator.GetSelectMaterial_by_name(sListPart);
            NotValidSpareParts = Validator.GetNotValidSpareParts(AllSparePartsFromDB);
            NotExistDBParts = Validator.GetNotExistDBSpareParts(sListPart);
            NotExistPhotoFile = Validator.GetSpare_NotExistPhotoFile(AllSparePartsFromDB);

            if (!AllSparePartsFromDB.Any())
            {
                MessageBox.Show("Проверьте ввод, по вашему запросу данных в базе нет");
                return;
            }

            if (!NotValidSpareParts.Any() && !NotExistDBParts.Any())
            {
                OkWindows okWindows = new OkWindows();
                okWindows.Show();
            }
            else
            {
                ValidationSpares();
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
                   partsList.Add(item);
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
