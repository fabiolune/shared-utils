using System.IO;
using Microsoft.AspNetCore.Http;

namespace Fl.Shared.Utils.Components.Unit.Tests.Utils;

public static class TestUtils
{
    public static HttpContext SetupHttpContext(
        PathString path,
        string method = "GET") => 
        new DefaultHttpContext
        {
            Request =
            {
                Path = path,
                Method = method
            },
            Response =
            {
                Body = new MemoryStream()
            }
        };
}