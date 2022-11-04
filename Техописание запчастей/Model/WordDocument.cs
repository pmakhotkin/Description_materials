using Microsoft.Office.Interop.Word;
using System.Data;
using System.IO;
using System.Threading;
using DataTable = System.Data.DataTable;

namespace Техописание_запчастей.Model
{
    public static class WordDocument
    {
        private static object templateFile = Environment.CurrentDirectory + "\\Template_Description_Spare.docx";
        private static Application word = new();
        public static void CreateDescription(List<SparePart> spare_Parts)
        {
            try
            {
                var doctemplate = word.Documents.Open(templateFile);
                var docNew = word.Documents.Add();
                var counter = 1;
                //word.Visible = true;
                foreach (var material in spare_Parts)
                {
                    Console.Clear();
                    Console.WriteLine($" Обработка {counter} из {spare_Parts.Count}");
                    counter++;
                    object missing = Type.Missing;
                    object what = WdGoToItem.wdGoToLine;
                    object which = WdGoToDirection.wdGoToLast;

                    doctemplate.Range(ref missing, ref missing).Copy();
                    wordApp.Range endRange = docNew.GoTo(ref what, ref which, ref missing, ref missing);
                    endRange.Paste();
                    ReplaceWord("@unity", material.Unity, ref docNew);
                    ReplaceWord("@material", material.Material, ref docNew);
                    ReplaceWord("@rus_description", material.RusDescription, ref docNew);
                    ReplaceWord("@description", material.Description, ref docNew);
                    ReplaceWord("@photo", material.Photo, ref docNew);

                }
                object newFileName = Path.Combine(Environment.CurrentDirectory, "Техописания_" + DateTime.Today.ToString("d")) + ".docx";
                docNew.SaveAs2(newFileName);
                word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
                docNew.Close(true);
                doctemplate.Close(false);
                word.Quit();
            }
            finally
            {
                if (word != null)
                {
                    word.Quit();
                }
            }
        }

        private static void ReplaceWord(string findword, string replaceword, ref Document doc)
        {
            object missing = Type.Missing;
            object wrap = WdFindWrap.wdFindContinue;

            if (findword == "@photo")
            {
                //word.Visible = true;
                wordApp.Range insertPhotoRange = doc.Range();
                insertPhotoRange.Find.Execute(findword, false, false, false, missing, false, true, wrap, false, missing);
                doc.Activate();
                insertPhotoRange.Select();
                var picture = word.Selection.InlineShapes.AddPicture(replaceword, Type.Missing, Type.Missing, Type.Missing);
                //picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;              
                picture.Height = 320;
                picture.Width = 380;
                Thread.Sleep(500);
                Shape shape = picture.ConvertToShape();
                if (shape.Rotation != 0)
                {
                    shape.Rotation = 0;

                }
                return;
            };
            Find find = doc.Range().Find;
            object replace = WdReplace.wdReplaceAll;
            find.Text = findword;
            find.Replacement.Text = replaceword;
            find.Execute(missing, false, false, false, missing, false, true, wrap, false, missing, replace);
        }
    }
}