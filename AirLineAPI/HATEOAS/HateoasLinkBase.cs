using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.HATEOAS
{
    public abstract class HateoasLinkBase
    {
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
