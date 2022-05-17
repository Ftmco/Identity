using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Tools;

public static class MetadataConvertor
{
    public static IHeaderDictionary ConvertToHeaderDictonary(this Metadata metadata)
    {
        IDictionary<string, StringValues> headers = new Dictionary<string, StringValues>();
        foreach (var rHeader in metadata)
            headers[rHeader.Key] = rHeader.Value;
        return (IHeaderDictionary)headers;
    }
}
