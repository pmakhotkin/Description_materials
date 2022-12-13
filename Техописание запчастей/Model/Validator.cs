

using System.Collections.ObjectModel;

namespace Техописание_запчастей.Model
{
    public static class Validator
    {
        public static ObservableCollection<SparePart> GetNotValidSpareParts(List<SparePart>? parts)
        {
            var selectMaterialByNull = new ObservableCollection<SparePart>();
            if (parts == null)
            {
                return selectMaterialByNull;
            }
            var selectMaterial = parts.Where(x => x.Description == null || x.RusDescription == null || x.Unity == null || x.Photo == null).ToList();
            selectMaterialByNull = new ObservableCollection<SparePart>(selectMaterial);
            return selectMaterialByNull;
        }

        public static List<string?> GetNotExistDbSpareParts(List<string?> parts)
        {
            using (DeliveryContext db = new DeliveryContext())
            {
                parts = parts.Where(name => !db.SpareParts.Any(x => x.Material == name)).ToList();
            }
            return parts;
        }

        public static List<SparePart>? GetSelectMaterial_by_name(List<string?> parts)
        {
            List<SparePart> selectMaterialByName;
            using (DeliveryContext db = new DeliveryContext())
            {
                selectMaterialByName = db.SpareParts.Where(x => parts.Contains(x.Material)).ToList();
            }
            return selectMaterialByName;
        }

        public static List<SparePart>? GetSpare_NotExistPhotoFile(List<SparePart> listLinkPhoto)
        {
            List<SparePart> notExistPhoto = new List<SparePart>();

            foreach (var link in listLinkPhoto)
            {
                if (!File.Exists(link.Photo) && !String.IsNullOrEmpty(link.Photo))
                {
                    notExistPhoto.Add(link);
                }
            }

            return notExistPhoto;
        }


    }
}
