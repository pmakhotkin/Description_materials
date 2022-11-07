namespace Техописание_запчастей.ViewModel
{
    public class MainViewModel
    {
        private Page Welcome;
        public Page CurrentPage { get; set; }
        public double FrameOpacity { get; set; }

        public MainViewModel()
        {
            Welcome = new WelcomePage();
            FrameOpacity = 1;
            CurrentPage = Welcome;
        }

    }
}
