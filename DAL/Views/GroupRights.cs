using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace orion.ims.DAL
{
    public class GroupRights
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int UserGroupId { get; set; }
        public bool AllowAccess { get; set; }
        public string MenuName { get; set; }
        public string GroupName { get; set; }
    }
}
