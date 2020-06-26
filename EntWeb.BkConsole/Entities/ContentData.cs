using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.BkConsole.Entities
{
    public class ContentData
    {
        public string Title { get; set; }
        public string PubDate { get; set; }
        public string Author { set; get; }
        public string Content { get; set; }
    }
}