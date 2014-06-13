using FunWithGenerics.InterfacesAndDataContainers.DataContainers;
using System;

namespace FunWithGenerics.InterfacesAndDataContainers.Interfaces
{
    public interface ISimpleType
    {
        SimpleDataContainer GetSimpleClassForId(int id);
        void UpdateRoadLength(int newLength);

        int GetRoadLength();
    }
}
