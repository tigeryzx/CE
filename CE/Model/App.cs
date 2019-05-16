using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CE.Model
{
    public class App
    {
        public string name { get; set; }

        private bool _multi;
        public bool multi {
            get
            {
                if (_sub != null && _sub.Count > 0)
                    return true;
                else
                    return false;
            }
            set
            {
                _multi = value;
            }
        }
        public int runTime { get; set; }
        public string path { get; set; }

        private List<App> _sub = null;
        public List<App> sub 
        {
            get
            {
                return _sub = _sub ?? new List<App>();
            }
        }

        public override bool Equals(object obj)
        {
            string OtherName = obj as string;
            if (!string.IsNullOrEmpty(OtherName))
                return this.name.ToLower() == OtherName.ToLower();
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("程序名：{0} ; 路径 {1}",this.name,this.path);
        }
    }
}
