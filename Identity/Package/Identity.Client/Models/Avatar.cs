using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Client.Models;

public record Avatar(Guid FileId,string FileToken,string? Base64);