using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldLibrary.Models;

public class CustomText
{
    public String Language { get; set; }
    public Dictionary<String, String> Translations { get; set; }
}
