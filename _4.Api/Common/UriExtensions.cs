using System.Text;

namespace Api.Common;

public static class UriExtensions
{
    public static string GetUriWithQueryString(
        this string requestUri,
        Dictionary<string, object?> queryStringParams)
    {
        bool startingQuestionMarkAdded = false;
        var sb = new StringBuilder();
        sb.Append(requestUri);
        foreach (var parameter in queryStringParams)
        {
            if (parameter.Value == null)
            {
                continue;
            }
            sb.Append(startingQuestionMarkAdded ? '&' : '?');
            sb.Append(parameter.Key);
            sb.Append('=');
            sb.Append(parameter.Value);
            startingQuestionMarkAdded = true;
        }
        return sb.ToString();
    }
}
