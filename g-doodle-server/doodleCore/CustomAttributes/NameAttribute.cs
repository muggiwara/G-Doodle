using System;

namespace doodleCore.CustomAttributes
{
    public class NameAttribute : Attribute
    {
        private string _name;
        public string Name {
            get {
                return _name;
            }
        }

        public NameAttribute(string name){
            _name = name;
        }
    }
}