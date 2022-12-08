namespace Техописание_запчастей.ViewModel
{
    public class MainViewModel
    {
        private sys.Page _welcome;
        public sys.Page CurrentPage { get; set; }
        public double FrameOpacity { get; set; }

        public MainViewModel()
        {
            _welcome = new WelcomePage();
            FrameOpacity = 1;
            CurrentPage = _welcome;
        }

    }
}
