namespace Техописание_запчастей.ViewModel;

public class OkPageViewModel
{
    public OkPageViewModel()
    {
        CreateDocs = new BaseCommand(_=>true,_=> CreateDoc());
    }
    
    #region Command
    public BaseCommand CreateDocs { get; set; }
    #endregion
    public static void CreateDoc()
    {
        if (MainViewModel.AllSparePartsFromDb != null) WordDocument.CreateDescription(MainViewModel.AllSparePartsFromDb);
    }
}