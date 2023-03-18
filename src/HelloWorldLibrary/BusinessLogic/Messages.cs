using HelloWorldLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;

namespace HelloWorldLibrary.BusinessLogic;

public class Messages : IMessages
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<Messages> _logger;
     

    public Messages(IConfiguration configuration, ILogger<Messages> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public String Greeting(String language)
    {
        String output = LookUpCustomText("Greeting", language);
        return output;
    }

    private String LookUpCustomText(String key, String language)
    {
        try
        {
            var messageSets = _configuration.GetSection("MessageSets").Get<IEnumerable<CustomText>>();
            

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