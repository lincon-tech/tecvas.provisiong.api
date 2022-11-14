using System;
using System.Collections.Generic;
using System.Text;

namespace Techrunch.TecVas.Entities
{
    public interface ITrackable
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
