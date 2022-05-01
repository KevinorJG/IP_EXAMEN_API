using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IModel<T>
    {
        Task<T> GetWather();
        Task<string> GetIcon();
        public string Recibir(string city);

    }
}
