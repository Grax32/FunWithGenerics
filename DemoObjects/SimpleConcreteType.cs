using FunWithGenerics.InterfacesAndDataContainers.DataContainers;
using FunWithGenerics.InterfacesAndDataContainers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithGenerics.DemoObjects
{
    public class MyConcreteType : IMyInterface
    {
        int _roadLength = 5;

        public void UpdateRoadLength(int newLength)
        {
            _roadLength = newLength;
        }

        public SimpleDataContainer GetSimpleClassForId(int id)
        {
            return new SimpleDataContainer { Id = id, Description = "From Simple Concrete Type" };
        }


        public int GetRoadLength()
        {
            return _roadLength;
        }

    }
}
