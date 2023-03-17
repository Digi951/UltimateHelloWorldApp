using HelloWorldLibrary.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HelloWorldLibrary.BusinessLogic;

public class Messages : IMessages
{
    private readonly ILogger<Messages> _logger;

    public Messages(ILogger<Messages> logger)
    {
        _logger = logger;
    }

    public String Greeting(String language)
    {
        String output = LookUpCustomText("Greeting", language);
        return output;
    }

    private String LookUpCustomText(String key, String language)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {

            List<CustomText>? messageSets = JsonSerializer
                .Deserialize<List<CustomText>>
                (
                    File.ReadAllText("CustomText.json"), options
                );

            CustomText? messages = messageSets?.Where(x => x.Language == language).First();

            if (messages is null)
            {
                throw new NullReferenceException("The specified langage was not found in the json file.");
            }

            return messages.Translations[key];
        }
        catch (Exception ex)
        {
            _logger.LogError("Error looking up the custom text", ex);
            throw;
        }
    }
}