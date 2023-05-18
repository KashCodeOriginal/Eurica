using UnityEngine;
using System.Threading.Tasks;

namespace Services.Factories.AbstractFactory
{
    public interface IAbstractFactory
    {
        Task<T> CreateInstance<T>(string path) where T : Object;
    }
}