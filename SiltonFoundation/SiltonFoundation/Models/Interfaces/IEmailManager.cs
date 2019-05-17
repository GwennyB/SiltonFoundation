using SiltonFoundation.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models.Interfaces
{
    public interface IEmailManager
    {
        Task<bool> Send(Email message);
    }
}
