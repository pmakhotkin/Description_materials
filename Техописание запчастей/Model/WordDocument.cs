namespace Техописание_запчастей.Model
{
    public static class WordDocument
    {
        private static readonly object
            TemplateFile = Environment.CurrentDirectory + "\\Template_Description_Spare.docx";

        private static Microsoft.Office.Interop.Word.Application? Word = default;

        public static void CreateDescription(List<SparePart> spareParts)
        {
            try
            {
                if (Word !=default)
                {
                    Word = default;
                    Word = new();
                }
                else
                {
                    Word = new();
                }
                var template = Word.Documents.Open(TemplateFile);
                var docNew = Word.Documents.Add();
                var counter = 1;
                //word.Visible = true;
                foreach (var material in spareParts)
                {
                    counter++;
                    object missing = Type.Missing;
                    object what = WdGoToItem.wdGoToLine;
                    object which = WdGoToDirection.wdGoToLast;

                    template.Range(ref missing, ref missing).Copy();
                    wordApp.Range endRange = docNew.GoTo(ref what, ref which, ref missing, ref missing);
                    endRange.Paste();
                    ReplaceWord("@unity", material.Unity, ref docNew);
                    ReplaceWord("@material", material.Material, ref docNew);
                    ReplaceWord("@rus_description", material.RusDescription, ref docNew);
                    ReplaceWord("@description", material.Description, ref docNew);
                    ReplaceWord("@photo", material.Photo, ref docNew);
                }

                object newFileName =
                    Path.Combine(Environment.CurrentDirectory, "Техописания_" + DateTime.Today.ToString("d")) + ".docx";
                docNew.SaveAs2(newFileName);
                Word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
                docNew.Close(true);
                template.Close(false);
                Word.Quit();
            }
            finally
            {
                Word.Quit();
            }
        }

        private static void ReplaceWord(string findword, string? replaceword, ref Document doc)
        {
            object missing = Type.Missing;
            object wrap = WdFindWrap.wdFindContinue;

            if (findword == "@photo")
            {
                //word.Visible = true;
                wordApp.Range insertPhotoRange = doc.Range();
                insertPhotoRange.Find.Execute(findword, false, false, false, missing, false, true, wrap, false,
                    missing);
                doc.Activate();
                insertPhotoRange.Select();
                var picture =
                    Word.Selection.InlineShapes.AddPicture(replaceword, Type.Missing, Type.Missing, Type.Missing);
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
            }

            ;
            Find find = doc.Range().Find;
            object replace = WdReplace.wdReplaceAll;
            find.Text = findword;
            find.Replacement.Text = replaceword;
            find.Execute(missing, false, false, false, missing, false, true, wrap, false, missing, replace);
        }
    }
}