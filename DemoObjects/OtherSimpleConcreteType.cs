using FunWithGenerics.InterfacesAndDataContainers.DataContainers;
using FunWithGenerics.InterfacesAndDataContainers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithGenerics.DemoObjects
{
    public class OtherSimpleConcreteType : IMyInterface
    {
        readonly string _parm;

        public OtherSimpleConcreteType() : this("no parm") { }

        public OtherSimpleConcreteType(string parm)
        {
            _parm = parm;
        }
        int _roadLength = 67;

        public void UpdateRoadLength(int newLength)
        {
            _roadLength = newLength;
        }

        public SimpleDataContainer GetSimpleClassForId(int id)
        {
            return new SimpleDataContainer { Id = id, Description = "From Other Simple Concrete Type with parm of " + _parm };
        }

        public int GetRoadLength()
        {
            return _roadLength;
        }
    }
}
