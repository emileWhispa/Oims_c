using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace orion.ims.DAL
{
    public class vw_ProfileMenus
    {
        public int Id { get; set; }       
        
        public string MenuName { get; set; }
        
        public string Url { get; set; }
        
        public int MenuLevel { get; set; }
        
        public int ParentMenuId { get; set; }

        public int UserGroupId { get; set; }
        
        public bool Active { get; set; }

        public bool AllowAccess { get; set; }
    }
}
