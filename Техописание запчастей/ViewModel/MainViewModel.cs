using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
        private static List<string> GetInvoiceList(string waybills)
        {
            List<string> invoiceList = new List<string>();
            string list = waybills.Replace(" ", "");
            list = list.Replace("\r", "");

            string[] words = list.Split('\n');

            foreach (var item in words)
            {
                Regex regex = new Regex(@"^S[0-9]{14}");
                if (regex.IsMatch(item))
                {
                    invoiceList.Add(item);
                }

            }

            return invoiceList;
        }
    }
}
