namespace Техописание_запчастей.Model
{
    public partial class SparePart
    {
        public string Material { get; set; } = null!;
        public string? Description { get; set; }
        public string? Tnved { get; set; }
        public string? TnvedDescription { get; set; }
        public string? RusDescription { get; set; }
        public string? Division { get; set; }
        public string? Height { get; set; }
        public string? Width { get; set; }
        public string? Length { get; set; }
        public string? GrossWgt { get; set; }
        public string? GrossVolume { get; set; }
        public string? NetHeight { get; set; }
        public string? NetWidth { get; set; }
        public string? NetLength { get; set; }
        public string? NetWgt { get; set; }
        public bool IsStock { get; set; }
        public string? Photo { get; set; }
        public string? Photo1 { get; set; }
        public string? Unity { get; set; }
        public string? Comment { get; set; }
        public bool CheckComment { get; set; }
        public DateTime? DateCheckCom { get; set; }
        public DateTime? SyncDate { get; set; }
        public string? UserName { get; set; }
    }
}
