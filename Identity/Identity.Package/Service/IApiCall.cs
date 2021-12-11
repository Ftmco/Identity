﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Package.Service;

public interface IApiCall
{
    Task<ApiResponse?> GetAsync(string url);

    Task<ApiResponse?> PostAsync(string url, object data);

    void SetHeader(IDictionary<string,string> headers,HttpClient client);
}