namespace ShoesEShop.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AllowFileExtensionsAttribute : Attribute
    {
        private string[] _allowedFileExtensions;

        public AllowFileExtensionsAttribute(params string[] extensions)
            => _allowedFileExtensions = extensions;

        public bool IsValidExtension(string extension)
        {
            extension = extension.ToLower();
            return _allowedFileExtensions
                .Select(t => t.ToLower())
                .Any(t => t.Equals(extension));
        }
    }
}
