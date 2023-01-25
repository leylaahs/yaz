using System.Diagnostics;

namespace eBusiness.Utilies.Extension
{
    public static class FileExtension
    {
        public static bool CheckMemory(this IFormFile file, int kb) => kb * 1024 > file.Length;
        public static bool CheckType(this IFormFile file, string type) => file.ContentType.Contains(type);
        public static string ChangeName(string oldname)
        {
            string extension = oldname.Substring(oldname.LastIndexOf("."));
            if (oldname.Length < 32)
            {
                oldname = oldname.Substring(0, oldname.LastIndexOf("."));
            }
            else
            {
                oldname = oldname.Substring(0, 32);
            }
            return Guid.NewGuid + oldname + extension;
        }
        public static string SaveFile(this IFormFile file , string path)
        {
            string filename = ChangeName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }
        public static string CheckValidate(this IFormFile file,int kb,string type)
        {
            string result = "";
            if (!CheckMemory(file, kb))
            {
                result+= $"Faylin uzunlugu {kb}-dan artiq ola bilmez";
            }
            if (!CheckType(file, type))
            {
                result+= $"Faylin uzunlugu {kb}-dan artiq ola bilmez";
            }
            return result;
        }
        public static void DeleteFile(this string fileName,string root,string folder)
        {
            string path = Path.Combine(root,folder,fileName);
            if (!File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
