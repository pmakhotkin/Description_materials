

namespace Техописание_запчастей.ViewModel
{
    class WelcomePageViewModel
    {
        private Page? ValidPage = default;

        public WelcomePageViewModel(bool isValid)
        {
            if (isValid)
            {
                ValidPage = new OkPage();
            }
            else
            {
                ValidPage = new NokPage();

            }
        }
    }

}
