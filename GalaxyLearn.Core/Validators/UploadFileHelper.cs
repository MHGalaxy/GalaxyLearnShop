namespace GalaxyLearn.Core.Validators
{
    public static class UploadFileHelper
    {
        public static Dictionary<string, string> AllowedFileTypes()
        {
            var dictionary = new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };

            return dictionary;
        }

        public static Dictionary<string, string> AllowedImageTypes()
        {
            var dictionary = new Dictionary<string, string>
            {
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"}
            };

            return dictionary;
        }

        public static Dictionary<string, string> AllowedVideoTypes()
        {
            var dictionary = new Dictionary<string, string>
        {
            {".mp4", "video/mp4"},
            {".avi", "video/x-msvideo"},
            {".mkv", "video/x-matroska"},
        };

            return dictionary;
        }
    }
}
