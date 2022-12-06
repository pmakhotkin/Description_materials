using System.Runtime.CompilerServices;

namespace Техописание_запчастей.Model
{
    public partial class SparePart: INotifyPropertyChanged
    {
        #region private fields
        
        private string? _material = null!;
        private string? _description;
        private string? _tnved;
        private string? _tnvedDescription;
        private string? _rusDescription;
        private string? _division;
        private string? _height;
        private string? _width;
        private string? _length;
        private string? _grossWgt;
        private string? _grossVolume;
        private string? _netHeight;
        private string? _netWidth;
        private string? _netLength;
        private string? _netWgt;
        private bool _isStock;
        private string? _photo;
        private string? _photo1;
        private string? _unity;
        private string? _comment;
        private bool _checkComment;
        private DateTime? _dateCheckCom;
        private DateTime? _syncDate;
        private string? _userName;
        #endregion
        public string? Material
        {
            get => _material;
            set => SetField(ref _material, value);
        }
        public string? Description
        {
            get => _description;
            set => SetField(ref _description, value);
        }
        public string? Tnved
        {
            get => _tnved;
            set => SetField(ref _tnved, value);
        }
        public string? TnvedDescription
        {
            get => _tnvedDescription;
            set => SetField(ref _tnvedDescription, value);
        }
        public string? RusDescription
        {
            get => _rusDescription;
            set => SetField(ref _rusDescription, value);
        }
        public string? Division
        {
            get => _division;
            set => SetField(ref _division, value);
        }
        public string? Height
        {
            get => _height;
            set => SetField(ref _height, value);
        }
        public string? Width
        {
            get => _width;
            set => SetField(ref _width, value);
        }
        public string? Length
        {
            get => _length;
            set => SetField(ref _length, value);
        }
        public string? GrossWgt
        {
            get => _grossWgt;
            set => SetField(ref _grossWgt, value);
        }
        public string? GrossVolume
        {
            get => _grossVolume;
            set => SetField(ref _grossVolume, value);
        }
        public string? NetHeight
        {
            get => _netHeight;
            set => SetField(ref _netHeight, value);
        }
        public string? NetWidth
        {
            get => _netWidth;
            set => SetField(ref _netWidth, value);
        }
        public string? NetLength
        {
            get => _netLength;
            set => SetField(ref _netLength, value);
        }
        public string? NetWgt
        {
            get => _netWgt;
            set => SetField(ref _netWgt, value);
        }
        public bool IsStock
        {
            get => _isStock;
            set => SetField(ref _isStock, value);
        }
        public string? Photo
        {
            get => _photo;
            set => SetField(ref _photo, value);
        }
        public string? Photo1
        {
            get => _photo1;
            set => SetField(ref _photo1, value);
        }
        public string? Unity
        {
            get => _unity;
            set => SetField(ref _unity, value);
        }
        public string? Comment
        {
            get => _comment;
            set => SetField(ref _comment, value);
        }
        public bool CheckComment
        {
            get => _checkComment;
            set => SetField(ref _checkComment, value);
        }
        public DateTime? DateCheckCom
        {
            get => _dateCheckCom;
            set => SetField(ref _dateCheckCom, value);
        }
        public DateTime? SyncDate
        {
            get => _syncDate;
            set => SetField(ref _syncDate, value);
        }
        public string? UserName
        {
            get => _userName;
            set => SetField(ref _userName, value);
        }

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
