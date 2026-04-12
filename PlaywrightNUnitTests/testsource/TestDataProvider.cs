using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
// using Models;

namespace PlaywrightNUnitTests.testsource;

public class TestDataProvider
{
    public static List<LoginData> GetLoginData()
    {
        var jsonData = File.ReadAllText("C:\\Users\\Laptop\\Documents\\Automation\\Playwright_Csharp_v1\\Playwright_CSharp\\PlaywrightNUnitTests\\data\\formdata.json");
        var data = JsonConvert.DeserializeObject<List<LoginData>>(jsonData);
        return data;
    }
}

