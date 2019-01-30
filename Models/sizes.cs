using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wigsboot.Models
{
    public class sizes
    {
        public Int32 sizeid { get; set; }
        public String sizename { get; set; }
        public String esize { get; set; }
        public Decimal sizeprice { get; set; }
        public Int16 isstock { get; set; }
    }

    public class response
    {
        public int resultcode { get; set; }
        public String infoid { get; set; }
    }

    public class images
    {
        public String image { get; set; }
    }

    public class categories
    {
        public Int32 code { get; set; }
        public String name { get; set; }
        public String catimg { get; set; }
        public String url { get; set; }
        public Int32 issale { get; set; }
        public String icon { get; set; }
        public Boolean isshown { get; set; }

        public IEnumerable<cats> subcats { get; set;}
    }
}