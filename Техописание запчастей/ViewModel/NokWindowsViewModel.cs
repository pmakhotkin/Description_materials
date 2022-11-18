namespace Техописание_запчастей.ViewModel
{
    class NokWindowsViewModel:INotifyPropertyChanged
    {
        #region Properties
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
        public List<string> NotExistDBParts
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
                NotifyPropertyChanged("NotExistDBParts");
            }
        }

        #endregion
        public NokWindowsViewModel() 
        {
            AllSparePartsFromDB = WelcomePageViewModel.AllSparePartsFromDB;
            NotValidSpareParts = WelcomePageViewModel.NotValidSpareParts;
            NotExistDBParts = WelcomePageViewModel.NotExistDBParts;
            NotExistPhotoFile = WelcomePageViewModel.NotExistPhotoFile;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
