namespace AzureProject.Shared.Helpers;

public static class FormatHelper
{
    public static string DateIso8601()
    {
        //Calendar dates - https://en.wikipedia.org/wiki/ISO_8601
        return DateTime.Now.ToString("yyyy-MM-dd");
    }
    
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}

//https://stackoverflow.com/questions/17632584/how-to-get-the-unix-timestamp-in-c-sharp
//https://docs.microsoft.com/en-us/dotnet/api/system.datetime.unixepoch?view=net-6.0
//https://stackoverflow.com/questions/249760/how-can-i-convert-a-unix-timestamp-to-datetime-and-vice-versa
//https://stackoverflow.com/questions/11743160/how-do-i-encode-and-decode-a-base64-string
//https://www.toolsqa.com/postman/basic-authentication-in-postman/
//https://www.base64decode.org/
