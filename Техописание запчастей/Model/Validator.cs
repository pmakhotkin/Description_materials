

namespace Техописание_запчастей.Model
{
    public static class Validator
    {
        public static List<SparePart> GetNotValidSpareParts(List<SparePart> parts)
        {
            List<SparePart> selectMaterial_by_null = new List<SparePart>();
                selectMaterial_by_null = parts.Where(x => x.Description == null || x.RusDescription == null || x.Unity == null || x.Photo == null).ToList();
            return selectMaterial_by_null;
        }

        public static List<string> GetNotExistDBSpareParts(List<string> parts)
        {
            using (DeliveryContext db = new DeliveryContext())
            {
                parts = parts.Where(name => !db.SpareParts.Any(x => x.Material == name)).ToList();
            }
            return parts;
        }

        public static List<SparePart> GetSelectMaterial_by_name(List<string> parts)
        {
            List<SparePart> selectMaterial_by_name = new List<SparePart>();
            using (DeliveryContext db = new DeliveryContext())
            {
                selectMaterial_by_name = db.SpareParts.Where(x => parts.Contains(x.Material)).ToList();
            }
            return selectMaterial_by_name;
        }

        public static List<SparePart> GetSpare_NotExistPhotoFile(List<SparePart> ListLinkPhoto)
        {
            List<SparePart> notExistPhoto = new List<SparePart>();

            foreach (var Link in ListLinkPhoto)
            {
                if (!File.Exists(Link.Photo) && !String.IsNullOrEmpty(Link.Photo))
                {
                    notExistPhoto.Add(Link);
                }
            }

            return notExistPhoto;
        }


    }
}
