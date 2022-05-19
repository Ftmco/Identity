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
        IHeaderDictionary dict = new HeaderDictionary();
        foreach (var rHeader in metadata)
            dict[rHeader.Key] = rHeader.Value;
        return dict;
    }
}
