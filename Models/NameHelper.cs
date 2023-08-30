namespace BlueHaisAnisHotelManagement.Models
{
    public static class NameHelper
    {
        public static string GetTypeName(string fullTypeName)
        {
            string retString = "";
            try
            {
                int lastIndex = fullTypeName.LastIndexOf('.') + 1;
                retString = fullTypeName.Substring(lastIndex, fullTypeName.Length - lastIndex);
            }
            catch
            {
                retString = fullTypeName;
            }
            return retString;
        }
    }
}
