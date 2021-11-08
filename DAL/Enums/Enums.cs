using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace orion.ims.DAL
{
    public enum AuthStatus { Created = 0, Authorized = 1, Rejected = 2 }   

    public enum UserLoginStatus { Ok = 0, ChangePassword = 1, PassExpired = 2, UserLocked = 3 }

    public enum OrderStatus { Pending = 0,Submitted = 1, Dispatched = 2, Received =3,}
}
