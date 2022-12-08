namespace Техописание_запчастей.ViewModel;

public class OkWindowsViewModel
{
    public OkWindowsViewModel()
    {
        CreateDocs = new BaseCommand(_=>true,_=> CreateDoc());
    }

    #region Command
    public BaseCommand CreateDocs { get; set; }
    #endregion
    private static void CreateDoc()
    {
        if (WelcomePageViewModel.AllSparePartsFromDb != null) WordDocument.CreateDescription(WelcomePageViewModel.AllSparePartsFromDb);
    }
}