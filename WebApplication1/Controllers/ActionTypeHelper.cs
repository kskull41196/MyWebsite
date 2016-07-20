using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    public class ActionTypeHelper
    {
        public enum ActionType
        {
            TYPE_VIEW, TYPE_CREATE, TYPE_EDIT, TYPE_DEL
        }
        public static ActionType getTypeFromString(String act)
        {
            if (act==null || act.Equals("") || act.Equals("view"))
                return ActionType.TYPE_VIEW;
            else if (act.Equals("create"))
                return ActionType.TYPE_CREATE;
            else if (act.Equals("edit"))
                return ActionType.TYPE_EDIT;
            else return ActionType.TYPE_DEL;
        }
        public static String getStringFromType(ActionType type)
        {
            if (type == ActionType.TYPE_VIEW)
                return "view";
            else if (type == ActionType.TYPE_CREATE)
                return "create";
            else if (type == ActionType.TYPE_EDIT)
                return "edit";
            else return "del";
        }
    }
}